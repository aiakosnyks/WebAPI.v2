using Entities.Models;
using Repositories.Contracts;
using Repositories.EF_Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EF_Core
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository // interface'de istediğimiz kadar interface inheritance yapabiliriz
    {
        public BookRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book) => Create(book);
        public void DeleteOneBook(Book book) => Delete(book);
        public IQueryable<Book> GetAllBooks(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(x => x.Id);
        public IQueryable<Book> GetOneBookById(int id, bool trackChanges) =>
            FindByCondition(x => x.Id.Equals(id), trackChanges);

        public void UpdateOneBook(Book book) => Update(book);
    }
}
