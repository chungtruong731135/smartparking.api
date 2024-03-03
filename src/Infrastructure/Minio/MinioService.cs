using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Minio;
using Microsoft.Extensions.Localization;
using TD.WebApi.Application.Common.Exceptions;
using System.Text.RegularExpressions;
using TD.WebApi.Infrastructure.Common.Extensions;
using TD.WebApi.Application.Common.Minio;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using System.Net.Http;
using System.Net;

namespace TD.WebApi.Infrastructure.Minio;

public class MinioService : IMinioService
{
    private readonly MinioSettings _settings;
    private readonly ILogger<MinioService> _logger;
    private readonly IStringLocalizer _t;
    private readonly MinioClient _minioClient;

    public MinioService(IOptions<MinioSettings> settings, ILogger<MinioService> logger, IStringLocalizer<MinioService> localizer)
    {
        _t = localizer;
        _settings = settings.Value;
        _logger = logger;
        _minioClient = new MinioClient()
                              .WithEndpoint(_settings.Endpoint)
                              .WithCredentials(_settings.AccessKey, _settings.SecretKey)
                              .WithSSL()
                              .Build();
    }

    public async Task<string> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
    {
        var beArgs = new BucketExistsArgs()
                   .WithBucket(bucketName);

        bool bucketExists = await _minioClient.BucketExistsAsync(beArgs);
        if (!bucketExists)
        {
            var mbArgs = new MakeBucketArgs()
                       .WithBucket(bucketName);
            await _minioClient.MakeBucketAsync(mbArgs);
        } 

        var fileStream = file.OpenReadStream();

        string? fileName = file.FileName.Trim('"');
        fileName = fileName.RemoveSpecialCharacters(string.Empty);
        fileName = fileName.ReplaceWhitespace("_");

        var request = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(string.IsNullOrEmpty(prefix) ? fileName : $"{prefix?.TrimEnd('/')}/{fileName}")
            .WithStreamData(fileStream)
            .WithContentType(file.ContentType)
            .WithObjectSize(fileStream.Length)
            ;
        await _minioClient.PutObjectAsync(request);

        string result = bucketName + "/";
        result += string.IsNullOrEmpty(prefix) ? fileName : $"{prefix?.TrimEnd('/')}/{fileName}";

        return result;
    }

    public async Task<MemoryStream> GetFileByKeyAsync(string bucketName, string key)
    {
        var beArgs = new BucketExistsArgs()
                  .WithBucket(bucketName);

        bool bucketExists = await _minioClient.BucketExistsAsync(beArgs);
        if (!bucketExists)
        {
            throw new InternalServerException(string.Format(_t["Khong ton tai {0}."], bucketName));
        }
        var memory = new MemoryStream();

        try
        {
            StatObjectArgs statObjectArgs = new StatObjectArgs()
                                       .WithBucket(bucketName)
                                       .WithObject(key);
            await _minioClient.StatObjectAsync(statObjectArgs);

            GetObjectArgs getObjectArgs = new GetObjectArgs()
                                     .WithBucket(bucketName)
                                     .WithObject(key)
                                     .WithCallbackStream(stream =>
                                     {
                                         stream.CopyTo(memory);
                                         stream.Dispose();
                                     })
                                    ;

            var file = await _minioClient.GetObjectAsync(getObjectArgs);
        }
        catch (Exception e)
        {
            throw new InternalServerException(string.Format(_t["Loi {0}."], bucketName));
        }

        return memory;
    }

    [Obsolete]
    public async Task<List<AttachmentResponse>> UploadFilesAsync(List<IFormFile> files, string? bucketName, string? prefix, CancellationToken cancellationToken = default)
    {
        List<AttachmentResponse> listFile = new List<AttachmentResponse>();
        if (files.Any(f => f.Length == 0))
        {
            throw new InvalidOperationException("File Not Found.");
        }

        if (string.IsNullOrEmpty(bucketName))
        {
            bucketName = _settings.BucketName;
        }

        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                string? fileName = formFile.FileName.Trim('"');
                fileName = fileName.RemoveSpecialCharacters(string.Empty);
                fileName = fileName.ReplaceWhitespace("_");

                var attachment = new AttachmentResponse();
                attachment.Name = fileName;
                attachment.Type = Path.GetExtension(formFile.FileName);
                attachment.Url = await UploadFileAsync(formFile, bucketName!, prefix ?? Guid.NewGuid().ToString());
                attachment.Size = formFile.Length;

                listFile.Add(attachment);
            }
        }

        return await Task.FromResult(listFile);

    }

    public Task<AttachmentResponse> UploadFileAsync<T>(IFormFile? file, string? bucketName, string? prefix, CancellationToken cancellationToken = default)
        where T : class
    {
        throw new NotImplementedException();
    }

}