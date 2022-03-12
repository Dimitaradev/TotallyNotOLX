using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotallyNotOLX.Models;

namespace UnitTests
{
    [TestFixture]
    public class ApplicationUserTests
    {
        [Test]
        public void ShouldUserEligible()
        {
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = "John",
                LastName = "Sean"
            };

            var sut = new ApplicationUserValidator();

            var result = sut.CheckUserAligibility(user);

            Assert.That(result, Is.True);
        }
    }
}
