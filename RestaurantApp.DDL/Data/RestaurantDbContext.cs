namespace RestaurantApp.DDL.Data
{

    public class RestaurantDbContext : DbContext
    {
        
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders {  get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Category> Categories { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantDbContext).Assembly);
        
        }
    }
}
