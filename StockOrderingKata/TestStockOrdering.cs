using System;
using System.Linq;
using NUnit.Framework;

namespace StockOrderingKata
{
    [TestFixture]
    public class TestStockOrderingForStockA
    {
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(6)]
        public void When_six_or_fewer_A_items_are_ordered_Then_one_A_pallet_is_needed(int numItems)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("A", numItems);

            // Assert
            Assert.AreEqual(dispatchRequest.Consignment, new string[] {"A"});
        }

        [TestCase(7)]
        [TestCase(9)]
        [TestCase(12)]
        public void When_between_7_and_12_A_items_are_ordered_Then_two_A_Pallets_are_needed(int numItems)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("A", numItems);

            // Assert
            Assert.AreEqual(new string[] { "A", "A" }, dispatchRequest.Consignment);
        }

        [TestCase(7, 2)]
        [TestCase(23, 4)]
        [TestCase(123, 21)]
        public void When_A_items_are_ordered_Then_the_number_of_pallets_is_one_pallet_for_every_six_items_or_fewer(int numItems, int numExpectedPallets)
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("A", numItems);

            // Assert
            Assert.AreEqual(dispatchRequest.Consignment.Length, numExpectedPallets);
        }

        [Test]
        public void When_only_A_items_are_ordered_Then_all_pallets_should_be_A_pallets()
        {
            // Act
            DispatchRequest dispatchRequest = new Warehouse().OrderStock("A", 60);

            // Assert
            Assert.IsTrue(dispatchRequest.Consignment.ToList().All(x => x == "A"));
        }
    }
}
