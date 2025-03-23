
using dataPointsModule.Bll;
using dataPointsModule.Managers;
using domain.Dto;
using domain.Pojo.jobCore;
using infrastructure.Attributes;
using infrastructure.Exceptions;
using jobcoreModule.Attributes;
using jobcoreModule.Bll;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace jobcoreModule.Core.Block.Impl;


[Service(ServiceLifetime.Singleton)]
public class S7Block : IS7Block
{
    private readonly ILogger<S7Block> logger;
    private readonly IS7DataPointBll s7DataPointBll;
    private readonly IBlockLogBll blockLogBll;
    private readonly IS7Manager manager;
    private readonly JobCoreContext jobCoreContext;
    
    public S7Block(ILogger<S7Block> logger, IS7DataPointBll s7DataPointBll, IS7Manager manager,
        IBlockLogBll blockLogBll, JobCoreContext jobCoreContext)
    {
        this.logger = logger;
        this.s7DataPointBll = s7DataPointBll;
        this.manager = manager;
        this.blockLogBll = blockLogBll;
        this.jobCoreContext = jobCoreContext;

    }
    [BlockLog]
    [Monitoring]
    public DataPointDto Read(string id, string name)
    {
        BlockLog log = blockLogBll.GetByTaskIdAndBId(Thread.CurrentThread.Name, id);
        DataPointDto dto = s7DataPointBll.ReadByName(name);
        if (dto.state)
        {
            log.value = Convert.ToString(dto.value);
            log.txt = $"读取-{name}-数据点，值：{log.value}";
            log.executeStatus = true;
            log.updateTIme = DateTime.Now;
            blockLogBll.Update(log);
        }
        else
        {
            log.executeStatus = false;
            log.txt = $"读取-{name}-数据点失败，值：{log.value}";
            log.updateTIme = DateTime.Now;
            blockLogBll.Update(log);
            throw new BusinessException(dto.msg);
        }
        return dto;
    }

    [BlockLog]
    [Monitoring]
    public void Write(string id, string name, object value)
    {
        BlockLog log = blockLogBll.GetByTaskIdAndBId(Thread.CurrentThread.Name, id);
        DataPointDto dto = s7DataPointBll.WriteByName(name, value);
        if (dto.state)
        {
            log.value = Convert.ToString(dto.value);
            log.txt = $"写入-{name}-数据点，值：{log.value}";
            log.executeStatus = true;
            log.updateTIme = DateTime.Now;
            blockLogBll.Update(log);
        }
        else
        {
            log.executeStatus = false;
            log.txt = $"写入-{name}-数据点失败，值：{log.value}";
            log.updateTIme = DateTime.Now;
            blockLogBll.Update(log);
            throw new BusinessException(dto.msg);
        }
    }
    
    [BlockLog]
    [Monitoring]
    public bool WhileRead(string id, string name, object value)
    {
       
        DataPointDto dto = s7DataPointBll.ReadByName(name);
        while (dto.state && !dto.value.Equals(Convert.ToString(value)))
        {
            dto = s7DataPointBll.ReadByName(name);
            BlockLog log1 = blockLogBll.GetByTaskIdAndBId(Thread.CurrentThread.Name, id);
            string v = Convert.ToString(dto.value);
            if (!log1.value.Equals(v))
            {
                log1.value = v;
                log1.txt = $"{log1.txt} \\r \\n 读取-{name}-数据点，值：{v}";
                log1.updateTIme = DateTime.Now;
                blockLogBll.Update(log1);
            }
            Thread.Sleep(500);
            jobCoreContext.Monitor(Thread.CurrentThread.Name);
        }
        BlockLog log = blockLogBll.GetByTaskIdAndBId(Thread.CurrentThread.Name, id);
        if (!dto.state)
        {
            log.executeStatus = false;
            log.txt = $"读取-{name}-数据点失败，值：{log.value}";
            log.updateTIme = DateTime.Now;
            blockLogBll.Update(log);
            throw new BusinessException(dto.msg);
        }
        return dto.state;
    }
    
    
    
}