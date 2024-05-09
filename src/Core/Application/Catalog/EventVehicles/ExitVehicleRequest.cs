using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.EventVehicles;
public class ExitVehicleRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

}

public class ExitVehicleRequestHandler : IRequestHandler<ExitVehicleRequest, Result<Guid>>
{
    private readonly IRepository<EventVehicle> _repository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<BranchUser> _branchUserRepository;
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IRepository<CheckoutVehicleEvent> _checkoutRepository;

    public ExitVehicleRequestHandler(IRepository<EventVehicle> repository, IRepository<Branch> branchRepository, IRepository<BranchUser> branchUserRepository, IRepository<Ticket> ticketRepository, IRepository<CheckoutVehicleEvent> checkoutRepository)
    {
        _repository = repository;
        _branchRepository = branchRepository;
        _branchUserRepository = branchUserRepository;
        _ticketRepository = ticketRepository;
        _checkoutRepository = checkoutRepository;
    }

    public async Task<Result<Guid>> Handle(ExitVehicleRequest request, CancellationToken cancellationToken)
    {
        var eventVehicle = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (eventVehicle == null)
        {
            throw new NotFoundException("Event Vehicle not found.");
        }

        eventVehicle.LaneDirection = "OUT";

        var ticketId = eventVehicle.TicketId;

        var ticket = await _ticketRepository.GetByIdAsync(ticketId, cancellationToken);

        if (ticket == null) {
            throw new NotFoundException("ticket not found.");
        }

        ticket.IsActive = false;

        var utcNow = DateTime.UtcNow;
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZoneInfo);

        var checkoutEvent = new CheckoutVehicleEvent(
            eventVehicleId: eventVehicle.Id,
            checkoutDate: vietnamTime,
            amount: 3000,
            plateNumber: eventVehicle.PlateNumber,
            branchId: eventVehicle.BranchId
        );


        await _repository.UpdateAsync(eventVehicle, cancellationToken);
        await _ticketRepository.UpdateAsync(ticket, cancellationToken);
        await _checkoutRepository.AddAsync(checkoutEvent, cancellationToken);

        return Result<Guid>.Success(eventVehicle.Id);
    }

}