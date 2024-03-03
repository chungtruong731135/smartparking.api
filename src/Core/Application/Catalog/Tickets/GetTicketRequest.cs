using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WebApi.Application.Catalog.Products;

namespace TD.WebApi.Application.Catalog.Tickets;
public class GetTicketRequest : IRequest<Result<TicketDto>>
{
    public Guid Id { get; set; }
    public GetTicketRequest(Guid id)
    {
        Id = id;
    }
}

public class GetTicketRequestHandler : IRequestHandler<GetTicketRequest, Result<TicketDto>>
{
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IStringLocalizer _t;

    public GetTicketRequestHandler(IRepository<Ticket> ticketRepository, IStringLocalizer<GetTicketRequestHandler> t)
    {
        _ticketRepository = ticketRepository;
        _t = t;
    }

    public async Task<Result<TicketDto>> Handle(GetTicketRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.FirstOrDefaultAsync(
            (ISpecification<Ticket, TicketDto>)new TicketByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Ticket {0} Not Found.", request.Id]);
        return Result<TicketDto>.Success(ticket);
    }
}
