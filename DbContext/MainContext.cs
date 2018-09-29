namespace DbContext
{
    using Models.DbModels;
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;

    public class MainContext : DbContext
    {
        // Your context has been configured to use a 'MainContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DbContext.MainContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MainContext' 
        // connection string in the application configuration file.
        public MainContext()
            : base("data source=(LocalDb)\\MSSQLLocalDB;initial catalog=MainContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SqlProviderServices instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}