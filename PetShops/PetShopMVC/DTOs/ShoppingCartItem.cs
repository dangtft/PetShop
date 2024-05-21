namespace PetShopMVC.DTOs
{
    public class ShoppingCartItem
    {
        public int CartId { get; set; }
        public ProductDTO? Product { get; set; }
        public int Qty { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
