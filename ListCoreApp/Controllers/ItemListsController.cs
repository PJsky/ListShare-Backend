using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListCoreApp.Data;
using ListCoreApp.Models;
using Microsoft.CodeAnalysis;
using ListCoreApp.Requests;
using ListCoreApp.Helpers;
using ListCoreApp.Responses;
using ListCoreApp.Responses.ItemList;

namespace ListCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemListsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ItemListsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ItemLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemList>>> GetItemLists([FromQuery] GetListRequest request)
        {
            var itemList = await _context.ItemLists.Where(il => il.AccessCode == request.AccessCode).FirstAsync();
            itemList.Items = await _context.Items.Where(i => i.ListId == itemList.Id).ToListAsync();
            var items = itemList.Items.Select(i => new { i.Name, i.IsDone});
            if (itemList.IsPublic || itemList.ListPassword == request.ListPassword) return Ok(new SuccessfulGetListResponse() { 
                Name = itemList.Name,
                AccessCode = itemList.AccessCode,
                IsPublic = itemList.IsPublic,
                Items = items
            });;
            return BadRequest("Wrong list access code or password");
        }

        // GET: api/ItemLists/5
        [HttpGet("{accessCode}")]
        public async Task<ActionResult<ItemList>> GetItemList(string accessCode)
        {
            try
            {
                var itemList = await _context.ItemLists.Where(il => il.AccessCode == "#" + accessCode).FirstAsync();
                //itemList.Items = _context.Items.ToList();
            }
            catch {
                return BadRequest("List not found");
            }

            return Ok(accessCode);
        }

        // POST: api/ItemLists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemList>> PostItemList(CreateListRequest itemList)
        {
            const int accessCodeLength = 8;
            if (itemList.IsPublic == false && itemList.ListPassword == null) return BadRequest("Private list needs a password");

            var accessCode = AccessCodeGenerator.GetAccessCode(accessCodeLength, _context);
            _context.ItemLists.Add(new ItemList() { 
                Id = itemList.Id,
                Name = itemList.Name,
                IsPublic = itemList.IsPublic,
                ListPassword = itemList.ListPassword,
                AccessCode = accessCode
            });
            await _context.SaveChangesAsync();

            return Ok(new SuccessfulListCreation(){
                Name = itemList.Name,
                AccessCode = accessCode,
            });;
        }

        // DELETE: api/ItemLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemList>> DeleteItemList(int id)
        {
            var itemList = await _context.ItemLists.FindAsync(id);
            if (itemList == null)
            {
                return NotFound();
            }

            _context.ItemLists.Remove(itemList);
            await _context.SaveChangesAsync();

            return itemList;
        }

        private bool ItemListExists(int id)
        {
            return _context.ItemLists.Any(e => e.Id == id);
        }
    }
}
