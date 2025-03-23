using infrastructure.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace webAppiTest;

public class WebApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WebApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_Upload_File()
    {
        HttpUtil httpUtil = new HttpUtil(_factory.Services.GetRequiredService<ILogger<HttpUtil>>()
            , _factory.Services.GetRequiredService<IHttpClientFactory>());
        // file
        MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent(); 
        HttpContent httpContent = new ByteArrayContent(File.ReadAllBytes("/home/hygge/图片/NET Roadmap.png"));
        multipartFormDataContent.Add(httpContent, "file", "NET Roadmap.png");
        var res = await httpUtil.PostFormDataAsAsync("http://localhost:5182/api/SysFileInfo/uploadFile", null,
            multipartFormDataContent);
        string responseContent = await res.Content.ReadAsStringAsync();
        Assert.Equal("text/html; charset=utf-8", "ok");
    }

}