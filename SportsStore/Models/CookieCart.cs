using SportsStore.Infrastructure;
using System.Text.Json.Serialization;

namespace SportsStore.Models
{
    public class CookieCart : Cart
    {
        [JsonIgnore]
        public HttpContext? HttpContext { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            HttpContext? httpContext = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext;
            var cart = httpContext?.Request.Cookies.GetJson<CookieCart>("Cart") ?? new CookieCart();
            cart.HttpContext = httpContext;
            return cart;
        }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            HttpContext?.Response.Cookies.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            HttpContext?.Response.Cookies.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            HttpContext?.Response.Cookies.Delete("Cart");
        }
    }
}
