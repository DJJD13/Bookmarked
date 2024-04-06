using System.Net;
using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Mappers;
using Bookmarked.Server.Models;
using Newtonsoft.Json;

namespace Bookmarked.Server.Service
{
    public class ISBNdbService(IConfiguration config) : IISBNdbService
    {
        private readonly IConfiguration _config = config;

        public async Task<Book?> FindBookByISBNAsync(string isbn)
        {
            try
            {
                var url = $"https://api2.isbndb.com/book/{isbn}";
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", _config["ISBNdbKey"]);

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IsbnRoot>(content);

                return result?.book.ToBookFromIsbnBook();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
