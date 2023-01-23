using Microsoft.Extensions.Logging;
using Domain.Enums;
using Domain.Interfaces.Services;

namespace Application.CacheService;

public class CacheService : ICacheService
{
    private readonly ILogger<CacheService> _logger;
    private readonly BlobStorageService _blobService;
    public CacheService(ILogger<CacheService> logger, BlobStorageService blobService)
    {
        _logger = logger;
        _blobService = blobService;
    }
    public void SaveInCache(object obj, object objFileName, Blobs blob)
    {
        var filename = _blobService.GenerateFileName(objFileName);
        if(filename != null)
        {
            _logger.LogInformation($"Uploading file: {filename} in blobStorage.");
            Thread thread = new Thread(async () => await _blobService.GenerateBlob(obj, filename, blob));
            thread.Start();
        }
    }

    public async Task<T?> FindInCache<T>(object objFileName, Blobs blob) where T : class
    {
        var filename = _blobService.GenerateFileName(objFileName);
        if (filename != null)
        {
            _logger.LogInformation($"Uploading file: {filename} in blobStorage.");
            return await _blobService.FindInBlob<T>(filename, blob);
        }
        return null;
    }
}
