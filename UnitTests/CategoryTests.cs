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
    public class CategoryTests
    {
        [Test]
        public void ShouldCategoryEligible()
        {
            Category category = new Category()
            {
                Name = "Cars",
                Description = "djskl"
            };

            var sut = new CategoryValidator();

            var result = sut.CheckCategoryAligibility(category);

            Assert.That(result, Is.True);
        }
    }
}
