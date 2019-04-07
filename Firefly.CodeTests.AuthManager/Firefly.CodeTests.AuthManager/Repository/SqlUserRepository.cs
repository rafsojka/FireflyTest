using Firefly.CodeTests.AuthManager.Exceptions;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Firefly.CodeTests.AuthManager.Repository
{
    public class SqlUserRepository : IUserRepository
    {
        
        private readonly string connectionString;

        public SqlUserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AuthenticateUser(User user)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("AuthenticateUser", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pUserName", user.UserName));
                    cmd.Parameters.Add(new SqlParameter("@pPassword", user.Password));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 50001)
                {
                    throw new InvalidUserNameException($"UserName {user.UserName} is not valid.", sqlEx);
                }
                if (sqlEx.Number == 50002)
                {
                    throw new InvalidPasswordException($"Password is not valid.", sqlEx);
                }

                throw new UserRepositoryException("AuthenticateUser", sqlEx);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("AuthenticateUser", ex);
            }
        }

        public void CreateUser(User user)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("CreateUser", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pUserName", user.UserName));
                    cmd.Parameters.Add(new SqlParameter("@pPassword", user.Password));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                if(sqlEx.Number == 2601 || sqlEx.Number == 2627)
                {
                    throw new UserNameAlreadyExistsException($"UserName {user.UserName} alredy exists in database.");
                }

                throw new UserRepositoryException("CreateUser", sqlEx);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("CreateUser", ex);
            }
        }

        public User GetUser(string userName)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("GetUser", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pUserName", userName));
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            return new User(reader["UserName"].ToString(), reader["Password"].ToString());
                        }
                        return null;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 50001)
                {
                    throw new InvalidUserNameException($"UserName {userName} is not valid.", sqlEx);
                }

                throw new UserRepositoryException("GetUser", sqlEx);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("GetUser", ex);
            }
        }
    }
}
