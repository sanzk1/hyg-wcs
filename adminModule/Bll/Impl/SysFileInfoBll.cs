using System.Transactions;
using adminModule.Dal;
using domain.Pojo.config;
using domain.Pojo.sys;
using infrastructure.Attributes;
using infrastructure.Exceptions;
using infrastructure.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yitter.IdGenerator;

namespace adminModule.Bll.Impl;

[Service(ServiceLifetime.Singleton)]
public class SysFileInfoBll : ISysFileInfoBll
{
    private readonly string baseFilePath;
    private readonly ISysFileInfoDal _sysFileInfoDal;
    private readonly BaseSetting _baseSetting;
    private readonly  ILogger<SysFileInfoBll> _logger;

    public SysFileInfoBll(ISysFileInfoDal sysFileInfoDal, ILogger<SysFileInfoBll> logger, IOptions<BaseSetting> BaseSetting, IWebHostEnvironment environment)
    {
        _sysFileInfoDal = sysFileInfoDal;
        _logger = logger;
        _baseSetting = BaseSetting.Value;
        baseFilePath = Path.Combine(environment.ContentRootPath, _baseSetting.filePath);
    }
    
    [TransactionScope(TransactionScopeOption.Suppress)]
    public string SaveFileInfo(IFormFile file)
    {
        SysFileInfo fileInfo = new SysFileInfo();
        fileInfo.id = YitIdHelper.NextId();
        fileInfo.fileName = Path.GetFileName(file.FileName);
        fileInfo.suffix = Path.GetExtension(file.FileName);
        fileInfo.fileType = FileUtil.GetIFormFileType(fileInfo.suffix);
        fileInfo.size = file.Length;
        fileInfo.path = FileUtil.CreatedDirPath(baseFilePath);
        try
        {
            FileUtil.SaveFileLocal(fileInfo.path, fileInfo.fileName, file);
        }
        catch (Exception ex)
        {
            _logger.LogError($"保存文件失败，原因：{ex.Message}");
            throw new BusinessException("保存文件失败");
        }
        string urlPre = fileInfo.path.Replace(baseFilePath, string.Empty);
        fileInfo.url = $"{_baseSetting.baseUrl}{urlPre}/{fileInfo.fileName}";
        
        _sysFileInfoDal.Insert(fileInfo);

        return fileInfo.url;
    }
    
   
    
}