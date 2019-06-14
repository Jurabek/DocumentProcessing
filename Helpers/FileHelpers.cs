using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentProcessing.Models;
using Microsoft.AspNetCore.Http;

namespace DocumentProcessing.Helpers
{
    public static class FileHelpers
    {
        public static async Task<IList<ScannedFile>> GetScannedFiles(IList<IFormFile> files)
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
                            Startup.Progress = progress < 0 ? 0 : progress;
                            await Task.Delay(2);
                        }
                    }

                    scannedFiles.Add(new ScannedFile
                    {
                        FileName = f.FileName,
                        ContentType = f.ContentType,
                        Length = f.Length,
                        File = output.ToArray()
                    });
                }
            }

            return scannedFiles;
        }
    }
}