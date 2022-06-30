using APIs.Model;
using System.Text;
using Newtonsoft.Json;

namespace RestTest;
public class BooksTest {

    String url = "https://localhost:7201/books";

    [Fact]
    public async Task<List<Book>> getAllBooks() {
        List<Book>? jsonList = new List<Book>();
        var client = new HttpClient();
        var request = new HttpRequestMessage();
        request.RequestUri = new Uri(url);
        request.Method = HttpMethod.Get;

        request.Headers.Add("Accept", "*/*");
        request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

        var response = await client.SendAsync(request);
        var result = response.IsSuccessStatusCode;
        if (result) { 
            jsonList = JsonConvert.DeserializeObject<List<Book>>(await response.Content.ReadAsStringAsync());
        }
        Assert.True(result);
        Console.WriteLine(result);
        return jsonList;
    }

    [Fact]
    public async Task getBook() {
        List<Book> books = await getAllBooks();
        var countSuccesful = 0;

        foreach (Book book in books) {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"{url}/{book.Id}");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var response = await client.SendAsync(request);
            var result = response.IsSuccessStatusCode;
            if (result) { countSuccesful++; }
        }
        var boolean = (countSuccesful == books.Count);
        Assert.True(boolean);
    }
    [Fact]
    public async Task addBook() {
        
        var countSuccesful = 0;
        for (int i = 0; i < 20; i++) {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Post;
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var bodyString = "{    \"title\": \"1984\",    \"author\": \"George Orwell\",    \"synopsis\": \"Una distopia\",    \"genre\": \"Sci-Fi\",    \"cover\": \"cover\",    \"path\": \"path\"}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = response.IsSuccessStatusCode;
            if (result) { countSuccesful++; }
        }
        var boolean = (countSuccesful == 20);
        Assert.True(boolean);
        
    }

    [Fact]
    public async Task deleteBook() {
        List<Book> books = await getAllBooks();
        var countSuccesful = 0;

        foreach (Book book in books) {
            Console.WriteLine(book.Id);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"{url}?id={book.Id}");
            request.Method = HttpMethod.Delete;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var response = await client.SendAsync(request);
            var result = response.IsSuccessStatusCode;
            if (result) { countSuccesful++; }
        }
        var boolean = (countSuccesful == books.Count);
        Assert.True(boolean);
    }
    [Fact]
    public async Task updateBook() {
        List<Book> books = await getAllBooks();
        var countSuccesful = 0;

        foreach (Book book in books)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"{url}/{book.Id}?title=Mujercitas&author=Louisa%20May%20Alcott&synopsis=Little%20women&genre=Classics&cover=cover&path=path ");
            request.Method = HttpMethod.Put;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var response = await client.SendAsync(request);
            var result = response.IsSuccessStatusCode;
            if (result) { countSuccesful++; }
        }
        var boolean = (countSuccesful == books.Count);
        Assert.True(boolean);
    }
}