using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries.Services
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> UploadImageAsync(IFormFileCollection files, string src)
        {
            var SavedImagePaths = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot","Images", src);
            if (Directory.Exists(ImageDirectory) is not true)
            {
                Directory.CreateDirectory(ImageDirectory);
            }


            foreach(var  item in files)
            {
                if (item.Length > 0)
                {
                    //get Image Name
                    var ImageName = item.FileName;
                    var ImagePath = $"Images/{src}/{ImageName}";
                    var root  = Path.Combine(ImageDirectory, ImageName);
                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                      await item.CopyToAsync(stream);
                    }
                SavedImagePaths.Add(ImagePath);
                }
            }
            return SavedImagePaths;
        }

        public void DeleteImageAsync(string imagePath)
        {
            var info = _fileProvider.GetFileInfo(imagePath);
            var root = info.PhysicalPath;
            File.Delete(root);
        }

    }
}
