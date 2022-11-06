namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext _dbContext;

        public EFStoreRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> Products => _dbContext.Products;

        public void CreateProduct(Product product)
        {
            _dbContext.Add(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _dbContext.Remove(product);
            _dbContext.SaveChanges();
        }

        public void SaveProduct(Product product)
        {
            _dbContext.SaveChanges();
        }
    }
}
