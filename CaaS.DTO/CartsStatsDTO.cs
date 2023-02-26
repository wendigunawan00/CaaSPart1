namespace CaaS.DTO;

public class CartsStatsDTO
{
    public double TotalOpenCart { get; set; }

    public CartsStatsDTO(double totalOpenCart, double totalClosedCart)
    {
        TotalOpenCart = totalOpenCart;
        TotalClosedCart = totalClosedCart;
    }

    public double TotalClosedCart { get; set; }


    public override string ToString() =>
      $"Carts(still open:{TotalOpenCart}, all closed:{TotalClosedCart})";
}
