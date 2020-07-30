using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListCoreApp.Data;
using ListCoreApp.Models;
using ListCoreApp.Requests.Item;
using ListCoreApp.Responses.Item;

namespace ListCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutItem(UpdateItemRequest request)
        {
            try
            {
                var itemList = await _context.ItemLists.Where(il => il.AccessCode == request.ListAccessCode).FirstAsync();
                if (!itemList.IsPublic && itemList.ListPassword != request.ListPassword) return BadRequest("Wrong list password");
                var item = await _context.Items.Where(i => i.Id == request.ItemId).FirstAsync();
                item.IsDone = request.IsDone;
                await _context.SaveChangesAsync();
                return Ok(request.IsDone);
            }
            catch 
            {
                return BadRequest("Proper list access code needed");
            }
        }

        // POST: api/Items
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(CreateItemRequest request)
        {
            try
            {
                var itemList = await _context.ItemLists.Where(il => il.AccessCode == request.ListAccessCode).FirstAsync();
                if (!itemList.IsPublic && itemList.ListPassword != request.ListPassword) return BadRequest("Wrong list password");
                var item = new Item
                {
                    Name = request.Name,
                    ListId = itemList.Id
                };
                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                return Ok(new SuccessfulItemCreationResponse()
                {
                    ItemId = item.Id,
                    ItemName = request.Name,
                    ListName = itemList.Name
                });
            }
            catch {
                return BadRequest("Proper list access code needed");
            }
        }

        // DELETE: api/Items
        [HttpDelete]
        public async Task<ActionResult<Item>> DeleteItem(ItemDeleteRequest request)
        {
            try
            {
                var item = await _context.Items.FindAsync(request.ItemId);
                var itemList = await _context.ItemLists.Where(il => il.AccessCode == request.ListAccessCode).FirstAsync();
                if (!itemList.IsPublic && itemList.ListPassword != request.ListPassword) return BadRequest("Wrong list password");
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch {
                return BadRequest("Proper list access code needed");
            }
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
