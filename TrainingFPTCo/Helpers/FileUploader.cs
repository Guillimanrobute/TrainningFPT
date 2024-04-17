using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace TrainingFPTCo.Helpers
{
    public class FileUploader
    {
        public static string UploadFile(IFormFile file)
        {
            string uniqueFileName;
            try
            {
                string pathUploadServer = "wwwroot/uploads/documents"; // Đường dẫn lưu trữ tệp tin
                string fileName = file.FileName;

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                fileNameWithoutExtension = CommonText.GenerateSlug(fileNameWithoutExtension);

                string ext = Path.GetExtension(fileName);

                // Tạo tên tệp tin không trùng lặp
                string uniqueStr = Guid.NewGuid().ToString(); // Chuỗi ngẫu nhiên
                string fileNameUpload = uniqueStr + "-" + fileNameWithoutExtension + ext;

                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), pathUploadServer, fileNameUpload);
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                uniqueFileName = fileNameUpload;
            }
            catch (Exception ex)
            {
                uniqueFileName = ex.Message.ToString();
            }
            return uniqueFileName;
        }
    }
}
