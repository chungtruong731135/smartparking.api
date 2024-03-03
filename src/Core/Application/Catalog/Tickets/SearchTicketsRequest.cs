using TD.WebApi.Application.Common.Persistence;

namespace TD.WebApi.Application.Catalog.Tickets;
public class SearchTicketsRequest : PaginationFilter, IRequest<PaginationResponse<TicketDto>>
{
    public Guid? BranchId { get; set; }
    public bool? IsActive { get; set; }
}

public class TicketsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Ticket, TicketDto>
{
    public TicketsBySearchRequestSpec(SearchTicketsRequest request)
        : base(request) =>
        Query
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(t => t.BranchId.Equals(request.BranchId!.Value), request.BranchId.HasValue)
            .Where(t => t.IsActive == request.IsActive, request.IsActive.HasValue);
}

public class SearchTicketsRequestHandler : IRequestHandler<SearchTicketsRequest, PaginationResponse<TicketDto>>
{
    private readonly IReadRepository<Ticket> _repository;

    public SearchTicketsRequestHandler(IReadRepository<Ticket> repository)
    {
        _repository = repository;
    }

    public async Task<PaginationResponse<TicketDto>> Handle(SearchTicketsRequest request, CancellationToken cancellationToken)
    {
        var spec = new TicketsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}