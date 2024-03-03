using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Tickets;
public class TicketByIdSpec : Specification<Ticket, TicketDto>, ISingleResultSpecification
{
    public TicketByIdSpec(Guid ticketId)
    {
        Query.Where(ticket => ticket.Id == ticketId);
    }
}
