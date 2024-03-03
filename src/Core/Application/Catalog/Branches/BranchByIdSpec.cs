using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Branches;
public class BranchByIdSpec : Specification<Branch, BranchDto>, ISingleResultSpecification
{
    public BranchByIdSpec(Guid id)
    {
        Query.Where(p => p.Id == id);
    }
}

