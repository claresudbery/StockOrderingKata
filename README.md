# StockOrderingKata
testing testing... (for Kirsty)

18_is_reu are writing the software to manage the dispatching of stock from our main warehouse to our three supermarkets based in Manchester, Salford and Stockport.

Stock is delivered as whole pallets (see table below), if a supermarket orders fewer items than would fit on a single pallet, then they will have to deal with the excess. Partial pallet deliveries are not supported, and pallets may not be mixed with different unit types. For example, a request for 2 As and 1 B would result in a Consignment array of  [“A”, “B”].

The table below shows how many units of each stock are delivered per pallet, and whether the stock must be transported in a refrigerated vehicle or not. 

Stock which does not require refrigeration may still be transported in a vehicle with refrigeration capabilities.

Code    Units per Pallet  Refrigerated?
A       6                 Yes
B       10                Yes
C       20                No
D       48                No

Our logistics partner has a number of different types of vehicle available to us, each has a fixed cost per delivery, whether the vehicle is full or not.

Vehicle type      Capacity (Pallets)  Refrigerated? Cost Per Delivery 
Transit           4                   No            £50               
Modified Transit  3                   Yes           £60               
Box Van           8                   No            £80               
Lorry             30                  Yes           £200

A supermarket may make several delivery requests per day. Delivery requests are reconciled at the end of the day and your task is to ensure the cost of delivery is low as possible.

Our supermarkets send us requests in this format:

class DeliveryRequest {
	public string SupermarketId { get; set; }
	public string Sku { get; set; }
	public int Quantity { get; set; }
}

We send requests to our logistics partner in this format:

public class DispatchRequest {
        public string LorryType { get; set; }
        public string[] Consignment { get; set; }
        public string Destination { get; set; }
}

