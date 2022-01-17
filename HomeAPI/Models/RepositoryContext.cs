using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ShoppingList> shoppingList { get; set; }
        public DbSet<ShoppingCategory> shoppingCategory { get; set; }
        public DbSet<SearchOutput> searchOutput { get; set; }
        public DbSet<ShoppingItem> myShoppingItem { get; set; }
        public DbSet<UserTotalExpense> userTotalExpenses { get; set; }
        public DbSet<ShoppingItemByCategoryName> myCategoryShoppingItem { get; set; }

    }
}
