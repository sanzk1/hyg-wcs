using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using jobcoreModule.Attributes;
using jobcoreModule.Bll;
using jobcoreModule.Controller;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace jobcoreModule;

public static class ModuleJobCore
{
    public static void AddJobCoreModule(this IServiceCollection services)
    {
        
        // services.AddControllers().AddApplicationPart(typeof(TestJobCoreController).Assembly);
        services.ConfigureDynamicProxy();

    }
    
    public static void UseInitializesJobCoreModule(this IApplicationBuilder app)
    {
        // app.ApplicationServices.GetService<ITaskInfoBll>().Initialized();
        
    }

    
}