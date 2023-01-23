using Azure.Storage.Blobs;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Application.CacheService;

public class BlobStorageService
{
    private readonly ILogger<BlobStorageService> _logger;
    private readonly BlobContainerClient? _container;
    public BlobStorageService(ILogger<BlobStorageService> logger, IConfiguration _configuration)
    {
        try
        {
            _logger = logger;
            _container = new BlobContainerClient(_configuration.GetConnectionString(""), "blobstorage");
        }catch(Exception ex)
        {
            _logger = logger;
            _logger.LogError("BlobStorageService Cannot Start::" + ex.Message, ex);
        }
    } 


    private readonly Dictionary<Type, Func<object, string>> formatters = new(){
        { typeof(DateTime), value => $"{GetDateString((DateTime)value)}" },
        { typeof(Guid), value => $"{value}" },
        { typeof(bool), value => $"{value}" },
        { typeof(int), value => $"{value}" },
        { typeof(long), value => $"{value}" },
        { typeof(string), value => $"{Regex.Replace(value.ToString()!, @"[^0-9a-zA-Z]+", "")}" },
    };
    private static string GetDateString(DateTime date) => date.ToString("ddMMyyyy", CultureInfo.InvariantCulture);

    public async Task GenerateBlob(object obj, string filename, Blobs blob)
    {
        try
        {
            using (MemoryStream fileStream = new MemoryStream())
            {
                var content = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(obj));
                // Write the contents of the file to the MemoryStream
                // (You can use any method you like to populate the stream, such as reading from a file on disk or generating the contents programmatically)
                fileStream.Write(content, 0, content.Length);
                fileStream.Position = 0;
                string folderName = $"{blob}/";
                if (_container != null)
                {
                    BlobClient blobClient = _container.GetBlobClient(folderName + filename + ".json");
                    // Upload the file to the folder
                    await blobClient.UploadAsync(fileStream);
                    _logger.LogInformation($"Success file: {filename} in blobStorage.");
                }    
            }
        }
        catch(Exception ex) 
        {
            _logger.LogError(ex.Message, ex);
            Console.WriteLine(ex.Message);
        }

    }

    public async Task<T?> FindInBlob<T>(string filename, Blobs blob) where T: class
    {
        try
        {
            string folderName = $"{blob}/";
            if(_container != null)
            {
                BlobClient blobClient = _container.GetBlobClient(folderName + filename + ".json");
                if (await blobClient.ExistsAsync())
                {
                    using (Stream stream = await blobClient.OpenReadAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<T>(stream);
                    }
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public string? GenerateFileName(object typeObject)
    {
        try
        {
            Type type = typeObject.GetType();
            var sb = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var prop in properties.Select((value, i) => new { i, value }))
            {
                Func<object, string> formatter = formatters.ContainsKey(prop.value.PropertyType) ?
                    formatters[prop.value.PropertyType] : value => value.ToString()!;
                if (prop.i < properties.Length - 1)
                    sb.Append($"{formatter(prop.value.GetValue(typeObject)!)}-");
                else
                    sb.Append($"{formatter(prop.value.GetValue(typeObject)!)}");
            }
            return sb.ToString();
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            Console.WriteLine(ex.Message);
            return null;
        }

    }

}
