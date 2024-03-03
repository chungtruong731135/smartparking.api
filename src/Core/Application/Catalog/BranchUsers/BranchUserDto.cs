using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.BranchUsers;
public class BranchUserDto : IDto
{
    public Guid Id { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }

    // Constructor
    public BranchUserDto(Guid id, Guid? branchId, Guid? userId, string? userName)
    {
        Id = id;
        BranchId = branchId;
        UserId = userId;
        UserName = userName;
    }
}

