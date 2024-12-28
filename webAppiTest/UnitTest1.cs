namespace webAppiTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        int i = 1;
        Assert.True(i == 1);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void Test2(int i)
    {
        int sum = 0;
        sum += i;
        Assert.True(sum == 1);
    }

    [Fact]
    public async void Test3()
    {
        // HttpUtil httpUtil = new HttpUtil(_factory.Services.GetRequiredService<ILogger<HttpUtil>>()
        //     , _factory.Services.GetRequiredService<IHttpClientFactory>());
        // file
        MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent(); 
        HttpContent httpContent = new ByteArrayContent(File.ReadAllBytes("/home/hygge/图片/NET Roadmap.png"));
        multipartFormDataContent.Add(httpContent, "file", "NET Roadmap.png");
        var res = await PostFormDataAsAsync("http://localhost:5182/api/SysFileInfo/uploadFile", null,
            multipartFormDataContent);
        string responseContent = await res.Content.ReadAsStringAsync();
        Assert.True(true);
    }
    public async Task<HttpResponseMessage> PostFormDataAsAsync(string url, Dictionary<string, string>? headers, 
        MultipartFormDataContent content, int timeout = 30)
    {
        using var client = new HttpClient();
        if (headers is not null)
        {
            foreach (var keyValuePair in headers)
            {
                client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
        client.Timeout = TimeSpan.FromSeconds(timeout);
        return await client.PostAsync(new Uri(url), content);
    }
    
}