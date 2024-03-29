﻿namespace TD.WebApi.Application.Catalog.Brands;

public class GenerateRandomBrandRequest : IRequest<Result<string>>
{
    public int NSeed { get; set; }
}

public class GenerateRandomBrandRequestHandler : IRequestHandler<GenerateRandomBrandRequest, Result<string>>
{
    private readonly IJobService _jobService;

    public GenerateRandomBrandRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<Result<string>> Handle(GenerateRandomBrandRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<IBrandGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(Result<string>.Success(jobId));
    }
}