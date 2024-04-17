namespace TrainingFPTCo.Helpers
{
    public class TypeFileHelper
    {
        public static class FileHelper
        {
            public static bool IsAllowedFileType(string fileName)
            {
                // Danh sách các phần mở rộng được chấp nhận
                string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".mp3", ".mp4" };

                // Lấy phần mở rộng của tệp
                string fileExtension = Path.GetExtension(fileName);

                // Kiểm tra xem phần mở rộng của tệp có trong danh sách được chấp nhận không
                return allowedExtensions.Contains(fileExtension);
            }
        }
    }
}
