using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrainingFPTCo.Validations;

namespace TrainingFPTCo.Models
{
    public class TopicViewModel
    {
        public List<TopicDetail> TopicDetailsList { get; set; }
    }

    public class TopicDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Choose Course, please")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Enter name's topic, please")]
        public string Name { get; set; }
        public string? CourseName { get; set; }
        public string? ViewCourseName { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Enter status, please")]
        public string Status { get; set; }

       
        [AllowExtensionFile(new string[] { ".pdf", ".doc", ".docx", ".mp3", ".mp4", })] // Thêm các định dạng tệp tin mới vào đây
        [AllowSizeFile(50 * 1024 * 1024)] // Giả sử cho phép tải lên tệp tin tối đa 50MB
        public IFormFile? Documents { get; set; }
        public string? ViewDocuments { get; set; }

       
        [AllowExtensionFile(new string[] { ".png", ".jpg", ".jpeg", ".pdf", ".doc", })]
        [AllowSizeFile(5 * 1024 * 1024)]
        public IFormFile? AttachFiles { get; set; }
        public string? ViewAttachFiles { get; set; }

       
        [AllowExtensionFile(new string[] { ".png", ".jpg", ".jpeg", })]
        public IFormFile? PosterTopic { get; set; }

        public string? ViewPosterTopic { get; set; }

        [Required(ErrorMessage = "Enter type of document, please")]
        public string TypeDocument { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; internal set; }
    }
}