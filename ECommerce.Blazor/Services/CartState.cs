namespace ECommerce.Blazor.State
{
    public class CartState
    {
        public List<CartItem> Items { get; set; } = new();

        public void Add(ProductDto product)
        {
            var existing = Items.FirstOrDefault(x => x.Product.Id == product.Id);

            if (existing == null)
            {
                Items.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                existing.Quantity++;
            }
        }

        public void Remove(int productId)
        {
            var item = Items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)
                Items.Remove(item);
        }

        public decimal TotalPrice =>
            Items.Sum(i => i.Product.Price * i.Quantity);
    }

    public class CartItem
    {
        public ProductDto Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
