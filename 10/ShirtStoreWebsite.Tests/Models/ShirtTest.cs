using ShirtStoreWebsite.Models;

namespace ShirtStoreWebsite.Tests.Models
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsGetFormattedTaxedPriceReturnsCorrectly()
        {
            var shirt = new Shirt { Price = 10F, Tax = 1.2F };
            var taxedPrice = shirt.GetFormattedTaxedPrice();
            Assert.AreEqual(taxedPrice, "$12.00");
        }
    }
}