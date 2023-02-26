using CaaS.Domain;
using System.Data;

namespace CaaS.Dal.Ado
{
    public class AdoMapDao {

        public static Person MapRowToPerson(IDataRecord row) =>
        new(
            id: (string)row["person_id"],
            firstName: (string)row["first_name"],
            lastName: (string)row["last_name"],
            dateOfBirth: (DateTime)row["dob"],
            email: (string)row["email"],
            addressId: (string)row["address"],
            status: (string)row["status"],
            password: (string)row["password"]
        );


        public static Address MapRowToAddress(IDataRecord row) =>
        new(
            id: (string)row["address_id"],
            street: (string)row["street"],
            floor: (string)row["floor"],
            postalCode: (int)row["postal_code"],
            city: (string)row["city"],
            province: (string)row["province"],
            country: (string)row["country"]
        );

        public static Shop MapRowToShop(IDataRecord row) =>
        new(
           id: (string)row["shop_id"],
           name: (string)row["shop_name"],
           fieldDesc: (string)row["field_descriptions"],
           address: (string)row["address"]
        );


        public static Product MapRowToProduct(IDataRecord row) =>
        new(
            id: (string)row["product_id"],
            name: (string)row["product_name"],
            price: (double)row["price"],
            amountDesc: (string)row["amount_desc"],
            productDesc: (string)row["product_desc"],
            downloadLink: (string)row["download_link"],
            shopId: (string)row["shop_id"]
        );


        public static OrderDetails MapRowToOrderDetails(IDataRecord row) =>
        new(
           id: (string)row["order_details_id"],
           orderId: (string)row["order_id"],
           productId: (string)row["product_id"],
           unitPrice: (double)row["unit_price"],
           quantity: (double)row["qty"],
           discount: (double)row["discount"],
           shopId: (string)row["shop_id"]

       );

        public static Order MapRowToOrder(IDataRecord row) =>
        new(
            id: (string)row["order_id"],
            custId: (string)row["cust_id"],
            cartId: (string)row["cart_id"],
            orderDate: (DateTime)row["order_date"]
        );

        public static CartDetails MapRowToCartDetails(IDataRecord row) =>
        new(
            id: (string)row["cart_details_id"],
            cartId: (string)row["cart_id"],
            productId: (string)row["product_id"],
            quantity: (double)row["qty"],
            shopId: (string)row["shop_id"]
        );

        public static Cart MapRowToCart(IDataRecord row) =>
        new(
            id: (string)row["cart_id"],
            custId: (string)row["cust_id"],
            status: (string)row["status"]
        );

        public static AppKey MapRowToAppKey(IDataRecord row) =>
        new(
           id: (string)row["app_key"],
           appKeyName: (string)row["app_key_name"],
           appKeyPassword: (string)row["app_key_password"],
           shopId: (string)row["shop_id"]
        );
    }
}
