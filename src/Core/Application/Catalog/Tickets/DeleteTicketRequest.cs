using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Tickets;
public class DeleteTicketRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; }

    public DeleteTicketRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteTicketRequestHandler : IRequestHandler<DeleteTicketRequest, Result<Guid>>
{
    private readonly IRepository<Ticket> _ticketRepository;

    public DeleteTicketRequestHandler(IRepository<Ticket> ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteTicketRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.Id, cancellationToken);
        if (ticket == null)
        {
            throw new NotFoundException($"Ticket with ID {request.Id} not found.");
        }

        await _ticketRepository.DeleteAsync(ticket);
        await _ticketRepository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}
