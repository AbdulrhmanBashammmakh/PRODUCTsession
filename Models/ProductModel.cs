namespace PRODUCTsession.Models
{
    public class ProductModel
    {
        private List<Product> Products;

        public ProductModel()
        {
            Products = new List<Product>() {
                new Product
                {
                    Id = 1,
                    Name = "name 1",
                    Price = 4,
                 
                },
                new Product
                {
                    Id = 2,
                    Name = "name 2",
                    Price = 2,
                     
                },
                new Product
                {
                    Id = 3,
                    Name = "name 3",
                    Price = 8,
                    
                }
            };
        }

        public List<Product> findAll()
        {
            return Products;
        }

        public Product find(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

    }
}
