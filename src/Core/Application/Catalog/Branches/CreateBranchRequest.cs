using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WebApi.Application.Common.Persistence;

namespace TD.WebApi.Application.Catalog.Branches;
public class CreateBranchRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? Logo { get; set; }
    public string? Description { get; set; }
}
public class CreateBranchRequestHandler : IRequestHandler<CreateBranchRequest, Result<Guid>>
{
    private readonly IRepository<Branch> _repository;
    private readonly IStringLocalizer _t;

    public CreateBranchRequestHandler(IRepository<Branch> repository, IStringLocalizer<CreateBranchRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<Result<Guid>> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var branch = new Branch(request.Name, request.PhoneNumber, request.Email, request.Website, request.Address, request.Logo, request.Description);

        await _repository.AddAsync(branch, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(branch.Id);
    }
}

public class CreateBranchRequestValidator : CustomValidator<CreateBranchRequest>
{
    private readonly IReadRepository<Branch> _repository;
    public CreateBranchRequestValidator(IReadRepository<Branch> repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.")
            .MustAsync((name, cancellationToken) => BeUniqueName(name, cancellationToken))
            .WithMessage("Name must be unique.");


        RuleFor(x => x.PhoneNumber)
            .Matches(@"^[0-9]+$").WithMessage("Phone number can only contain numbers.")
            .Length(8, 11).WithMessage("Phone number must be between 8 and 11 digits.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));


        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Website)
            .Must(BeAValidUrl).WithMessage("Invalid website URL.")
            .When(x => !string.IsNullOrWhiteSpace(x.Website));

        // Add more rules as needed
    }

    private bool BeAValidUrl(string url)
    {
        Uri uriResult;
        return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        var spec = new BranchByNameSpec(name);
        var existingBranch = await _repository.FirstOrDefaultAsync(spec);
        return existingBranch == null;
    }
}

