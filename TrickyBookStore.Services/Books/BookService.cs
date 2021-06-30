using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Store;
using System.Linq;

namespace TrickyBookStore.Services.Books
{
    public class BookService : IBookService
    {
        public BookService()
        {
        }

        private bool CheckIfThisBookIncluded(Book book, long[] ids)
        {
            int includedBookIndex = Array.IndexOf(ids, book.Id);
            return includedBookIndex > -1;
        }

        public IList<Book> GetBooks(params long[] ids)
        {
            List<Book> bookList = new List<Book>();
            List<Book> booksData = Store.Books.Data.ToList();
            foreach (Book book in booksData)
            {
                if (CheckIfThisBookIncluded(book, ids))
                {
                    bookList.Add(book);
                }
            }
            return bookList;
        }
    }
}