using System.Collections.Generic;

namespace StockOrderingKata
{
    public class Warehouse
    {
        public DispatchRequest OrderStock(string stockCode, int numUnits)
        {
            int numPallets = PrepareOnePalletForEverySixUnitsOrFewer(numUnits);

            var consignmentAsList = new List<string>();

            for (int palletCount = 1; palletCount <= numPallets; palletCount++)
            {
                consignmentAsList.Add("A");
            }

            return new DispatchRequest { Consignment = consignmentAsList.ToArray() };
        }

        private int PrepareOnePalletForEverySixUnitsOrFewer(int numUnits)
        {
            return ((numUnits - 1)/6) + 1;
        }
    }
}