using System.Runtime.CompilerServices;

namespace Company.Zeinab4.PL.Helper
{
    public static class DocumentSetting
    {
        //1-Upload
        public static string UploadFile(IFormFile file, string folderName)
        {
            ////1-Get Folder Location ==>Loction +Name Image 
            //var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "@wwwroot\files", folderName);

            //2-FileName
            var fileName = $"{Guid.NewGuid() + file.FileName}";


            //file path 
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;

        }


        public static   void DeleteFile(string fileName , string folderName )
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "@wwwroot\files", folderName, fileName);
            if(File.Exists(folderPath))
            {
                File.Delete(folderPath);
            }

        }


        }
        
}
