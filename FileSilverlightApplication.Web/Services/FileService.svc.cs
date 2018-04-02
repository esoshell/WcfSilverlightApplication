using System;
using System.IO;

namespace FileSilverlightApplication.Web.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FileService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FileService.svc or FileService.svc.cs at the Solution Explorer and start debugging.
    public class FileService : IFileService
    {
        public int DoWork(int x)
        {
            return x + 1;
        }

        public void UploadFile(UploadFileRequest request)
        {
            if (request != null)
            {
                var serverFileName = @"E:\Source\" + request.FileName;

                using (FileStream outfile = new FileStream(serverFileName, FileMode.Create))
                {
                    const int bufferSize = 65536; // 64K
                    Byte[] buffer = new Byte[bufferSize];
                    int bytesRead = request.FileContents.Read(buffer, 0, bufferSize);

                    while (bytesRead > 0)
                    {
                        outfile.Write(buffer, 0, bytesRead);
                        bytesRead = request.FileContents.Read(buffer, 0, bufferSize);
                    }
                }
            }
        }

        public void UploadStream(Stream stream)
        {
            if (stream != null)
            {
                var serverFileName = @"E:\Source\test.json";

                using (FileStream outfile = new FileStream(serverFileName, FileMode.Create))
                {
                    const int bufferSize = 65536; // 64K
                    Byte[] buffer = new Byte[bufferSize];
                    int bytesRead = stream.Read(buffer, 0, bufferSize);

                    while (bytesRead > 0)
                    {
                        outfile.Write(buffer, 0, bytesRead);
                        bytesRead = stream.Read(buffer, 0, bufferSize);
                    }
                }
            }
        }
    }
}
