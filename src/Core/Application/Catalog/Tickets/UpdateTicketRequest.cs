using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Tickets;
public class UpdateTicketRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? CardNumber { get; set; }
    public string? Type { get; set; }
    public bool? IsActive { get; set; }
    public string? Description { get; set; }
    //public Guid? VehicleTypeId { get; set; }
    public Guid? BranchId { get; set; }
}

public class UpdateTicketRequestValidator : AbstractValidator<UpdateTicketRequest>
{
    public UpdateTicketRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Ticket ID is required.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required.");

    }
}

public class UpdateTicketRequestHandler : IRequestHandler<UpdateTicketRequest, Result<Guid>>
{
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IRepository<Branch> _branchRepository;

    public UpdateTicketRequestHandler(IRepository<Ticket> ticketRepository, IRepository<Branch> branchRepository)
    {
        _ticketRepository = ticketRepository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateTicketRequest request, CancellationToken cancellationToken)
    {
        var existingTicket = await _ticketRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingTicket == null)
        {
            throw new NotFoundException("Ticket not found.");
        }

        var branchExists = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branchExists == null)
        {
            throw new NotFoundException("Provided BranchId does not match any existing branch.");
        }

        existingTicket.Update(
            request.Name,
            request.CardNumber,
            request.Type,
            request.IsActive,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,  // VehicleTypeId không được cập nhật
            request.BranchId
        );

        await _ticketRepository.UpdateAsync(existingTicket, cancellationToken);
        await _ticketRepository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(existingTicket.Id);
    }
}
