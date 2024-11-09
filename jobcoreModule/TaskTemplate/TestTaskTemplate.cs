using infrastructure.Attributes;
using jobcoreModule.Attributes;
using jobcoreModule.Core.Block;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace jobcoreModule.TaskTemplate;


[Service(ServiceLifetime.Singleton)]
public class TestTaskTemplate : ITaskTemplate
{

    private readonly IS7Block S7Block;
    private readonly IBaseBlock baseBlock;

    public TestTaskTemplate(ILogger<TestTaskTemplate> logger, IS7Block s7Block, IBaseBlock baseBlock)
    {
        this.S7Block = s7Block;
        this.baseBlock = baseBlock;
    }
    public void Test1( string taskId)
    {
        Console.WriteLine("测试方法执行打印方法" + taskId);
        
    }
    
    [Monitoring]
    public void test(string value)
    {
        int blockId = 0;
        baseBlock.Print((++blockId).ToString(), "任务开始》》》》》》");
        var dataPointDto = S7Block.Read((++blockId).ToString(), "测试数据点1");
        baseBlock.Print((++blockId).ToString(), "任务结束打印》》》》》》" + dataPointDto.value);
    }
}