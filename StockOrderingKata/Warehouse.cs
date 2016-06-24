using System;
using System.Collections.Generic;
using System.Linq;

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

            return new DispatchRequest {Consignment = consignmentAsList.ToArray()};
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
                        _ordersForToday[0].StockCode,
                        _ordersForToday.Sum(x => x.NumUnits))
                );
            }

            return allDispatchRequests;
        }

        public void EndOfDay()
        {
            _ordersForToday.Clear();
        }
    }
}