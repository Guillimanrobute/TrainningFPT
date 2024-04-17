using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using TrainingFPTCo.Models;

namespace TrainingFPTCo.Models.Queries
{
    public class AccountQuery
    {
        private readonly string _connectionString;

        public AccountQuery(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<AccountViewModel> GetAllUsers()
        {
            List<AccountViewModel> users = new List<AccountViewModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"
                    SELECT u.Id, u.Username, u.Email, u.FullName, u.FirstName, u.LastName, u.Birthday, u.Gender,
                           u.Education, u.ProgrammingLanguage, u.ToeicScore, u.Skills, u.Phone, u.Address, r.Name AS Role
                    FROM Users u
                    INNER JOIN Roles r ON u.RolesId = r.Id";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new AccountViewModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Birthday = Convert.ToDateTime(reader["Birthday"]),
                        Gender = reader["Gender"].ToString(),
                        Education = reader["Education"].ToString(),
                        ProgrammingLanguage = reader["ProgrammingLanguage"].ToString(),
                        TOEICScore = reader["ToeicScore"] != DBNull.Value ? Convert.ToInt32(reader["ToeicScore"]) : (int?)null,
                        Skills = reader["Skills"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Role = reader["Role"].ToString()
                    });
                }
            }

            return users;
        }

        public AccountViewModel GetUserById(int id)
        {
            AccountViewModel user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"
                    SELECT u.Id, u.Username, u.Email, u.FullName, u.FirstName, u.LastName, u.Birthday, u.Gender,
                           u.Education, u.ProgrammingLanguage, u.ToeicScore, u.Skills, u.Phone, u.Address, r.Name AS Role
                    FROM Users u
                    INNER JOIN Roles r ON u.RolesId = r.Id
                    WHERE u.Id = @Id";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new AccountViewModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Birthday = Convert.ToDateTime(reader["Birthday"]),
                        Gender = reader["Gender"].ToString(),
                        Education = reader["Education"].ToString(),
                        ProgrammingLanguage = reader["ProgrammingLanguage"].ToString(),
                        TOEICScore = reader["ToeicScore"] != DBNull.Value ? Convert.ToInt32(reader["ToeicScore"]) : (int?)null,
                        Skills = reader["Skills"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
            }

            return user;
        }

        public void CreateUser(AccountViewModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"
                    INSERT INTO Users (RolesId, Username, Password, ExtraCode, Email, Phone, Address, FullName, FirstName, LastName, Birthday, Gender, Education, ProgramingLanguage, ToeicScore, Skills, IPClient, CreatedAt, UpdatedAt)
                    VALUES (@RolesId, @Username, @Password, @ExtraCode, @Email, @Phone, @Address, @FullName, @FirstName, @LastName, @Birthday, @Gender, @Education, @ProgramingLanguage, @ToeicScore, @Skills, @IPClient, GETDATE(), GETDATE())";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@RolesId", user.RolesId);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@ExtraCode", user.ExtraCode);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@Education", user.Education ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ToeicScore", user.TOEICScore ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Skills", user.Skills ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IPClient", user.IPClient ?? (object)DBNull.Value);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUser(AccountViewModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"
                    UPDATE Users
                    SET RolesId = @RolesId, Username = @Username, Password = @Password, ExtraCode = @ExtraCode, Email = @Email, Phone = @Phone, Address = @Address,
                        FullName = @FullName, FirstName = @FirstName, LastName = @LastName, Birthday = @Birthday, Gender = @Gender, Education = @Education,
                        ProgramingLanguage = @ProgramingLanguage, ToeicScore = @ToeicScore, Skills = @Skills, IPClient = @IPClient, UpdatedAt = GETDATE()
                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@RolesId", user.RolesId);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@ExtraCode", user.ExtraCode);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@Education", user.Education ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ToeicScore", user.TOEICScore ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Skills", user.Skills ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IPClient", user.IPClient ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", user.Id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "DELETE FROM Users WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@Id", userId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
