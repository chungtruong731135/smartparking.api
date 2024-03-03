using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class VehicleByPlateNumberSpec : Specification<VehicleBlacklist>
{
    private readonly string _plateNumber;

    public VehicleByPlateNumberSpec(string plateNumber)
    {
        _plateNumber = plateNumber;
        Query.Where(p => p.PlateNumber == _plateNumber);
    }
}
