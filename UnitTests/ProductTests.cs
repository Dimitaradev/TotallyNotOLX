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
    public class ProductTests
    {
        [Test]
        public void ShouldProductEligible()
        {
            Product product = new Product()
            {
                Name = "Ski head",
                Sold = false,
                DatePosted = "07-03-2022",
                SellerId = "5b6244fc-2293-48f5-9432-273e84bfadeb",
            };

            var sut = new ProductValidator();

            var result = sut.CheckProductAligibility(product);

            Assert.That(result, Is.True);
        }
    }
}
