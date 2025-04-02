using System.Collections;

namespace SwagLabsHomepageEnum.Utils
{
    public enum ProductsInHomepage
    {
        Backpack,
        BikeLight,
        BoltTShirt,
        FleeceJacket,
        Onesie,
        RedTShirt        
    }

    public static class ProductsInHomepageExtension
    {
        public static string GetProduct(this ProductsInHomepage product)
        {
            return product switch
            {
                ProductsInHomepage.Backpack => "Sauce Labs Backpack",
                ProductsInHomepage.BikeLight => "Sauce Labs Bike Light",
                ProductsInHomepage.BoltTShirt => "Sauce Labs Bolt T-Shirt",
                ProductsInHomepage.FleeceJacket => "Sauce Labs Fleece Jacket",
                ProductsInHomepage.Onesie => "Sauce Labs Onesie",
                ProductsInHomepage.RedTShirt => "Test.allTheThings() T-Shirt (Red)",
                _ => throw new System.NotImplementedException()
            };
        }
    }
   
}