using System;
using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class BookCategories
    {
        public static readonly IEnumerable<@int> Data = new List<@int>
        {
            new @int { Id = 1, Title = "History"},
            new @int { Id = 2, Title = "Economics"},
            new @int { Id = 3, Title = "Novel"},
            new @int { Id = 4, Title = "Literature"}
        };

        internal static List<@int> ToList()
        {
            throw new NotImplementedException();
        }
    }
}