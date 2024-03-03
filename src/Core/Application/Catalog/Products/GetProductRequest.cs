﻿namespace TD.WebApi.Application.Catalog.Products;

public class GetProductRequest : IRequest<Result<ProductDetailsDto>>
{
    public Guid Id { get; set; }

    public GetProductRequest(Guid id) => Id = id;
}

public class GetProductRequestHandler : IRequestHandler<GetProductRequest, Result<ProductDetailsDto>>
{
    private readonly IRepository<Product> _repository;
    private readonly IStringLocalizer _t;

    public GetProductRequestHandler(IRepository<Product> repository, IStringLocalizer<GetProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Result<ProductDetailsDto>> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.FirstOrDefaultAsync(
            (ISpecification<Product, ProductDetailsDto>)new ProductByIdWithBrandSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Product {0} Not Found.", request.Id]);
        return Result<ProductDetailsDto>.Success(item);
    }
}