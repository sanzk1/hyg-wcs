using domain.Enums;
using SqlSugar;

namespace domain.Pojo.ortherSystems;

/// <summary>
/// 接口请求配置表
/// </summary>
public class InterfaceRequestConfig
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long configId { set; get; }
    /// <summary>
    /// 请求接口名称
    /// </summary>
    public string interfaceName { set; get; } = string.Empty;
    /// <summary>
    /// 请求方法
    /// </summary>
    public HttpMethodEnum requestMethod { set; get; } = HttpMethodEnum.POST;
    /// <summary>
    /// 请求url
    /// </summary>
    public string requestUrl { set; get; } = string.Empty;
    /// <summary>
    /// 请求参数body示例
    /// </summary>
    public string requestBody { set; get; } = string.Empty;
    /// <summary>
    /// 授权请求头key
    /// </summary>
    public string authTypeKey { set; get; } = string.Empty;
    /// <summary>
    /// 授权令牌
    /// </summary>
    public string authCredentials { set; get; } = string.Empty;

    public bool isDelete { set; get; } = false;
    
    public DateTime createTime { set; get; } = DateTime.Now;
    
    public DateTime updateTime { set; get; } = DateTime.Now;
    
}