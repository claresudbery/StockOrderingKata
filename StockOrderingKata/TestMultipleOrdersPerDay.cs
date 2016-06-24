using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StockOrderingKata
{
    [TestFixture]
    public class TestMultipleOrdersPerDay
    {
        [Test]
        public void When_two_orders_are_made_in_one_day_Then_they_are_both_processed_together()
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("B", 10);
            warehouse.OrderStock("B", 20);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(dispatchRequests[0].Consignment, new string[] { "B", "B", "B" });
        }

        [Test]
        public void When_three_orders_are_made_in_one_day_Then_they_are_all_processed_together()
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("B", 10);
            warehouse.OrderStock("B", 20);
            warehouse.OrderStock("B", 18);

            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(dispatchRequests[0].Consignment, new string[] { "B", "B", "B", "B", "B" });
        }

        [Test]
        public void When_five_orders_are_made_in_one_day_Then_they_are_all_processed_together()
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("A", 10);
            warehouse.OrderStock("A", 20);
            warehouse.OrderStock("A", 18);
            warehouse.OrderStock("A", 5);
            warehouse.OrderStock("A", 1);

            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(new string[] { "A", "A", "A", "A", "A", "A", "A", "A", "A" }, dispatchRequests[0].Consignment);
        }

        [Test]
        public void When_day_ends_Then_no_orders_remain()
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("B", 10);
            warehouse.OrderStock("B", 20);
            warehouse.EndOfDay();

            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(0, dispatchRequests.Count);
        }

        [Test]
        public void When_new_day_ends_Then_only_orders_made_during_that_day_are_present()
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("A", 10);
            warehouse.OrderStock("A", 20);
            warehouse.EndOfDay();
            warehouse.OrderStock("B", 10);
            warehouse.OrderStock("B", 20);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(new string[] { "B", "B", "B" }, dispatchRequests[0].Consignment);
            Assert.AreEqual(1, dispatchRequests.Count);
        }
    }
}