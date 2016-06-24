using System.Linq;
using NUnit.Framework;

namespace StockOrderingKata
{
    [TestFixture]
    public class TestStockOrderingForStockD
    {
        [TestCase(1)]
        [TestCase(39)]
        [TestCase(48)]
        public void When_48_or_fewer_D_items_are_ordered_Then_one_D_pallet_is_needed(int numItems)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("D", numItems);

            // Assert
            Assert.AreEqual(dispatchRequest.Consignment, new string[] { "D" });
        }

        [TestCase(49)]
        [TestCase(73)]
        [TestCase(96)]
        public void When_between_49_and_96_D_items_are_ordered_Then_two_D_Pallets_are_needed(int numItems)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("D", numItems);

            // Assert
            Assert.AreEqual(new string[] { "D", "D" }, dispatchRequest.Consignment);
        }

        [TestCase(37, 1)]
        [TestCase(100, 3)]
        [TestCase(500, 11)]
        public void When_D_items_are_ordered_Then_the_number_of_pallets_is_one_pallet_for_every_48_items_or_fewer(int numItems, int numExpectedPallets)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("D", numItems);

            // Assert
            Assert.AreEqual(dispatchRequest.Consignment.Length, numExpectedPallets);
        }

        [Test]
        public void When_only_D_items_are_ordered_Then_all_pallets_should_be_C_pallets()
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("D", 100);

            // Assert
            Assert.IsTrue(dispatchRequest.Consignment.ToList().All(x => x == "D"));
        }
    }
}