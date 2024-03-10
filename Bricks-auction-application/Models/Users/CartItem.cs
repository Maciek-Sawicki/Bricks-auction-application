namespace Bricks_auction_application.Models.Users
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Set Set { get; set; } // 
    }
}
