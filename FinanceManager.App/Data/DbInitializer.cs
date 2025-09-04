namespace FinanceManager.App.Data
{
    public static class DbInitializer
    {
        public static void EnsureCreated()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
            db.SaveChanges();
        }
    }
}
