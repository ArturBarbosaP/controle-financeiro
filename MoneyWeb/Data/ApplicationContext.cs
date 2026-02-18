using Microsoft.EntityFrameworkCore;
using MoneyWeb.Models.Entities;

namespace MoneyWeb.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {}
    }
}