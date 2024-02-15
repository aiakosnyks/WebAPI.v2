using Entities.Models;

namespace Book_Demo.Data
{
    public static class ApplicationContext
    {
        public static List<Book> Books { get; set; }
        static ApplicationContext()
        {
            Books = new List<Book>()
            {
                new Book (){Id=1, Title="Otomatik Portakal", Price=130},
                new Book (){Id=2, Title="Tututnamayanlar", Price=250},
                new Book (){Id=3, Title="Trainspotting", Price=180}
            };
        }
    }
}
