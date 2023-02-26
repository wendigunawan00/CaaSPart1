﻿namespace CaaS.DTO;

public class CartDetailsDTO
{
    public string Id { get; set; }
    public string CartId { get; set; }
    public string ProductId { get; set; }
    public double Quantity { get; set; }
    public string ShopId { get; set; }

    public override string ToString() =>
      $"CartDetails(id:{Id}, CartId:{CartId}, ProductId:{ProductId}, Quantity:{Quantity}, Shop:{ShopId})";
}
