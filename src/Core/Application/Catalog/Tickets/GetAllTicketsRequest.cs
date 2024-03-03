using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Tickets;
public class GetAllTicketsRequest : IRequest<Result<List<TicketDto>>>
{

}

public class GetAllTicketsRequestHandler : IRequestHandler<GetAllTicketsRequest, Result<List<TicketDto>>>
{
    private readonly IRepository<Ticket> _ticketRepository;

    public GetAllTicketsRequestHandler(IRepository<Ticket> ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Result<List<TicketDto>>> Handle(GetAllTicketsRequest request, CancellationToken cancellationToken)
    {
        var tickets = await _ticketRepository.ListAsync(cancellationToken);

        var ticketDtos = tickets.Select(t => new TicketDto
        {
            Name = t.Name,
            CardNumber = t.CardNumber,
            Type = t.Type,
            IsActive = t.IsActive,
            IsLocked = t.IsLocked,
            LockedDate = t.LockedDate,
            LockedNote = t.LockedNote,
            IsLose = t.IsLose,
            LoseDate = t.LoseDate,
            LoseNote = t.LoseNote,
            Description = t.Description,
            VehicleTypeId = t.VehicleTypeId,
            BranchId = t.BranchId
        }).ToList();


        return Result<List<TicketDto>>.Success(ticketDtos);
    }
}

