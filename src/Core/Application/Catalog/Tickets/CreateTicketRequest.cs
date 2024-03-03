using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Tickets;
public class CreateTicketRequest : IRequest<Result<Guid>>
{
    public string? Name { get; set; }
    public string? CardNumber { get; set; }
    public string? Type { get; set; }
    //public bool? IsActive { get; set; } = false;
    public string? Description { get; set; }
    public Guid? BranchId { get; set; }
}

public class CreateTicketRequestValidator : AbstractValidator<CreateTicketRequest>
{
    public CreateTicketRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required.");
        RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.");
        RuleFor(x => x.BranchId).NotEmpty().WithMessage("BranchId is required.");
    }
}

public class CreateTicketRequestHandler : IRequestHandler<CreateTicketRequest, Result<Guid>>
{
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IRepository<Branch> _branchRepository;

    public CreateTicketRequestHandler(IRepository<Ticket> ticketRepository, IRepository<Branch> branchRepository)
    {
        _ticketRepository = ticketRepository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<Guid>> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
    {
        var branchExists = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branchExists == null)
        {
            throw new NotFoundException("Provided BranchId does not match any existing branch.");
        }

        var ticket = new Ticket(
            request.Name,
            request.CardNumber,
            request.Type,
            false, //mac dinh khoi tao the moi thi isActive = false;
            false,
            null,
            null,
            false,
            null,
            null,
            request.Description,
            null,
            request.BranchId
        );

        await _ticketRepository.AddAsync(ticket, cancellationToken);
        await _ticketRepository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(ticket.Id);
    }
}

