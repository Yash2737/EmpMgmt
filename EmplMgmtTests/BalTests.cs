using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using NUnit.Framework;
using BusinessAccessLayer;
using Helper;

namespace EmplMgmtTests
{
    [TestFixture]
    public class BalTests
    {
        private EmployeeBal _sut;
        [SetUp]
        public void setup()
        {
            _sut = new EmployeeBal();
        }

        [Test]
        public void sendmail()
        {
            _sut.SendEmail(new User
            {
                EmailPersonal = "sachin.threepin@gmail.com",
                Password = "helloworld",
                FirstName = "Yash"
            });
        }

        [Test]
        public void ResetPass()
        {
            _sut.ResetPasswordEmail("yash.threepin@gmail.com", "Yash", "yashleo123");
        }
    }
}
