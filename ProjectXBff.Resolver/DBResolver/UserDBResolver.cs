using System;
using System.Threading.Tasks;
using ProjectXBff.Resolver.CommonResolver.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ProjectXBff.Resolver.CommonResolver.Models.Users;
using ProjectXBff.Resolver.ConstantResolver;

namespace ProjectXBff.Resolver.DBResolver
{
    public class UserDBResolver : IUserDBResolver
    {

        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;

        public UserDBResolver(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration[key: "ConnectionStrings:DBSettingConnection"]);
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            CreateUserResponse response = new CreateUserResponse();
            try
            {
                Guid id = Guid.NewGuid();
                string query = "Insert Into Users (Id,UserName,Password,Created_at,Designation) value (@Id,@UserName,@Password,@Created_at,@Designation)";

                using (MySqlCommand command = new MySqlCommand(query, _mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue(parameterName: "@Id", id);
                    command.Parameters.AddWithValue(parameterName: "@UserName", request.UserName);
                    command.Parameters.AddWithValue(parameterName: "@Password", request.Password);

                    command.Parameters.AddWithValue(parameterName: "@Created_at", request.Created_at);
                    command.Parameters.AddWithValue(parameterName: "@Designation", request.Designation.ToString());
                    _mySqlConnection.Open();
                    int Status = await command.ExecuteNonQueryAsync();

                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                    }
                    else
                    {
                        string subQuery = string.Empty;
                        if(request.Designation == desig.student)
                        {
                            subQuery = "Insert Into Student (Id,UserName) value (@Id,@UserName)";
                        }
                        else
                        {
                            subQuery = "Insert Into Mentor (Id,UserName) value (@Id,@UserName)";
                        }
                        using (MySqlCommand subCommand = new MySqlCommand(subQuery, _mySqlConnection))
                        {
                            subCommand.CommandType = System.Data.CommandType.Text;
                            subCommand.CommandTimeout = 180;
                            subCommand.Parameters.AddWithValue(parameterName: "@Id", id);
                            subCommand.Parameters.AddWithValue(parameterName: "@UserName", request.UserName);
                            int subStatus = await subCommand.ExecuteNonQueryAsync();
                            if (subStatus <= 0)
                            {
                                response.IsSuccess = false;
                            }
                        }

                        response.IsSuccess = true;
                        response.Id = id;
                    }
                }
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Id = Guid.Empty;
            }
            finally
            {
                _mySqlConnection.Close();
            }

            return response;
        }

        public async Task<EnterUserResponse> EnterUser(EnterUserRequest request)
        {
            EnterUserResponse response = new EnterUserResponse();
            try
            {
                string query = "Select Id,Designation from Users where username=@UserName and password=@Password";

                using (MySqlCommand command = new MySqlCommand(query, _mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue(parameterName: "@UserName", request.UserName);
                    command.Parameters.AddWithValue(parameterName: "@Password", request.Password);
                    _mySqlConnection.Open();

                    using(MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if(reader.HasRows)
                        {
                            var x = await reader.ReadAsync();
                            response.DoesExist = true;
                            response.Designation = (desig)Enum.Parse(typeof(desig), (string)reader[name: "Designation"]);
                            response.Id = new Guid((string)reader[name: "Id"]);
                        }
                        else
                        {
                            response.DoesExist = false;
                            response.Id = Guid.Empty;
                            response.Designation = desig.none;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.DoesExist = false;
                response.Id = Guid.Empty;
                response.Designation = desig.none;
            }
            finally
            {
                _mySqlConnection.Close();

            }
            return response;
        }
    }
}
