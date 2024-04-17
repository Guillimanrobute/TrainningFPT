using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using TrainingFPTCo.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TrainingFPTCo.Models.Queries
{
    public class TopicQuery
    {
        public TopicDetail GetTopicById(int id)
        {
            TopicDetail topic = null;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sql = "SELECT * FROM [Topics] WHERE [Id] = @Id";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        topic = new TopicDetail
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            CourseId = Convert.ToInt32(reader["CourseId"]),
                            Description = reader["Description"].ToString(),
                            Status = reader["Status"].ToString(),
                            ViewDocuments = reader["Documents"].ToString(),
                            ViewAttachFiles = reader["AttachFiles"].ToString(),
                            ViewPosterTopic = reader["PosterTopic"].ToString(),
                            TypeDocument = reader["TypeDocument"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            DeletedAt = reader["DeletedAt"] != DBNull.Value ? Convert.ToDateTime(reader["DeletedAt"]) : (DateTime?)null
                        };
                    }
                }
                connection.Close();
            }
            return topic;
        }

        public List<TopicDetail> GetAllTopics(string searchString, string status)
        {
            List<TopicDetail> topics = new List<TopicDetail>();
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sql = @"SELECT t.*, c.Name AS CourseName 
               FROM [Topics] t 
               JOIN Courses c ON t.CourseId = c.Id";
                if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(status))
                {
                    sql += " WHERE";
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        sql += $" (t.Name LIKE '%{searchString}%' OR t.Description LIKE '%{searchString}%' OR c.Name LIKE '%{searchString}%')";
                        if (!string.IsNullOrEmpty(status))
                            sql += " AND";
                    }
                    if (!string.IsNullOrEmpty(status))
                    {
                        sql += $" t.Status = '{status}'";
                    }
                }
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TopicDetail detail = new TopicDetail();
                        detail.Id = Convert.ToInt32(reader["Id"]);
                        detail.Name = reader["Name"].ToString();
                        detail.CourseName = reader["CourseName"].ToString();
                        detail.CourseId = Convert.ToInt32(reader["CourseId"]);
                        detail.Description = reader["Description"].ToString();
                        detail.Status = reader["Status"].ToString();
                        detail.ViewDocuments = reader["Documents"].ToString();
                        detail.ViewAttachFiles = reader["AttachFiles"].ToString();
                        detail.ViewPosterTopic = reader["PosterTopic"].ToString();
                        detail.TypeDocument = reader["TypeDocument"].ToString();
                        detail.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                        detail.DeletedAt = reader["DeletedAt"] != DBNull.Value ? Convert.ToDateTime(reader["DeletedAt"]) : (DateTime?)null;
                        topics.Add(detail);
                    }
                }
                connection.Close();
            }
            return topics;
        }

        public int InsertTopic(
            string name,
            int courseId,
            string description,
            string status,
            string documents,
            string posterTopic,
            string attachFiles,
            string typeDocument)
        {
            int idTopic = 0;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sqlQuery = @"INSERT INTO [Topics] ([Name], [CourseId], [Description], [Status], [Documents], [TypeDocument], [PosterTopic], [CreatedAt], [AttachFiles]) 
            VALUES (@Name, @CourseId, @Description, @Status, @Documents, @TypeDocument, @PosterTopic, @CreatedAt, @AttachFiles);
            SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Documents", documents);
                cmd.Parameters.AddWithValue("@PosterTopic", posterTopic);
                cmd.Parameters.AddWithValue("@AttachFiles", attachFiles ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@TypeDocument", typeDocument); // Thêm tham số TypeDocument vào câu truy vấn
                cmd.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                connection.Open();
                idTopic = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return idTopic;
        }


        public bool DeleteTopicById(int id)
        {
            bool deleteResult = false;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sqlQuery = "DELETE FROM [Topics] WHERE [Id] = @Id";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                deleteResult = rowsAffected > 0;
                connection.Close();
            }
            return deleteResult;
        }
        public bool UpdateTopic(
            int id,
            string name,
            int courseId,
            string description,
            string status,
            string documents,
            string posterTopic,
            string attachFiles,
            string typeDocument)
        {
            bool updateResult = false;
            using (SqlConnection connection = Database.GetSqlConnection())
            {
                string sqlQuery = @"UPDATE [Topics] SET 
            [Name] = @Name,
            [CourseId] = @CourseId,
            [Description] = @Description,
            [Status] = @Status,
            [Documents] = @Documents,
            [TypeDocument] = @TypeDocument,
            [PosterTopic] = @PosterTopic,
            [AttachFiles] = @AttachFiles
            WHERE [Id] = @Id";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Documents", documents);
                cmd.Parameters.AddWithValue("@TypeDocument", typeDocument);
                cmd.Parameters.AddWithValue("@PosterTopic", posterTopic);
                cmd.Parameters.AddWithValue("@AttachFiles", attachFiles ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                updateResult = rowsAffected > 0;
                connection.Close();
            }
            return updateResult;
        }

    }
}