using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentProcessing.Models;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace DocumentProcessing.Helpers
{
    public interface IFileUploader
    {
        Task<IList<ScannedFile>> GetScannedFilesForDocument(Document document, IList<IFormFile> files);
    }

    public class FileUploader : IFileUploader
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IElectronicStamp _electronicStamp;

        public FileUploader(
            IHttpContextAccessor httpContextAccessor,
            IElectronicStamp electronicStamp)
        {
            _httpContextAccessor = httpContextAccessor;
            _electronicStamp = electronicStamp;
        }

        public async Task<IList<ScannedFile>> GetScannedFilesForDocument(Document document, IList<IFormFile> files)
        {
            long totalBytes = files.Sum(f => f.Length);
            IList<ScannedFile> scannedFiles = new List<ScannedFile>();
            foreach (var f in files)
            {
                byte[] buffer = new byte[16 * 1024];
                using (var output = new MemoryStream()) 
                {
                    using (var input = f.OpenReadStream())
                    {
                        long totalReadBytes = 0;
                        int readBytes;

                        while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            await output.WriteAsync(buffer, 0, readBytes);
                            totalReadBytes += readBytes;
                            var progress = (int) ((float) totalReadBytes / (float) totalBytes * 100.0);
                            _httpContextAccessor.HttpContext.Session.SetInt32("progress", progress < 0 ? 0 : progress);
                        }
                    }

                    using (var image = Image.Load(output.ToArray()))
                    {
                        _electronicStamp.Process(image, document.EntryNumber.ToString(), document.Date);
                        using (var newImage = new MemoryStream())
                        {
                            image.SaveAsJpeg(newImage);
                            scannedFiles.Add(new ScannedFile
                            {
                                FileName = f.FileName,
                                ContentType = f.ContentType,
                                Length = f.Length,
                                File = newImage.ToArray(),
                                CreatedDate = DateTime.Now
                            });
                        }
                    }
                }
            }

            return scannedFiles;
        }
    }
}