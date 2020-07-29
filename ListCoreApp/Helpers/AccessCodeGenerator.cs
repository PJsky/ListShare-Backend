using ListCoreApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Helpers
{
    public static class AccessCodeGenerator
    {
        private static Random random = new Random();
        public static string GetAccessCode(int length, DatabaseContext _context)
        {
            string accessCode;
            //Generate new string until its unique in database
            do {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                accessCode = new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                accessCode = "#" + accessCode;
            } while (_context.ItemLists.Where(itemList => itemList.AccessCode == accessCode).Any());
            return accessCode;
        }
    }
}
