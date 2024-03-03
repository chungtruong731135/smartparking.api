namespace TD.WebApi.Application.Catalog.EventVehicles;
public class EnterVehicleRequest : IRequest<Result<Guid>>
{
    public string? PlateNumber { get; set; }
    public string? DetectedPlateNumber { get; set; }
    public DateTime? DateTimeEvent { get; set; }
    public FileUploadRequest? PlateImage { get; set; }
    public string? VehicleImage { get; set; }
    public string? HardwareSyncId { get; set; }
    //public Guid? UserId { get; set; }
    public Guid? TicketId { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get; set; }
    public int? Status { get; set; }
}
public class EnterVehicleRequestHandler : IRequestHandler<EnterVehicleRequest, Result<Guid>>
{
    private readonly IRepository<EventVehicle> _repository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<BranchUser> _branchUserRepository;
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IFileStorageService _file;

    public EnterVehicleRequestHandler(IRepository<EventVehicle> repository, IRepository<Branch> branchRepository, IRepository<BranchUser> branchUserRepository, IRepository<Ticket> ticketRepository, IFileStorageService file)
    {
        _repository = repository;
        _branchRepository = branchRepository;
        _branchUserRepository = branchUserRepository;
        _ticketRepository = ticketRepository;
        _file = file;
    }


    public async Task<Result<Guid>> Handle(EnterVehicleRequest request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(request.BranchId);
        if (branch == null) throw new NotFoundException("Branch not found.");

        var branchUsers = await _branchUserRepository.ListAsync(cancellationToken);
        var branchUser = branchUsers.SingleOrDefault(bu => bu.BranchId == request.BranchId);
        if (branchUser == null) throw new NotFoundException("User not found in the specified branch.");

        var userName = branchUser.UserName;
        var userId = branchUser.UserId;

        var tickets = await _ticketRepository.ListAsync(cancellationToken);
        var ticket = tickets.SingleOrDefault(t => t.BranchId == request.BranchId && t.Id == request.TicketId);
        if (ticket == null) throw new NotFoundException("Ticket not found for the specified branch.");
        if (ticket.IsActive.HasValue && ticket.IsActive.Value)
        {
            throw new InvalidOperationException("Ticket is already active.");
        }

        ticket.IsActive = true;

        string plateImagePath = await _file.UploadAsync<EventVehicle>(request.PlateImage, FileType.Image, cancellationToken);


        var eventVehicle = new EventVehicle(
            request.PlateNumber,
            request.DetectedPlateNumber,
            request.DateTimeEvent,
            plateImagePath,
            request.VehicleImage,
            "IN",
            request.HardwareSyncId,
            userName,
            userId,
            request.TicketId,
            request.BranchId,
            request.Description,
            request.Status
        );

        await _repository.AddAsync(eventVehicle);
        await _ticketRepository.UpdateAsync(ticket, cancellationToken);
        return Result<Guid>.Success(eventVehicle.Id);
    }
}
