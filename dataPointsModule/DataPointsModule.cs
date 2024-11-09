﻿using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using dataPointsModule.Attributes;
using dataPointsModule.Bll;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace dataPointsModule;

public static class DataPointsModule
{

    public static void AddDataPonitsModule(this IServiceCollection services)
    {
        
    }

    public static void UseInitializesDataPointsModule(this IApplicationBuilder app)
    {
        app.ApplicationServices.GetService<IS7DataPointBll>().Initializes();
    }
    
    
}