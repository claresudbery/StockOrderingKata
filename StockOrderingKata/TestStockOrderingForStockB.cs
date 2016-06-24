using System.Linq;
using NUnit.Framework;

namespace StockOrderingKata
{
    [TestFixture]
    public class TestStockOrderingForStockB
    {
        [TestCase(1)]
        [TestCase(9)]
        [TestCase(10)]
        public void When_ten_or_fewer_B_items_are_ordered_Then_one_B_pallet_is_needed(int numItems)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("B", numItems);

            // Assert
            Assert.AreEqual(warehouse.ReconcileOrders()[0].Consignment, new string[] { "B" });
        }

        [TestCase(11)]
        [TestCase(13)]
        [TestCase(20)]
        public void When_between_11_and_20_B_items_are_ordered_Then_two_B_Pallets_are_needed(int numItems)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("B", numItems);

            // Assert
            Assert.AreEqual(new string[] { "B", "B" }, warehouse.ReconcileOrders()[0].Consignment);
        }

        [TestCase(7, 1)]
        [TestCase(23, 3)]
        [TestCase(123, 13)]
        public void When_B_items_are_ordered_Then_the_number_of_pallets_is_one_pallet_for_every_ten_items_or_fewer(int numItems, int numExpectedPallets)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("B", numItems);

            // Assert
            Assert.AreEqual(warehouse.ReconcileOrders()[0].Consignment.Length, numExpectedPallets);
        }

        [Test]
        public void When_only_B_items_are_ordered_Then_all_pallets_should_be_B_pallets()
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("B", 100);

            // Assert
            Assert.IsTrue(warehouse.ReconcileOrders()[0].Consignment.ToList().All(x => x == "B"));
        }
    }
}