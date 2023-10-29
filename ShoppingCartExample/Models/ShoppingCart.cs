namespace ShoppingCartExample.Models;

public class ShoppingCart
{
    private readonly ICollection<CartItem> _items;

    public IReadOnlyCollection<CartItem> Items => _items.ToList();

    public ShoppingCart()
    {
        _items = new List<CartItem>();
    }

    public void AddItem(Product product, int quantity)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0.", nameof(quantity));
        }

        var existingItem = Items.FirstOrDefault(item => item.Product.Id == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                Product = product,
                Quantity = quantity
            });
        }
    }

    public void ReduceItem(Product product, int quantity)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        if (quantity <= 0)
        {
            return;
        }

        var item = Items.FirstOrDefault(item => item.Product.Id == product.Id);
        if (item == null)
        {
            return;
        }

        item.Quantity -= quantity;

        if (item.Quantity <= 0)
        {
            RemoveItem(product);
        }
    }

    public void RemoveItem(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var itemToRemove = Items.FirstOrDefault(item => item.Product.Id == product.Id);
        if (itemToRemove != null)
        {
            _items.Remove(itemToRemove);
        }
    }
}