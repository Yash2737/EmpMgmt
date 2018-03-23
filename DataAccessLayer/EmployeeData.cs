using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EmployeeManagement.Models;
using Helper;

namespace DataAccessLayer
{
    public class EmployeeData
    {
        public static string a = @"Data Source=MYNEWPC\SQLEXPRESS;Initial Catalog=EmployeeInfo;Integrated Security=True;Pooling=False";
        private EmployeeContext emp;
        public EmployeeData()
        {
            emp = new EmployeeContext();
        }

        public List<User> GetAllEmployees()
        {
            var list = new List<User>();

            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"SELECT
  EmployeeInfo.dbo.tbluser.Id,
  EmployeeInfo.dbo.tbluser.FirstName,
  EmployeeInfo.dbo.tbluser.LastName,
  EmployeeInfo.dbo.tbluser.RoleId,
EmployeeInfo.dbo.tblrole.Role,
  EmployeeInfo.dbo.tbluser.DateOfJoining,
  EmployeeInfo.dbo.tbluser.UniqueEmpCode
FROM
  EmployeeInfo.dbo.tbluser
  INNER JOIN EmployeeInfo.dbo.tblrole ON EmployeeInfo.dbo.tbluser.RoleId = EmployeeInfo.dbo.tblrole.Id where tbluser.Status = '1'", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var temp = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                        Role = reader.GetString(reader.GetOrdinal("Role")),
                        DateofJoining = reader.GetDateTime(reader.GetOrdinal("DateOfJoining")),
                        UniqueEmpCode = reader.GetString(reader.GetOrdinal("UniqueEmpCode"))
                    };
                    list.Add(temp);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }

            return list;
        }

        public Details GetEmployeeDetails(int? id)
        {
            var user = new Details();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"SELECT
  EmployeeInfo.dbo.tbluser.Id,
  EmployeeInfo.dbo.tbluser.FirstName,
  EmployeeInfo.dbo.tbluser.LastName,
  EmployeeInfo.dbo.tbluser.Gender,
  EmployeeInfo.dbo.tbluser.DateOfBirth,
  EmployeeInfo.dbo.tbluser.DateOfJoining,
  EmployeeInfo.dbo.tblrole.Role,
  EmployeeInfo.dbo.tbluser.RoleId,
  EmployeeInfo.dbo.tbluser.UniqueEmpCode,
  EmployeeInfo.dbo.tbluser.EmailOfficial,
  EmployeeInfo.dbo.tbluser.EmailPersonal,
  EmployeeInfo.dbo.tbluser.PhoneNoOfficial,
  EmployeeInfo.dbo.tbluser.PhoneNoPersonal,
  EmployeeInfo.dbo.tbluser.PanNumber,
  EmployeeInfo.dbo.tbluser.PanDetailsId,
  EmployeeInfo.dbo.tbluser.AadharNumber,
  EmployeeInfo.dbo.tbluser.AadharDetailsId,
  EmployeeInfo.dbo.tbluser.Qualification,
  EmployeeInfo.dbo.tbluser.Status,
  EmployeeInfo.dbo.tbluser.IsPasswordReset,
  EmployeeInfo.dbo.tblbankinfo.UserId,
  EmployeeInfo.dbo.tblbankinfo.BankName,
  EmployeeInfo.dbo.tblbankinfo.IfscCode,
  EmployeeInfo.dbo.tblbankinfo.AccountNo,
  EmployeeInfo.dbo.tblbankinfo.AccountType,
  EmployeeInfo.dbo.tblbankinfo.City,
  EmployeeInfo.dbo.tblbankinfo.State,
  EmployeeInfo.dbo.tblbankinfo.PinCode,
  EmployeeInfo.dbo.tblbankinfo.UPICode,
  EmployeeInfo.dbo.tblbankinfo.Status AS Bank_Status
FROM
  EmployeeInfo.dbo.tblbankinfo
  RIGHT JOIN EmployeeInfo.dbo.tbluser ON EmployeeInfo.dbo.tblbankinfo.UserId = EmployeeInfo.dbo.tbluser.Id
  INNER JOIN EmployeeInfo.dbo.tblrole ON EmployeeInfo.dbo.tbluser.RoleId = EmployeeInfo.dbo.tblrole.Id
WHERE
  EmployeeInfo.dbo.tbluser.Id = @id AND
  EmployeeInfo.dbo.tbluser.Status = 1", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var tt = new Details
                    {
                        User = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? "None" : reader.GetString(reader.GetOrdinal("Gender")),
                            Role = reader.GetString(reader.GetOrdinal("Role")),
                            RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                            DateofBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            DateofJoining = reader.IsDBNull(reader.GetOrdinal("DateofJoining")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateOfJoining")),
                            UniqueEmpCode = reader.GetString(reader.GetOrdinal("UniqueEmpCode")),
                            EmailOfficial = reader.IsDBNull(reader.GetOrdinal("EmailOfficial")) ? "None" : reader.GetString(reader.GetOrdinal("EmailOfficial")),
                            EmailPersonal = reader.IsDBNull(reader.GetOrdinal("EmailPersonal")) ? "None" : reader.GetString(reader.GetOrdinal("EmailPersonal")),
                            PhoneNoOfficial = reader.IsDBNull(reader.GetOrdinal("PhoneNoOfficial")) ? "None" : reader.GetString(reader.GetOrdinal("PhoneNoOfficial")),
                            PhoneNoPersonal = reader.IsDBNull(reader.GetOrdinal("PhoneNoPersonal")) ? "None" : reader.GetString(reader.GetOrdinal("PhoneNoPersonal")),
                            PanNumber = reader.IsDBNull(reader.GetOrdinal("PanNumber")) ? "None" : reader.GetString(reader.GetOrdinal("PanNumber")),
                            PanDetailsId = reader.IsDBNull(reader.GetOrdinal("PanDetailsId")) ? "None" : reader.GetString(reader.GetOrdinal("PanDetailsId")),
                            AadharNumber = reader.IsDBNull(reader.GetOrdinal("AadharNumber")) ? "None" : reader.GetString(reader.GetOrdinal("AadharNumber")),
                            AadharDetailsId = reader.IsDBNull(reader.GetOrdinal("AadharDetailsId")) ? "None" : reader.GetString(reader.GetOrdinal("AadharDetailsId")),
                            Qualification = reader.IsDBNull(reader.GetOrdinal("Qualification")) ? "None" : reader.GetString(reader.GetOrdinal("Qualification")),
                            Status = reader.GetInt32(reader.GetOrdinal("Status"))
                        },
                        BankInfo = new BankInfo
                        {
                            UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId")),
                            AccountNo = reader.IsDBNull(reader.GetOrdinal("AccountNo")) ? "None" : reader.GetString(reader.GetOrdinal("AccountNo")),
                            AccountType = reader.IsDBNull(reader.GetOrdinal("AccountType")) ? "None" : reader.GetString(reader.GetOrdinal("AccountType")),
                            BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? "None" : reader.GetString(reader.GetOrdinal("BankName")),
                            IfscCode = reader.IsDBNull(reader.GetOrdinal("IfscCode")) ? "None" : reader.GetString(reader.GetOrdinal("IfscCode")),
                            City = reader.IsDBNull(reader.GetOrdinal("City")) ? "None" : reader.GetString(reader.GetOrdinal("City")),
                            State = reader.IsDBNull(reader.GetOrdinal("State")) ? "None" : reader.GetString(reader.GetOrdinal("State")),
                            PinCode = reader.IsDBNull(reader.GetOrdinal("PinCode")) ? "None" : reader.GetString(reader.GetOrdinal("PinCode")),
                            UPICode = reader.IsDBNull(reader.GetOrdinal("UPICode")) ? "None" : reader.GetString(reader.GetOrdinal("UPICode")),
                            Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? 0 : reader.GetInt32(reader.GetOrdinal("Status")),

                        }
                    };
                    user = tt;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return user;
        }

        public bool AddEmployee(User user)
        {
            //if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Gender) || string.IsNullOrEmpty(user.City))
            //{
            //    return false;
            //}
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"insert into dbo.tbluser (FirstName,LastName,Gender,DateofBirth,DateofJoining,RoleId,EmailPersonal,Status,Password,UniqueEmpCode) 
                values (@fname,@lname,@gender,@dob,@doj,@role,@email,@status,@pwd,@empcode);", conn);
                cmd.Parameters.AddWithValue("@fname", user.FirstName);
                cmd.Parameters.AddWithValue("@lname", user.LastName);
                cmd.Parameters.AddWithValue("@gender", user.Gender);
                cmd.Parameters.AddWithValue("@dob", user.DateofBirth);
                cmd.Parameters.AddWithValue("@doj", user.DateofJoining);
                cmd.Parameters.AddWithValue("@role", user.RoleId);
                cmd.Parameters.AddWithValue("@email", user.EmailPersonal);
                cmd.Parameters.AddWithValue("@status", user.Status);
                cmd.Parameters.AddWithValue("@pwd", user.Password);
                cmd.Parameters.AddWithValue("@empcode", user.UniqueEmpCode);
                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DeleteEmployee(int id)
        {
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"update tbluser set tbluser.Status = '0' where tbluser.Id = @id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public User GetLastEmpCode()
        {
            var user = new User();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"select top 1 FirstName,UniqueEmpCode,Id from tbluser order by Id desc", conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var temp = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        UniqueEmpCode = reader.GetString(reader.GetOrdinal("UniqueEmpCode"))
                    };
                    user = temp;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return user;
        }

        public List<NavItems> GetMenuAccToUser(int id)
        {
            var nav = new List<NavItems>();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"SELECT
  tblnavigation.Id,
  tblnavigation.ParentId,
  tblnavigation.Action,
  tblnavigation.Name,
  tblnavigation.Controller
FROM
  tblnavigation
  INNER JOIN tblnavrole ON tblnavrole.NavigationId = tblnavigation.Id
  INNER JOIN tblrole ON tblnavrole.RoleId = tblrole.Id
  INNER JOIN tbluser ON tbluser.RoleId = tblrole.Id
WHERE
  tbluser.Id = @id AND tbluser.Status = '1' order by tblnavigation.Sequence asc", conn);

                cmd.Parameters.AddWithValue("@id", id);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var temp = new NavItems
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Action = reader.IsDBNull(reader.GetOrdinal("Action")) ? string.Empty : reader.GetString(reader.GetOrdinal("Action")),
                        Controller = reader.IsDBNull(reader.GetOrdinal("Controller")) ? string.Empty : reader.GetString(reader.GetOrdinal("Controller")),
                        DisplayName = reader.GetString(reader.GetOrdinal("Name")),
                        ParentId = reader.IsDBNull(reader.GetOrdinal("ParentId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ParentId"))
                    };

                    nav.Add(temp);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return nav;


        }

        public User CheckPass(Login user)
        {
            var temp = new User();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"Select Id,Firstname,RoleId,IsPasswordReset from tbluser where tbluser.Status = '1'
                and (tbluser.EmailOfficial = @email or tbluser.EmailPersonal = @email or tbluser.PhoneNoOfficial = @email or tbluser.PhoneNoPersonal = @email) 
                and tbluser.Password = @pass", conn);

                cmd.Parameters.AddWithValue("@email", user.Username);
                cmd.Parameters.AddWithValue("@pass", user.Password);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var userr = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                        IsPasswordReset = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("IsPasswordReset")))
                    };
                    temp = userr;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return temp;
        }

        public Edit GetEditInfo(int id)
        {
            var temp = new Edit();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"Select Id,Firstname,LastName,EmailPersonal,DateofBirth,DateofJoining,Gender,Status,RoleId from tbluser where tbluser.Status = '1'
                and tbluser.Id = @id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var userr = new Edit
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        EmailPersonal = reader.GetString(reader.GetOrdinal("EmailPersonal")),
                        DateofBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        DateofJoining = reader.IsDBNull(reader.GetOrdinal("DateofJoining")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateOfJoining")),
                        Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? "None" : reader.GetString(reader.GetOrdinal("Gender")),
                        Status = reader.GetInt32(reader.GetOrdinal("Status")),
                        RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),

                    };
                    temp = userr;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return temp;

        }

        public bool UpdateEditInfo(Edit edit)
        {
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"update dbo.tbluser set FirstName = @fname,LastName = @lname,Gender = @gender,DateofBirth = @dob,DateofJoining = @doj,RoleId = @role,EmailPersonal = @email,Status = @status where tbluser.Id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", edit.Id);
                cmd.Parameters.AddWithValue("@fname", edit.FirstName);
                cmd.Parameters.AddWithValue("@lname", edit.LastName);
                cmd.Parameters.AddWithValue("@gender", edit.Gender);
                cmd.Parameters.AddWithValue("@dob", edit.DateofBirth);
                cmd.Parameters.AddWithValue("@doj", edit.DateofJoining);
                cmd.Parameters.AddWithValue("@role", edit.RoleId);
                cmd.Parameters.AddWithValue("@email", edit.EmailPersonal);
                cmd.Parameters.AddWithValue("@status", edit.Status);
                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public User GetUserFromEmail(string user)
        {
            var temp = new User();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"Select Id,Firstname,Password,RoleId from tbluser where tbluser.Status = '1'
and tbluser.EmailOfficial = @email or tbluser.EmailPersonal = @email or tbluser.PhoneNoOfficial = @email or tbluser.PhoneNoPersonal = @email;", conn);

                cmd.Parameters.AddWithValue("@email", user);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var userr = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        Password = reader.GetString(reader.GetOrdinal("Password")),
                        RoleId = reader.GetInt32(reader.GetOrdinal("RoleId"))
                    };
                    temp = userr;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return temp;
        }


        public bool UpdatePassword(Password user)
        {
            var temp = new Password();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"update tbluser set tbluser.Password = @new,tbluser.IsPasswordReset = '1' where tbluser.Password = @old and tbluser.Id = @id ", conn);

                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@new", user.NewPass);
                cmd.Parameters.AddWithValue("@old", user.OldPass);

                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UpdateEmployee(User user)
        {
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"update dbo.tbluser set FirstName = @fname,LastName = @lname,Gender=@gender,DateofBirth=@dob,DateofJoining =@doj
                ,RoleId = @role,EmailPersonal = @emailp,EmailOfficial = @emailoff,Status = @status,UniqueEmpCode = @empcode,PhoneNoOfficial = @phoneoff,
                 PhoneNoPersonal = @phoneper,PanNumber = @pan,AadharNumber = @aadhar where tbluser.Id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@fname", user.FirstName);
                cmd.Parameters.AddWithValue("@lname", user.LastName);
                cmd.Parameters.AddWithValue("@gender", user.Gender);
                cmd.Parameters.AddWithValue("@dob", user.DateofBirth);
                cmd.Parameters.AddWithValue("@doj", user.DateofJoining);
                cmd.Parameters.AddWithValue("@phoneoff", user.PhoneNoOfficial);
                cmd.Parameters.AddWithValue("@phoneper", user.PhoneNoPersonal);
                cmd.Parameters.AddWithValue("@role", user.RoleId);
                cmd.Parameters.AddWithValue("@emailoff", user.EmailOfficial);
                cmd.Parameters.AddWithValue("@emailp", user.EmailPersonal);
                cmd.Parameters.AddWithValue("@status", user.Status);
                cmd.Parameters.AddWithValue("@empcode", user.UniqueEmpCode);
                cmd.Parameters.AddWithValue("@pan", user.PanNumber);
                cmd.Parameters.AddWithValue("@aadhar", user.AadharNumber);
                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UpdateBankInfo(BankInfo bank)
        {
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"update dbo.tblbankinfo set BankName = @bankname,IfscCode = @ifsc,AccountNo=@accno,AccountType=@acctype,City=@city
                ,State = @state,PinCode = @pin,UPICode = @upi where UserId = @id;", conn);
                cmd.Parameters.AddWithValue("@id", bank.UserId);
                cmd.Parameters.AddWithValue("@bankname", bank.BankName);
                cmd.Parameters.AddWithValue("@ifsc", bank.IfscCode);
                cmd.Parameters.AddWithValue("@accno", bank.AccountNo);
                cmd.Parameters.AddWithValue("@acctype", bank.AccountType);
                cmd.Parameters.AddWithValue("@city", bank.City);
                cmd.Parameters.AddWithValue("@state", bank.State);
                cmd.Parameters.AddWithValue("@pin", bank.PinCode);
                cmd.Parameters.AddWithValue("@upi", bank.UPICode);
                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
