using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Vehicles.API.Helpers
{
    public interface IBlobHelper
    {

        Task<Guid> UpLoadBlobAsync(IFormFile file, string containerName);

        Task<Guid> UpLoadBlobAsync(byte[] file, string containerName);

        Task<Guid> UpLoadBlobAsync(string image, string containerName);

        Task DeletBlobAsync(Guid id, string containerName);
    }
}
