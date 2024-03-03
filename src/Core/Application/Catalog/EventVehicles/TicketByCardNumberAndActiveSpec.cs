using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.EventVehicles;
public class TicketByCardNumberAndActiveSpec : Specification<Ticket>
{
    public TicketByCardNumberAndActiveSpec(string cardNumber)
    {
        Query.Where(ticket => ticket.CardNumber == cardNumber && ticket.IsActive == true);
    }
}
