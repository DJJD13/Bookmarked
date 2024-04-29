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

        public async Task<IsbnBook?> FindBookByISBNAsync(string isbn)
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

                return result?.book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        
        public async Task<List<IsbnBook>> FindBooksAsync(string isbn)
        {
            try
            {
                var url = $"https://api2.isbndb.com/books/{isbn}?page=1&pageSize=20";
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", _config["ISBNdbKey"]);
         
                var response = await client.GetAsync(url);
         
                if (!response.IsSuccessStatusCode) return [];
         
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IsbnBookSearch>(content);

                return result != null ? result.books : [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return [];
            }
        }
        
        public async Task<List<IsbnBook>> FindBooksByAuthorAsync(string query)
        {
            try
            {
                var url = $"https://api2.isbndb.com/author/{query}?page=1&pageSize=15";
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", _config["ISBNdbKey"]);
         
                var response = await client.GetAsync(url);
         
                if (!response.IsSuccessStatusCode) return [];
         
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IsbnAuthorBooks>(content);

                return result != null ? result.books : [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return [];
            }
        }
    }
}
