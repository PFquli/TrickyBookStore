using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Store;
using System.Linq;

namespace TrickyBookStore.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IList<Book> _books;

        public BookService()
        {
            _books = Store.Books.Data.ToList();
        }

        private bool CheckIfThisBookIncluded(Book book, long[] ids)
        {
            int includedBookIndex = Array.IndexOf(ids, book.Id);
            return includedBookIndex > -1;
        }

        public IList<Book> GetBooks(params long[] ids)
        {
            List<Book> bookList = new List<Book>();
            foreach (Book book in _books)
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