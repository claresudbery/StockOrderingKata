using System;
using System.Collections.Generic;

namespace StockOrderingKata
{
    public class Warehouse
    {
        public DispatchRequest OrderStock(string stockCode, int numUnits)
        {
            int numPallets = PrepareOnePalletForEveryBatchOfUnits(numUnits, GetBatchSize(stockCode));

            var consignmentAsList = GetConsignmentAsList(stockCode, numPallets);

            return new DispatchRequest { Consignment = consignmentAsList.ToArray() };
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
            return new List<DispatchRequest> {OrderStock("B", 30)};
        }
    }
}