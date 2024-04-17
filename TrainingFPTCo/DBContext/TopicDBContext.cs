using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingFPTCo.DBContext
{
    public class TopicDBContext
    {
        // Định danh chính
        [Key]
        public int Id { get; set; }

        // Khóa ngoại trỏ đến bảng Courses
        [ForeignKey("CourseId")]
        public CourseDBContext Course { get; set; }
        public int CourseId { get; set; }

        // Tên chủ đề
        [Column("Name", TypeName = "VARCHAR(50)")]
        [Required]
        public string Name { get; set; }

        // Mô tả chủ đề
        [Column("Description", TypeName = "VARCHAR(MAX)")]
        public string Description { get; set; }

        // Trạng thái chủ đề
        [Column("Status", TypeName = "VARCHAR(20)")]
        [Required]
        public string Status { get; set; }

        // Tài liệu chủ đề
        [Column("Documents", TypeName = "VARCHAR(MAX)")]
        [Required]
        public string Documents { get; set; }

        // Tệp đính kèm
        [Column("AttachFiles", TypeName = "VARCHAR(MAX)")]
        public string AttachFiles { get; set; }

        // Loại tài liệu
        [Column("TypeDocument", TypeName = "VARCHAR(20)")]
        [Required]
        public string TypeDocument { get; set; }

        // Người đăng chủ đề
        [Column("PosterTopic", TypeName = "VARCHAR(MAX)")]
        [Required]
        public string PosterTopic { get; set; }

        // Ngày tạo
        public DateTime? CreatedAt { get; set; }

        // Ngày cập nhật
        public DateTime? UpdatedAt { get; set; }

        // Ngày xóa
        public DateTime? DeletedAt { get; set; }
    }
}
