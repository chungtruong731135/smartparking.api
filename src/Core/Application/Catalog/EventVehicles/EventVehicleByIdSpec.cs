using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.EventVehicles;
public class EventVehicleByIdSpec : Specification<EventVehicle, EventVehicleDto>, ISingleResultSpecification
{
    public EventVehicleByIdSpec(Guid id) =>
        Query.Where(ev => ev.Id == id);
}

