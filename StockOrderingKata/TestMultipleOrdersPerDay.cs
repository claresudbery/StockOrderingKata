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
    }
}