using APIs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestTest;

public class UsersTest {

    HttpClient client;
    HttpRequestMessage request;
    String url = "https://localhost:7201/users";

    [Fact]
    public async Task<List<User>> getAllUsers() {
        List<User>? jsonList = new List<User>();
        client = new HttpClient();
        request = new HttpRequestMessage();
        request.RequestUri = new Uri(url);
        request.Method = HttpMethod.Get;

        request.Headers.Add("Accept", "*/*");
        request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

        var response = await client.SendAsync(request);
        var result = response.IsSuccessStatusCode;
        if (result) {
            jsonList = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
        }
        Assert.True(result);
        return jsonList;
    }

    [Fact]
    public async Task getUser()
    {
        List<User> users = await getAllUsers();
        var countSuccesful = 0;

        foreach (User user in users)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"{url}/{user.Id}");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var response = await client.SendAsync(request);
            var result = response.IsSuccessStatusCode;
            if (result) { countSuccesful++; }
        }
        var boolean = (countSuccesful == users.Count);
        Assert.True(boolean);
    }
    [Fact]
    public async Task addUser()
    {

        var countSuccesful = 0;
        for (int i = 0; i < 20; i++)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Post;
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var bodyString = "{  \"username\": \"Fer\",  \"password\": \"power\",  \"email\": \"payon@gmail.com\",  \"role\": 2}";
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
    public async Task deleteUser()
    {
        List<User> users = await getAllUsers();
        var countSuccesful = 0;

        foreach (User user in users)
        {
            Console.WriteLine(user.Id);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"{url}?id={user.Id}");
            request.Method = HttpMethod.Delete;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var response = await client.SendAsync(request);
            var result = response.IsSuccessStatusCode;
            if (result) { countSuccesful++; }
        }
        var boolean = (countSuccesful == users.Count);
        Assert.True(boolean);
    }
    [Fact]
    public async Task updateUser()
    {
        List<User> users = await getAllUsers();
        var countSuccesful = 0;

        foreach (User user in users) {

            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"https://localhost:7201/users/{user.Id}?username=Eli&password=limoncito&email=fer%40gmail.com ");
            request.Method = HttpMethod.Put;
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            var response = await client.SendAsync(request);
            var result = response.IsSuccessStatusCode;
            if (result) { countSuccesful++; }
            
        }
        var boolean = (countSuccesful == users.Count);
        Assert.True(boolean, $"{countSuccesful}/{users.Count}");
    }
}
