using System.Text;
using domain.Enums;
using domain.Pojo.ortherSystems;
using domain.Result;
using infrastructure.Attributes;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using otherSystemModule.Dal;

namespace otherSystemModule.Bll.Impl;


[Service(ServiceLifetime.Singleton)]
public class InterfaceRequestConfigBll : IInterfaceRequestConfigBll
{
    private readonly IInterfaceRequestConfigDal _interfaceRequestConfigDal;
    private readonly HttpUtil _httpUtil;
    private readonly ILogger<IInterfaceRequestConfigBll> _logger;

    public InterfaceRequestConfigBll(IInterfaceRequestConfigDal interfaceRequestConfigDal, HttpUtil httpUtil, ILogger<IInterfaceRequestConfigBll> logger)
    {
        _interfaceRequestConfigDal = interfaceRequestConfigDal;
        _httpUtil = httpUtil;
        _logger = logger;
        
    }


    public ApiResult Execute(long configId, object param)
    {
        InterfaceRequestConfig requestConfig = _interfaceRequestConfigDal.SelectById(configId);


        return ApiResult.succeed();
    }

    private HttpResponseMessage execute(InterfaceRequestConfig requestConfig, object param)
    {
        bool isJson = !string.IsNullOrEmpty(requestConfig.requestBody);
        Dictionary<string, string> headers = new();
        headers.Add(requestConfig.authTypeKey, requestConfig.authCredentials);
        string url = requestConfig.requestUrl;
        if (param is not null &&  isJson)
        {
            Dictionary<string, object> parameters = param as Dictionary<string, object>;
            if (parameters is not null)
            {
                StringBuilder sb = new();
                sb.Append(url);
                sb.Append("?");
                foreach (var item in parameters)
                {
                    sb.Append($"{item.Key}={item.Value}&");
                }
                url = sb.ToString();
            }
            
        }
        // todo 表单提交、url、json待封装
        switch (requestConfig.requestMethod)
        {
            case HttpMethodEnum.GET:
               return _httpUtil.Get(url, headers);
                break;
            case HttpMethodEnum.POST:
                if (isJson)
                    return _httpUtil.PostAsJsonAsync(requestConfig.requestUrl, headers, param).GetAwaiter().GetResult();
                else
                return _httpUtil.PostAsJsonAsync(requestConfig.requestUrl, headers, param).GetAwaiter().GetResult();
                break;
            case HttpMethodEnum.PUT:
                break;
            case HttpMethodEnum.DELETE:
                break;
            default:
                break;
        }
        return null;
    }
}