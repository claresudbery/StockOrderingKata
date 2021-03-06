﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StockOrderingKata
{
    [TestFixture]
    public class TestLorryTypes
    {
        [TestCase("Transit", false)]
        [TestCase("Modified Transit", true)]
        [TestCase("Box Van", false)]
        [TestCase("Lorry", true)]
        public void Should_correctly_determine_refrigeratedness_for_each_lorry_type(string lorryType, bool expectedRefrigeratedness)
        {
            // Act
            bool refrigeratedness = new Warehouse().IsLorryRefrigerated(lorryType);

            // Assert
            Assert.AreEqual(expectedRefrigeratedness, refrigeratedness);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void When_small_quantity_of_stock_is_refrigerated_then_refrigerated_lorry_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock(stockCode, 5);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(true, warehouse.IsLorryRefrigerated(dispatchRequests[0].LorryType));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void When_small_quantity_of_stock_is_refrigerated_then_modified_transit_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock(stockCode, 5);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual("Modified Transit", dispatchRequests[0].LorryType);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void When_large_quantity_of_stock_is_refrigerated_then_refrigerated_lorry_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock(stockCode, 50);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(true, warehouse.IsLorryRefrigerated(dispatchRequests[0].LorryType));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void When_large_quantity_of_stock_is_refrigerated_then_lorry_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock(stockCode, 50);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual("Lorry", dispatchRequests[0].LorryType);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void When_some_stock_is_refrigerated_and_some_is_not_for_small_order_Then_refrigerated_lorry_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("C", 6);
            warehouse.OrderStock(stockCode, 6);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(true, warehouse.IsLorryRefrigerated(dispatchRequests[0].LorryType));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void When_some_stock_is_refrigerated_and_some_is_not_for_small_order_Then_modified_transit_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("C", 6);
            warehouse.OrderStock(stockCode, 6);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual("Modified Transit", dispatchRequests[0].LorryType);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void When_some_stock_is_refrigerated_and_some_is_not_for_large_order_Then_refrigerated_lorry_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("C", 5);
            warehouse.OrderStock(stockCode, 50);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(true, warehouse.IsLorryRefrigerated(dispatchRequests[0].LorryType));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void When_some_stock_is_refrigerated_and_some_is_not_for_large_order_Then_lorry_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock("C", 5);
            warehouse.OrderStock(stockCode, 50);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual("Lorry", dispatchRequests[0].LorryType);
        }

        [TestCase("C")]
        [TestCase("D")]
        public void When_stock_is_not_refrigerated_for_small_order_Then_non_refrigerated_lorry_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock(stockCode, 6);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(false, warehouse.IsLorryRefrigerated(dispatchRequests[0].LorryType));
        }

        [TestCase("C")]
        [TestCase("D")]
        public void When_stock_is_not_refrigerated_for_small_order_Then_transit_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock(stockCode, 6);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual("Transit", dispatchRequests[0].LorryType);
        }

        [TestCase("C")]
        [TestCase("D")]
        public void When_stock_is_not_refrigerated_for_large_order_Then_non_refrigerated_lorry_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock(stockCode, 300);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual(false, warehouse.IsLorryRefrigerated(dispatchRequests[0].LorryType));
        }

        [TestCase("C")]
        [TestCase("D")]
        public void When_stock_is_not_refrigerated_for_large_order_Then_box_van_is_used(string stockCode)
        {
            // Arrange
            var warehouse = new Warehouse();

            // Act
            warehouse.OrderStock(stockCode, 300);
            List<DispatchRequest> dispatchRequests = warehouse.ReconcileOrders();

            // Assert
            Assert.AreEqual("Box Van", dispatchRequests[0].LorryType);
        }
    }
}