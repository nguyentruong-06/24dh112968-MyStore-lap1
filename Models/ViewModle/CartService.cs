using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _24dh112968_MyStore_lap1.Models.ViewModle
{
    public class CartService // để tương tác với giỏ hàng trong phần session
    {
        private readonly HttpSessionStateBase session;

        public CartService(HttpSessionStateBase session)
        {
            this.session = session;
        }

        public Cart GetCart()
        {
            var cart = (Cart)session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                session["Cart"] = cart;
            }
            return cart;
        }

        public void ClearCart()
        {
            session["Cart"] = null;
        }
    }
}