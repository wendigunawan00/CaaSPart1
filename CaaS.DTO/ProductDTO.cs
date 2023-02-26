﻿namespace CaaS.DTO;

public class ProductDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price{ get; set; }
    public string AmountDesc { get; set; }
    public string? ProductDesc { get; set; }
    public string? DownloadLink { get; set; }
    public string ShopId { get; set; }


    public override string ToString() =>
      $"Product(id:{Id}, name:{Name}, price:{Price}, amountDesc:{AmountDesc}, productDesc:{ProductDesc},Shop:{ShopId})";
}
