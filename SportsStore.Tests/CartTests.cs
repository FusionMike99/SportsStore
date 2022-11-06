using SportsStore.Models;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // Arrange
            Product p1 = new() { ProductID = 1, Name = "P1" };
            Product p2 = new() { ProductID = 2, Name = "P2" };

            Cart target = new();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange
            Product p1 = new() { ProductID = 1, Name = "P1" };
            Product p2 = new() { ProductID = 2, Name = "P2" };

            Cart target = new();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = (target.Lines ?? new())
                .OrderBy(c => c.Product.ProductID).ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange
            Product p1 = new() { ProductID = 1, Name = "P1" };
            Product p2 = new() { ProductID = 2, Name = "P2" };
            Product p3 = new() { ProductID = 3, Name = "P3" };

            Cart target = new();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.Empty(target.Lines.Where(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count);
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange
            Product p1 = new() { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new() { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            // Assert
            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange
            Product p1 = new() { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new() { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act
            target.Clear();

            // Assert
            Assert.Empty(target.Lines);
        }
    }
}
