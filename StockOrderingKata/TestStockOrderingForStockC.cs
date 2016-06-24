using System.Linq;
using NUnit.Framework;

namespace StockOrderingKata
{
    [TestFixture]
    public class TestStockOrderingForStockC
    {
        [TestCase(1)]
        [TestCase(9)]
        [TestCase(20)]
        public void When_twenty_or_fewer_C_items_are_ordered_Then_one_C_pallet_is_needed(int numItems)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("C", numItems);

            // Assert
            Assert.AreEqual(dispatchRequest.Consignment, new string[] { "C" });
        }

        [TestCase(21)]
        [TestCase(33)]
        [TestCase(40)]
        public void When_between_21_and_40_C_items_are_ordered_Then_two_C_Pallets_are_needed(int numItems)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("C", numItems);

            // Assert
            Assert.AreEqual(new string[] { "C", "C" }, dispatchRequest.Consignment);
        }

        [TestCase(7, 1)]
        [TestCase(73, 4)]
        [TestCase(260, 13)]
        public void When_C_items_are_ordered_Then_the_number_of_pallets_is_one_pallet_for_every_twenty_items_or_fewer(int numItems, int numExpectedPallets)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("C", numItems);

            // Assert
            Assert.AreEqual(dispatchRequest.Consignment.Length, numExpectedPallets);
        }

        [Test]
        public void When_only_C_items_are_ordered_Then_all_pallets_should_be_C_pallets()
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("C", 100);

            // Assert
            Assert.IsTrue(dispatchRequest.Consignment.ToList().All(x => x == "C"));
        }
    }
}