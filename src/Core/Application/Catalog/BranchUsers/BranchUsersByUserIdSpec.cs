using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.BranchUsers;
public class BranchUsersByUserIdSpec : Specification<BranchUser, BranchUserDto>
{
    public BranchUsersByUserIdSpec(Guid? userId)
    {
        Query.Where(b => b.UserId == userId);
    }
}


