using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Branches;
public class BranchByNameSpec : Specification<Branch>, ISingleResultSpecification
{
    public BranchByNameSpec(string name)
    {
        Query.Where(b => b.Name == name);
    }
}

