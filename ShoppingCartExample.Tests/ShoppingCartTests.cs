using ShoppingCartExample.Models;

namespace ShoppingCartExample.Tests;

[TestFixture]
public class ShoppingCartTests
{
    private ShoppingCart _shoppingCart = null!;
    
    [SetUp]
    public void Setup()
    {
        _shoppingCart = new ShoppingCart();
    }

    [Test]
    public void AddItem_WhenValidItemAdded_ItemIsInCart()
    {
        // Arrange
        var product = new Product { Id = 1 };

        // Act
        _shoppingCart.AddItem(product, 2);

        // Assert
        var item = _shoppingCart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
        Assert.That(item, Is.Not.Null);
        Assert.That(item!.Quantity, Is.EqualTo(2));
    }

    [Test]
    public void ReduceItem_WhenValidItemReduced_QuantityDecreases()
    {
        // Arrange
        var product = new Product { Id = 1 };
        _shoppingCart.AddItem(product, 5);

        // Act
        _shoppingCart.ReduceItem(product, 2);

        // Assert
        var item = _shoppingCart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
        Assert.That(item, Is.Not.Null);
        Assert.That(item!.Quantity, Is.EqualTo(3));
    }

    [Test]
    public void ReduceItem_WhenReducingMoreThanQuantity_RemovesItem()
    {
        // Arrange
        var product = new Product { Id = 1 };
        _shoppingCart.AddItem(product, 3);

        // Act
        _shoppingCart.ReduceItem(product, 4);

        // Assert
        var item = _shoppingCart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
        Assert.That(item, Is.Null);
    }

    [Test]
    public void RemoveItem_WhenValidItemRemoved_ItemIsNotInCart()
    {
        // Arrange
        var product = new Product { Id = 1 };
        _shoppingCart.AddItem(product, 2);

        // Act
        _shoppingCart.RemoveItem(product);

        // Assert
        var item = _shoppingCart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
        Assert.That(item, Is.Null);
    }

    [Test]
    public void AddItem_WhenProductIsNull_ThrowsArgumentNullException()
    {
        //Arrange
        Product? product = null;
        
       // Act and Assert
        Assert.Throws<ArgumentNullException>(() => _shoppingCart.AddItem(product, 2));
    }

    [Test]
    public void AddItem_WhenQuantityIsZero_ThrowsArgumentException()
    {
        // Arrange
        var product = new Product { Id = 1 };

        // Act and Assert
        Assert.Throws<ArgumentException>(() => _shoppingCart.AddItem(product, 0));
    }

    [Test]
    public void ReduceItem_WhenProductIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        Product? product = null;

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => _shoppingCart.ReduceItem(product, 10));
    }

    [Test]
    public void ReduceItem_WhenQuantityIsZero_DoesNotThrowException()
    {
        // Arrange
        var product = new Product { Id = 1 };

        // Act and Assert
        Assert.DoesNotThrow(() => _shoppingCart.ReduceItem(product, 0));
    }

    [Test]
    public void RemoveItem_WhenProductIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        Product? product = null;

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => _shoppingCart.RemoveItem(product));
    }

    [Test]
    public void AddItem_WhenItemAlreadyAdded_QuantityIsUpdated()
    {
        // Arrange
        var product = new Product { Id = 1 };
        _shoppingCart.AddItem(product, 2);

        // Act
        _shoppingCart.AddItem(product, 3);

        // Assert
        var item = _shoppingCart.Items.First(item => item.Product.Id == product.Id);
        Assert.That(item.Quantity, Is.EqualTo(5));
    }

    [Test]
    public void ReduceItem_WhenItemNotInItemsList_NoChangeToCart()
    {
        // Arrange
        var product = new Product { Id = 1 };

        // Act
        _shoppingCart.ReduceItem(product, 2);

        // Assert
        var item = _shoppingCart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
        Assert.That(item, Is.Null);
    }

    [Test]
    public void AddItem_AddTwoDifferentProducts_ContainsTwoItems()
    {
        // Arrange
        var first = new Product { Id = 1 };
        var second = new Product { Id = 2 };

        // Act
        _shoppingCart.AddItem(first, 2);
        _shoppingCart.AddItem(second, 5);

        // Assert
        Assert.That(_shoppingCart.Items, Has.Count.EqualTo(2));
    }
}