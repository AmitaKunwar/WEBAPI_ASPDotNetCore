using HomeAPI.Models;
using HomeAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private IRepository _repository;
        public ShoppingController(IRepository _repos)
        {
            this._repository = _repos;
        }

        [HttpGet]
        [Route("GetShoppingList")]
        public async Task<ActionResult> GetAll()
        {
           
            return Ok(await _repository.GetList());
        }

        [HttpGet]
        [Route("MyCategoryList")]
        [Authorize]
        public async Task<ActionResult> GetListByCategory(string username)
        {

            return Ok(await _repository.GetMyShoppingCategory(username));
        }

        [HttpPost]
        [Route("SearchItem")]
        [Authorize]
        public async Task<ActionResult<Object>> Getoutput(SearchItem inputparam)
        {
            var result = await _repository.SearchItems(inputparam);
        
                return Ok(result);
        }

        [HttpGet]
        [Route("MyShoppingItems")]
        [Authorize]
        public async Task<ActionResult> GetItemList(string username,string categoryname)
        {
            var result = await _repository.GetMyShoppingItem(username, categoryname);
            return Ok(result);
        }

        [HttpGet("{Id}", Name = nameof(GetSingleItem))]
        [Authorize]
        public async Task<ActionResult<ShoppingList>> GetSingleItem(int id)
        {
            var result = await _repository.GetSingleEntity(id);
            if (result == null)
                return NotFound();
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ShoppingList>> CreateShoppingWish(ShoppingList _item)
        {
            try
            {
                if (_item == null)
                    return BadRequest();

                var _shoppingItem = await _repository.Add(_item);
                return CreatedAtAction(nameof(GetSingleItem),
               new { id = _item.Id }, _item);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Serve Error");
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ShoppingList>> DeleteItem(int id)
        {
            try
            {
                var itemToDelete = await this.GetSingleItem(id);
                if (itemToDelete.Value == null)
                {
                    return NotFound($"Item with Id = {id} not found");
                }
                await _repository.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
            }
        }
        [HttpPost]
        [Route("GetAllShoppingItem")]
        [Authorize]
        public async Task<ActionResult> GetAllShoppingList(SearchItem inputparam)
        {
            return Ok(await _repository.GetAllShoppingItem(inputparam));
        }

        [HttpGet]
        [Route("GetUserTotalExpense")]
        [Authorize]
        public async Task<ActionResult> GetTotalExpense(string username)
        {
            return Ok(await _repository.GetUserExpense(username));
        }


        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ShoppingList>> UpdateItem(int id , ShoppingList item)
        {
            try
            {
               if(id != item.Id)
                    return BadRequest("Item ID mismatch");
               
                await _repository.Update(item);              
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
            }
        }
    }
}

