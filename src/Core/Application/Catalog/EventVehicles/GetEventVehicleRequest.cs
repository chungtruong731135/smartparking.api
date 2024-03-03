using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.EventVehicles;
public class GetEventVehicleRequest : IRequest<Result<EventVehicleDto>>
{
    public Guid Id { get; set; }

    public GetEventVehicleRequest(Guid id) => Id = id;
}

public class GetEventVehicleRequestHandler : IRequestHandler<GetEventVehicleRequest, Result<EventVehicleDto>>
{
    private readonly IRepository<EventVehicle> _repository;
    private readonly IStringLocalizer _t;

    public GetEventVehicleRequestHandler(IRepository<EventVehicle> repository, IStringLocalizer<GetEventVehicleRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Result<EventVehicleDto>> Handle(GetEventVehicleRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.FirstOrDefaultAsync(
            (ISpecification<EventVehicle, EventVehicleDto>)new EventVehicleByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["EventVehicle {0} Not Found.", request.Id]);
        return Result<EventVehicleDto>.Success(item);
    }
}
