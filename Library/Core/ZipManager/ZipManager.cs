using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.ZipManager
{
    public class ZipManager
    {
        private readonly IHostingEnvironment _env;

        public ZipManager(IHostingEnvironment env)
        {
            _env = env;
        }

        public string QrImagesFolderPath => new StringBuilder(_env.WebRootPath).Append(@"\qrImages\").ToString();
        public string ZipFolder => string.Concat(QrImagesFolderPath, "/zips/");

        public void CreateZip(string name)
        {
            File.Create(name);
        }

        public byte[] AddFiles(string fileName, List<string> files)
        {
            var filePath = string.Concat(ZipFolder, fileName, ".zip");

            using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(filePath)))
            {
                zipOutputStream.SetLevel(9);

                byte[] buffer = new byte[4096];

                foreach (var file in files)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file))
                    {
                        DateTime = DateTime.Now,
                        IsUnicodeText = true
                    };

                    zipOutputStream.PutNextEntry(entry);

                    using (FileStream fileStream = File.OpenRead(file))
                    {
                        int sourcesBytes;
                        do
                        {
                            sourcesBytes = fileStream.Read(buffer, 0, buffer.Length);
                            zipOutputStream.Write(buffer, 0, sourcesBytes);
                        } while (sourcesBytes > 0);
                    }
                }

                zipOutputStream.Finish();
                zipOutputStream.Flush();
                zipOutputStream.Close();
            }

            return File.ReadAllBytes(filePath);
        }

        public string ByteArrayToFile(string fileName, byte[] byteArray)
        {

            fileName = string.Concat(QrImagesFolderPath, fileName, ".jpg");

            using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            fs.Write(byteArray, 0, byteArray.Length);

            return fileName;
        }
    }
}
