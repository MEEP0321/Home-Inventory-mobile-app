using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Services
{
    public class MediaService
    {
        public async Task<string> SavePhotoAsync(FileResult photo)
        {
            if (photo == null)
                return null;

            string localPath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localPath);
            await sourceStream.CopyToAsync(localFileStream);

            return localPath;
        }
    }
}
