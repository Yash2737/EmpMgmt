using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using DataAccessLayer;
using Helper;
using EmployeeManagement.Models;

namespace BusinessAccessLayer
{
    public class EmployeeBal
    {
        private EmployeeData _dal;
        private static Random random = new Random();

        public EmployeeBal()
        {
            _dal = new EmployeeData();
        }

        public List<User> GetAllEmployees()
        {
            var list = _dal.GetAllEmployees();

            return list;
        }

        public Details GetEmployeeDetails(int? id)
        {
            var empl = _dal.GetEmployeeDetails(id);

            return empl;
        }

        public User AddEmployee(User user)
        {
            var temp = new User();
            temp.UniqueEmpCode = GenerateUniqueEmpCode();
            if (!string.IsNullOrEmpty(temp.UniqueEmpCode))
            {
                user.UniqueEmpCode = temp.UniqueEmpCode;
                var pass = RandomPass();
                user.Password = pass;
                var res = _dal.AddEmployee(user);

                if (res)
                {
                    return user;
                }
                else
                {
                    return new User();
                }
            }
            else
            {
                throw new Exception("Unique emp code not generated");
            }
        }

        public bool DeleteEmployee(int id)
        {
            var res = _dal.DeleteEmployee(id);

            if (res)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static string RandomPass()
        {
            int length = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateUniqueEmpCode()
        {
            //Get previous emp code
            var lastclass = _dal.GetLastEmpCode();
            const string pref = "TPI";
            var last = lastclass.Id;
            string unique = null;

            if (last != 0)
            {
                last++;
                unique = pref + "00" + last;
                return unique;
            }
            else
            {
                throw new Exception("Error");
            }

        }

        public List<NavItems> GetMenuInfo(int id)
        {
            var res = _dal.GetMenuAccToUser(id);
            List<NavItems> hrItem = res.Where(x => x.ParentId == 0).ToList();

            Action<NavItems> SetChildren = null;
            SetChildren = parent =>
            {
                parent.ChildItems = res
                    .Where(childItem => childItem.ParentId == parent.Id)
                    .ToList();

                //Recursively call the SetChildren method for each child.
                parent.ChildItems
                    .ForEach(SetChildren);
            };

            hrItem.ForEach(SetChildren);
            var logout = hrItem.First();
            hrItem.Remove(logout);
            return hrItem;

        }

        public User CheckPass(Login user)
        {
            var res = _dal.CheckPass(user);

            return res;
        }

        public bool UpdatePassword(Password user)
        {
            var res = _dal.UpdatePassword(user);

            return res;
        }

        public void SendEmail(User user)
        {
            var fromAddress = new MailAddress("yash.threepin@gmail.com", "Threepin India");
            var toAddress = new MailAddress(user.EmailPersonal, user.FirstName);
            const string fromPassword = "yashleo123";
            const string subject = "Employee Management - Username And Password";

            var Body =
                "Your Password for Employee Management Application is generated successfully\n Your Username to login is: -"
                + user.EmailPersonal + "\nPassword is:- " + user.Password;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = Body
            })

                smtp.Send(message);




        }

        public void ResetPasswordEmail(string email, string name, string password)
        {
            try
            {
                var fromAddress = new MailAddress("yash.threepin@gmail.com", "Threepin India");
                var toAddress = new MailAddress(email, name);
                const string fromPassword = "yashleo123";
                const string subject = "Employee Management - Password Reset";

                var Body =
                    "Hello " + name + "Your Password for Employee Management Application is" + password;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = Body
                })

                    smtp.Send(message);


            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateEmployee(User employee)
        {
            var res = _dal.UpdateEmployee(employee);
            return res;
        }

        public bool UpdateBank(BankInfo bank)
        {
            var ress = _dal.UpdateBankInfo(bank);
            return ress;
        }

        public User GetUserFromEmail(string user)
        {
            var res = _dal.GetUserFromEmail(user);
            return res;
        }

        public Edit GetEditInfo(int id)
        {
            var res = _dal.GetEditInfo(id);
            return res;
        }

        public bool UpdateEditInfo(Edit edit)
        {
            var res = _dal.UpdateEditInfo(edit);
            return res;
        }
    }
}















