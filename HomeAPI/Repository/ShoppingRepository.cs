using HomeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeAPI.Repository
{
    public class ShoppingRepository : IRepository
    {
        protected RepositoryContext _repos;
        public ShoppingRepository(RepositoryContext repos)
        {
            this._repos = repos;
        }
        public async Task<ShoppingList> Add(ShoppingList entity)
        {
             var result = await _repos.shoppingList.AddAsync(entity);
            _repos.SaveChanges();
            return result.Entity;
        }

        public async Task<ShoppingList> Delete(int id)
        {
            var _item = await _repos.shoppingList.FindAsync(id);
           // var _item = await _repos.shoppingList.FirstOrDefaultAsync(x => x.ID == id);
            if(_item != null)
            {
                 _repos.shoppingList.Remove(_item);
                _repos.SaveChanges();
            }
            return _item;
           
        }

        public IQueryable<ShoppingList> FindByCondition(Expression<Func<ShoppingList, bool>> expression)
        {
            return this._repos.Set<ShoppingList>().Where(expression).AsNoTracking();
        }

        public async Task<IEnumerable<SearchOutput>> GetAllShoppingItem(SearchItem inputparam)
        {
            string StoredProc = "EXEC sp_GetShoppingItem " +
                  "@CategoryName = '" + inputparam.CategoryName + "'," +
                    "@ItemName = '" + inputparam.ItemName + "'," +
                    "@ItemPrice1 = '" + inputparam.ItemPrice_1 + "'," +
                    "@ItemPrice2 = '" + inputparam.ItemPrice_2 + "'," +
                    "@userid = '" + inputparam.UserID + "'";
            return await _repos.searchOutput.FromSqlRaw(StoredProc).ToListAsync();
        }

        public Task<IEnumerable<SearchOutput>> GetAllShoppingItem(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShoppingList>> GetList()
        {
            return await _repos.shoppingList.ToListAsync();
            
        }

        public async Task<IEnumerable<ShoppingItemByCategoryName>> GetMyShoppingCategory(string username)
        {
            string StoredProc = "EXEC sp_GetMyCategory " +
                 "@username = '" + username + "'";
            var result = await _repos.myCategoryShoppingItem.FromSqlRaw(StoredProc).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ShoppingItem>> GetMyShoppingItem(string username, string categoryname)
        {
            string StoredProc = "EXEC sp_GetMyShoppingIndividualItem " +
                "@username = '" + username + "'," +
                "@CategoryName = '" + categoryname + "'";

            var result = await _repos.myShoppingItem.FromSqlRaw(StoredProc).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ShoppingItemByCategoryName>> GetShoppingCategory(string username)
        {
            var items = await (from c in _repos.shoppingCategory
                               join s in _repos.shoppingList
                         on c.CatID equals s.CategoryID
                               group new { c, s } by new { c.CategoryName } into gr
                               select new ShoppingItemByCategoryName()
                               {
                                   CategoryName = gr.Key.CategoryName,
                                   ItemPrice = gr.Sum(x => x.s.ItemPrice)
                               }).ToListAsync();

            return  items;
        }

        public async Task<IEnumerable<ShoppingItem>> GetShoppingItem(string itemName)
        {
           var items = await(from c in _repos.shoppingCategory
                              join s in _repos.shoppingList
                        on c.CatID equals s.CategoryID
                             where c.CategoryName == itemName
                             select new ShoppingItem()
                              {
                                  //CategoryName = c.CategoryName,
                                  itemName = s.Itemname,
                                  itemPrice = s.ItemPrice
                              }).ToListAsync();

            return items;
        }

        public async Task<ShoppingList> GetSingleEntity(int id)
        {
           return await _repos.shoppingList.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserTotalExpense> GetUserExpense(string username)
        {
            string StoredProc = "EXEC sp_GetMyTotalExpenditure " +
                           "@username = '" + username + "'";
            var result = await _repos.userTotalExpenses.FromSqlRaw(StoredProc).ToListAsync();
            return  result.FirstOrDefault();
        }

        public async Task<IEnumerable<SearchOutput>> SearchItems(SearchItem sinput)
        {
            string StoredProc = "EXEC sp_SearchShoppingItem " +
                 "@CategoryName = '" + sinput.CategoryName + "'," +
                    "@ItemName = '" + sinput.ItemName + "'," +
                    "@ItemPrice1 = '" + sinput.ItemPrice_1 + "'," +
                    "@ItemPrice2 = '" + sinput.ItemPrice_2 + "'," +
                    "@userid = '" + sinput.UserID + "'" ;
            return await _repos.searchOutput.FromSqlRaw(StoredProc).ToListAsync();
        }

        public async  Task<ShoppingList> Update(ShoppingList entity)
        {
             _repos.shoppingList.Update(entity);
             await _repos.SaveChangesAsync();
           
            return entity;
        }



        
    }
}
