using System.Drawing;
using System.IO;
using LazZiya.ImageResize;

namespace SystemZglaszaniaAwariiGlowny.Infrastruktura

{
    public class ImageFileUpl
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        public ImageFileUpl(IWebHostEnvironment environment)
        {
            hostingEnvironment = environment;
        }

      

        public FileSendRes SendFile(IFormFile picture, string destination,int width)
        {
            FileSendRes SendingFile = new();


            string extension = Path.GetExtension(picture.FileName);
            if (!FileTypeCheck(extension))
            {
                SendingFile.Name = Path.GetFileName(picture.FileName);
                SendingFile.Success = false;
                SendingFile.Error = "Niepoprawny typ pliku graficznego.";
                return SendingFile;
            }

            SendingFile.Name = Guid.NewGuid().ToString() + extension;
            var upload = Path.Combine(hostingEnvironment.WebRootPath, destination);
            var filePath = Path.Combine(upload, SendingFile.Name); ;

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                picture.CopyTo(fileStream);
            }

            string path = Path.Combine(filePath);

            using (var imgFile = Image.FromFile(path))
            {
                var miniFile = imgFile.ScaleByWidth(width);
                upload = Path.Combine(hostingEnvironment.WebRootPath,destination + "\\magazyny");
                filePath = Path.Combine(upload, SendingFile.Name);
                miniFile.SaveAs(filePath);
            }
            SendingFile.Success = true;
            return SendingFile;
        }

        private static bool FileTypeCheck(string extension)
        {
            return extension.ToLower() switch
            {
                ".jpg" or ".png" or ".gif" => true,
                _ => false,
            };
        }

    }
}
