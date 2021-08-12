using System.Collections.Generic;

namespace DataAccessCore.Backet.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }

        public List<ShoppingCartItem> ShoppingCartItem { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (ShoppingCartItem item in ShoppingCartItem)
                {
                    total += item.Price * item.Quantity;
                }

                return total;
            }
        }

    }
}
