namespace TD.WebApi.Application.Catalog.Brands;

public class DeleteRandomBrandRequest : IRequest<Result<string>>
{
}

public class DeleteRandomBrandRequestHandler : IRequestHandler<DeleteRandomBrandRequest, Result<string>>
{
    private readonly IJobService _jobService;

    public DeleteRandomBrandRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<Result<string>> Handle(DeleteRandomBrandRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<IBrandGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(Result<string>.Success(jobId));
    }
}