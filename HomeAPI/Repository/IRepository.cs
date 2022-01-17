using HomeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeAPI.Repository
{
    public interface IRepository
    {
        public IQueryable<ShoppingList> FindByCondition(Expression<Func<ShoppingList, bool>> expression);
        Task<IEnumerable<ShoppingList>> GetList();
        Task<ShoppingList> GetSingleEntity(int id);
        Task<ShoppingList> Add(ShoppingList entity);
        Task<ShoppingList> Update(ShoppingList entity);
        Task<ShoppingList> Delete(int id);
        Task<IEnumerable<ShoppingItem>> GetMyShoppingItem(string username, string categoryname);
        Task<IEnumerable<ShoppingItemByCategoryName>> GetMyShoppingCategory(string username);
        Task<IEnumerable<SearchOutput>> SearchItems(SearchItem item);
        Task<IEnumerable<SearchOutput>> GetAllShoppingItem(SearchItem inputparam);

        Task<UserTotalExpense> GetUserExpense(string name);


    }
}
