using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;

namespace StockOrderingKata
{
    public class Warehouse
    {
        private List<StockOrder> _ordersForToday = new List<StockOrder>();

        public void OrderStock(string stockCode, int numUnits)
        {
            NoteOrder(stockCode, numUnits);
        }

        private void NoteOrder(string stockCode, int numUnits)
        {
            _ordersForToday.Add(new StockOrder {StockCode = stockCode, NumUnits = numUnits});
        }

        private DispatchRequest CreateDispatchRequest(string stockCode, int numUnits)
        {
            int numPallets = PrepareOnePalletForEveryBatchOfUnits(numUnits, GetBatchSize(stockCode));

            var consignmentAsList = GetConsignmentAsList(stockCode, numPallets);

            var lorryType = GetLorryType(stockCode, numPallets);

            return new DispatchRequest
            {
                Consignment = consignmentAsList.ToArray(),
                LorryType = lorryType
            };
        }

        private string GetLorryType(string stockCode, int numPallets)
        {
            bool orderIsLarge = IsOrderLarge(stockCode, numPallets);

            return orderIsLarge ? GetLargeLorryType(stockCode) : GetSmallLorryType(stockCode);
        }

        private string GetSmallLorryType(string stockCode)
        {
            Dictionary<string, string> smallLorryTypes = new Dictionary<string, string>
            {
                { "A", "Modified Transit" },
                { "B", "Modified Transit" },
                { "C", "Transit" },
                { "D", "Transit" },
            };

            return smallLorryTypes.ContainsKey(stockCode) ? smallLorryTypes[stockCode] : "Unknown";
        }

        private string GetLargeLorryType(string stockCode)
        {
            Dictionary<string, string> largeLorryTypes = new Dictionary<string, string>
            {
                { "A", "Lorry" },
                { "B", "Lorry" },
                { "C", "Box Van" },
                { "D", "Box Van" },
            };

            return largeLorryTypes.ContainsKey(stockCode) ? largeLorryTypes[stockCode] : "Unknown";
        }

        private bool IsOrderLarge(string stockCode, int numPallets)
        {
            var orderLargenessThresholdPerStock = new Dictionary<string, int>
            {
                { "A", 3 },
                { "B", 3 },
                { "C", 4 },
                { "D", 4 }
            };

            var threshold = orderLargenessThresholdPerStock.ContainsKey(stockCode)
                ? orderLargenessThresholdPerStock[stockCode]
                : 0;

            return numPallets > threshold;
        }

        private static List<string> GetConsignmentAsList(string stockCode, int numPallets)
        {
            var consignmentAsList = new List<string>();

            for (int palletCount = 1; palletCount <= numPallets; palletCount++)
            {
                consignmentAsList.Add(stockCode);
            }

            return consignmentAsList;
        }

        private int GetBatchSize(string stockCode)
        {
            Dictionary<string, int> batchSizes = new Dictionary<string, int>
            {
                {"A", 6},
                {"B", 10},
                {"C", 20},
                {"D", 48}
            };

            return batchSizes.ContainsKey(stockCode) ? batchSizes[stockCode] : -1;
        }

        private int PrepareOnePalletForEveryBatchOfUnits(int numUnits, int batchSize)
        {
            return OnePalletForEveryNUnitsOrFewer(numUnits, batchSize);
        }

        private static int OnePalletForEveryNUnitsOrFewer(int numUnits, int batchSize)
        {
            return ((numUnits - 1)/ batchSize) + 1;
        }

        public List<DispatchRequest> ReconcileOrders()
        {
            var allDispatchRequests = new List<DispatchRequest>();

            if (_ordersForToday.Any())
            {
                allDispatchRequests.Add(
                    CreateDispatchRequest(
                        GetMostRelevantStockCode(),
                        _ordersForToday.Sum(x => x.NumUnits))
                );
            }

            return allDispatchRequests;
        }

        private string GetMostRelevantStockCode()
        {
            return _ordersForToday.Any(x => IsStockRefrigerated(x.StockCode)) 
                ? _ordersForToday.First(x => IsStockRefrigerated(x.StockCode)).StockCode 
                : _ordersForToday[0].StockCode;
        }

        private bool IsStockRefrigerated(string stockCode)
        {
            var refrigerationValues = new Dictionary<string, bool>
            {
                { "A", true },
                { "B", true },
                { "C", false },
                { "D", false}
            };

            return refrigerationValues.ContainsKey(stockCode) ? refrigerationValues[stockCode] : false;
        }

        public void EndOfDay()
        {
            _ordersForToday.Clear();
        }

        public bool IsLorryRefrigerated(string lorryType)
        {
            var refrigerationValues = new Dictionary<string, bool>
            {
                { "Modified Transit", true },
                { "Transit", false },
                { "Box Van", false },
                { "Lorry", true}
            };

            return refrigerationValues.ContainsKey(lorryType) ? refrigerationValues[lorryType] : false;
        }
    }
}