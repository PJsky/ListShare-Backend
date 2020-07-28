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
        public async Task<ActionResult<IEnumerable<ItemList>>> GetItemLists()
        {
            return await _context.ItemLists.ToListAsync();
        }

        // GET: api/ItemLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemList>> GetItemList(int id)
        {
            var itemList = await _context.ItemLists.FindAsync(id);
            //itemList.Items = _context.Items.ToList();

            if (itemList == null)
            {
                return NotFound();
            }

            return itemList;
        }

        // PUT: api/ItemLists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemList(int id, ItemList itemList)
        {
            if (id != itemList.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItemLists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemList>> PostItemList(ItemList itemList)
        {
            _context.ItemLists.Add(itemList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemList", new { id = itemList.Id }, itemList);
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
