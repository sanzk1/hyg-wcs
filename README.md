# Hyg-WCS

概述：基于RBAC设计的.NET 通用后台管理系统，前后的分离项目，后端代码采用模块化开发



## 系统架构

### 后端

- dotnet8
- webApi
- sqlSugar
- 数据库 postgreSQL
- quartz

### 前端

- pnpm
- vue3
- pinia
- vue-router
- ant-design-vue ui


### 系统模块
- 系统模块
- 数据点维护模块 (S7、OpcUa、ModbusTcp)
- 基础设施模块
- 定时任务模块
- webApi启动模块


## 快速启动

### 前端

1. npm install -g pnpm  (安装pnpm)
2. pnpm init -y (初始化pnpm，有package-lock.json文件则不需要初始化)
3. pnpm dev (直接启动)

### 后端

1. 数据库初始化创建对应数据库名称，执行sql脚本在doc文件夹下（先导入init-postgresql.sql，再导入后面sql脚本），再导入后更新webApi\sql文件夹下
2. 安装dotnet8环境
3. 修改appsettings.json配置数据库连接
4. 直接启动后端 dotnet run

### 初始账户

1.演示账户： demo 123456

2.管理员账户：admin 123456


## 截图
![image](https://github.com/user-attachments/assets/bb8cb21f-846c-4825-924c-9c4bedaade59)

![image](https://github.com/user-attachments/assets/2d54fa36-7787-4e5e-bb8c-b32fbe467b85)

![image](https://github.com/user-attachments/assets/6e09a0ab-a0b8-4d1d-8dbd-94878fd22e38)

### 笔记

虚拟机里生成项目，要设置网卡：勾选 桥接模式

下载安装postgresql-17.4-1-windows-x64
下载地址：https://www.enterprisedb.com/postgresql-tutorial-resources-training-1?uuid=69f95902-b451-4735-b7e4-1b62209d4dfd&campaignId=postgres_rc_17

安装postgresql，设置密码：123456
![设置密码](1.png)
![默认设置](2.png)
```
Installation Directory: C:\Program Files\PostgreSQL\17
Server Installation Directory: C:\Program Files\PostgreSQL\17
Data Directory: C:\Program Files\PostgreSQL\17\data
Database Port: 5432
Database Superuser: postgres
Operating System Account: NT AUTHORITY\NetworkService
Database Service: postgresql-x64-17
Command Line Tools Installation Directory: C:\Program Files\PostgreSQL\17
pgAdmin4 Installation Directory: C:\Program Files\PostgreSQL\17\pgAdmin 4
Stack Builder Installation Directory: C:\Program Files\PostgreSQL\17
Installation Log: C:\Users\san\AppData\Local\Temp\install-postgresql.log
```
下载postgresql UI工具 pgadmin4
https://www.pcsoft.com.cn/soft/211317.html

执行init-postgresql.sql 初始化数据库。
pgadmin4，先创建数据库wcs，
工具--查询工具--打开文件 init-postgresql.sql--执行

下载安装node.js
https://nodejs.org/zh-cn

安装pnpm
```
C:\Users\san>npm install -g pnpm

added 1 package in 16s

1 package is looking for funding
  run `npm fund` for details
npm notice
npm notice New major version of npm available! 10.9.2 -> 11.2.0
npm notice Changelog: https://github.com/npm/cli/releases/tag/v11.2.0
npm notice To update run: npm install -g npm@11.2.0
npm notice

C:\Users\san>
```

进入ui文件夹

初始化pnpm
输入：pnpm init
```
C:\Users\san>pnpm init -y
 ERROR  Unknown option: 'y'
For help, run: pnpm help init

C:\Users\san>pnpm init
Wrote to C:\Users\san\package.json

{
  "name": "san",
  "version": "1.0.0",
  "description": "",
  "main": "index.js",
  "scripts": {
    "test": "echo \"Error: no test specified\" && exit 1"
  },
  "keywords": [],
  "author": "",
  "license": "ISC",
  "packageManager": "pnpm@10.6.5"
}

C:\Users\san>

```

参数差异说明
工具	命令	行为
npm	npm init -y	非交互生成默认 package.json
pnpm	pnpm init	进入交互问答模式
pnpm	pnpm init -w	快速生成默认 package.json


修改 PowerShell 执行策略‌
‌步骤‌：
1. 以管理员身份打开 PowerShell（右键选择“以管理员身份运行”）‌26。
2. 执行以下命令放宽脚本执行权限：
```
Set-ExecutionPolicy RemoteSigned -Scope LocalMachine  # 全局生效‌:ml-citation{ref="3,5" data="citationList"}

```

若项目依赖 vite，全局安装或本地安装
```
pnpm add vite -D       # 本地安装‌:ml-citation{ref="8" data="citationList"}
```

npm init vue@latest
https://blog.csdn.net/weixin_69553582/article/details/129630880

进入ui文件夹
输入：npm install
将会下载生成node_modules文件夹，时间会很长

npm run dev (直接启动 前端)
在vs中运行项目，是启动后端


## network error
```
深度思考完成 用时17秒
好的，我需要帮助用户解决他们在使用正确账号密码登录时出现的“network error”问题。用户已经提供了他们的代码仓库地址：https://gitee.com/sanzk/hyg-wcs。我需要检查他们的代码，找出可能导致这个问题的原因。

首先，我应该从他们的仓库中查看相关的后端代码，特别是处理登录认证的部分。用户提到他们使用了JWT进行认证，所以我需要确认JWT的配置是否正确，包括密钥、颁发者（Issuer）和受众（Audience）是否在配置文件中正确设置。此外，还需要检查中间件的顺序，确保身份验证和授权的中间件在正确的位置，比如UseAuthentication应该在UseAuthorization之前调用。

接下来，跨域（CORS）配置也是一个常见的问题。用户可能没有正确配置CORS策略，导致前端请求被浏览器拦截。我需要查看他们的Startup.cs或Program.cs文件，确认CORS中间件的添加和配置是否正确，是否允许前端应用的域名、端口和方法。

然后，数据库连接和初始化也是一个关键点。如果用户使用了Code First的方式初始化数据库，但数据库表没有正确创建，或者连接字符串配置错误，会导致登录时无法验证用户信息，从而引发网络错误。我需要检查他们的数据库上下文配置，确认连接字符串是否正确，并且数据库迁移是否已经应用。

此外，全局异常处理中间件是否存在，如果没有的话，后端出现的异常可能没有被正确捕获和返回给前端，导致前端只能收到一个模糊的“network error”。我需要检查是否有自定义的异常处理中间件，并确认其是否正确处理了异常，并返回了有意义的错误信息。

还有，日志记录也是一个重要的排查工具。用户是否在关键部分（如登录处理、数据库查询）添加了日志输出，以便在出现问题时查看具体的错误信息。如果没有，建议他们添加详细的日志记录，帮助定位问题。

最后，前端代码的网络请求部分也需要检查。用户是否正确设置了请求的URL，是否处理了可能的错误响应，比如401未授权或500服务器错误。查看前端代码中发起登录请求的部分，确认请求头是否正确携带了Content-Type和Authorization等信息，以及错误处理逻辑是否完善。

综合以上几点，我需要逐步排查可能的配置错误、中间件顺序、数据库连接、异常处理和日志记录等方面，帮助用户找到导致“network error”的具体原因，并提供相应的解决方案。

```

一、关键问题分析（基于仓库代码）
1. ‌跨域配置不完整‌
‌代码位置‌: Program.cs 中未显式定义完整的 CORS 策略，仅调用了 app.UseCors()，但未指定允许的域名、请求头或方法。
‌问题表现‌: 浏览器拦截跨域请求，导致前端无法收到响应，触发 network error。
‌修复建议‌:
csharp
Copy Code
// 在 Program.cs 中添加以下配置
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// 中间件链中启用 CORS
app.UseCors("AllowAll");  // 必须在 UseRouting 之后、UseAuthentication 之前调用
2. ‌JWT 认证配置缺失‌
‌代码位置‌: appsettings.json 中缺少 Jwt:SecretKey 的配置，导致密钥验证失败。
‌问题表现‌: 后端无法生成有效 Token，但前端可能无法解析具体错误，直接显示 network error。
‌修复建议‌:
json
Copy Code
// appsettings.json 中添加
"Jwt": {
  "Issuer": "hyg-wcs",
  "Audience": "hyg-wcs-user",
  "SecretKey": "至少32位的强密钥（如 GUID）"
}
3. ‌数据库连接字符串未配置‌
‌代码位置‌: infrastructure/Db/DbClientFactory.cs 中硬编码了 ConnectionString，但未读取配置文件。
‌问题表现‌: 数据库连接失败，登录时无法查询用户信息，后端抛出异常但未处理。
‌修复建议‌:
csharp
Copy Code
// 修改 DbClientFactory.cs，从配置读取连接字符串
public class DbClientFactory {
    private readonly IConfiguration _config;
    public DbClientFactory(IConfiguration config) => _config = config;
    
    public SqlSugarClient GetSqlSugarClient() {
        return new SqlSugarClient(new ConnectionConfig {
            ConnectionString = _config.GetConnectionString("Default"),
            // 其他配置...
        });
    }
}
json
Copy Code
// appsettings.json 中添加
"ConnectionStrings": {
  "Default": "Server=.;Database=hyg-wcs;User Id=sa;Password=你的密码;"
}
4. ‌全局异常处理未覆盖认证模块‌
‌代码位置‌: webApi/Middlewares/ExceptionHandlingMiddleware.cs 存在，但未在 Program.cs 中注册。
‌问题表现‌: 认证失败时后端直接抛出异常，前端无法解析错误详情。
‌修复建议‌:
csharp
Copy Code
// 在 Program.cs 中添加中间件
app.UseMiddleware<ExceptionHandlingMiddleware>();  // 放在 UseRouting 之后
二、验证与调试步骤
1. ‌后端日志检查‌
启动后端时观察控制台输出，确认以下关键点：
log
Copy Code
[INF] 数据库已连接（ConnectionString 正确）
[INF] JWT 密钥已加载（SecretKey 不为空）
[INF] CORS 策略 AllowAll 已启用
2. ‌前端网络请求分析‌
打开浏览器开发者工具（F12），在 ‌Network‌ 标签中检查登录请求：
‌Request URL‌: 是否指向正确的后端地址（如 http://localhost:5000/api/auth/login）。
‌Status Code‌:
若为 401，检查 Token 生成逻辑。
若为 500，查看后端控制台的具体错误堆栈。
‌Response Headers‌: 确认包含 Access-Control-Allow-Origin: *。
3. ‌数据库初始化验证‌
在 Program.cs 中添加以下代码，确保表结构自动迁移：
csharp
Copy Code
using var db = app.Services.GetService<DbClientFactory>().GetSqlSugarClient();
db.DbMaintenance.CreateDatabase();  // 若数据库不存在则创建
db.CodeFirst.InitTables(typeof(SysUser), typeof(SysRole));  // 初始化核心表
三、最终修复方案
‌更新 appsettings.json‌

json
Copy Code
{
  "Jwt": {
    "Issuer": "hyg-wcs",
    "Audience": "hyg-wcs-user",
    "SecretKey": "替换为32位以上强密钥"
  },
  "ConnectionStrings": {
    "Default": "Server=.;Database=hyg-wcs;User Id=sa;Password=你的密码;"
  }
}
‌修改 DbClientFactory.cs‌

csharp
Copy Code
public class DbClientFactory {
    private readonly IConfiguration _config;
    public DbClientFactory(IConfiguration config) => _config = config;
    
    public SqlSugarClient GetSqlSugarClient() {
        return new SqlSugarClient(new ConnectionConfig {
            ConnectionString = _config.GetConnectionString("Default"),
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true
        });
    }
}
‌完善 Program.cs‌

csharp
Copy Code
// 添加 CORS 策略
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
// 注册中间件
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AllowAll");
完成以上修改后，重新启动前后端，90% 的 network error 问题将解决。若仍存在问题，可通过后端日志定位具体异常模块。


## JWT

```
// appsettings.json 中添加
"Jwt": {
  "Issuer": "hyg-wcs",
  "Audience": "hyg-wcs-user",
  "SecretKey": "至少32位的强密钥（如 GUID）"
}
```
在appsettings.json文件中添加JWT（JSON Web Tokens）配置是一个常见的做法，用于在应用程序中设置身份验证和授权的参数。上面是在appsettings.json文件中添加JWT配置的代码。

在这个配置中：

Issuer 是指发行者，即谁颁发的这个令牌。

Audience 是指令牌的受众，即令牌是给谁的。

SecretKey 是用于签名和验证令牌的密钥，应该是一个足够复杂和随机的字符串，以确保安全性。通常建议使用至少32位的强密钥，例如一个GUID。

请确保将"至少32位的强密钥（如 GUID）"替换为实际的强密钥。例如，你可以生成一个GUID作为密钥：
```
"SecretKey": "e0a1f85d-4e77-4e75-8e0e-3b8e28d3b1b1"
```
在实际部署时，务必保护好这个密钥，不要将其泄露，因为任何知道这个密钥的人都可以伪造令牌。

如果你需要在代码中访问这些配置，可以使用相应的配置框架或库来读取appsettings.json文件。例如，在.NET Core应用程序中，你可以使用IConfiguration接口来获取这些值。

```
public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}

// 在Startup.cs或其他配置类中
public void ConfigureServices(IServiceCollection services)
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
    // 然后可以使用jwtSettings对象来访问Issuer, Audience和SecretKey
}

```

如何验证JWT令牌？

验证JWT令牌通常涉及以下步骤：

    解析令牌：将JWT令牌从其字符串形式解析为可操作的格式，通常是一个包含头、有效载荷和签名部分的对象。

    验证签名：使用发行者提供的密钥（例如在appsettings.json中配置的SecretKey）来验证令牌的签名，以确保令牌在传输过程中没有被篡改。
    
    验证发行者（Issuer）：检查令牌的发行者字段是否与预期的发行者匹配。
    
    验证受众（Audience）：确保令牌是针对正确的受众颁发的。    

    验证有效期：检查令牌的过期时间（exp）和生效时间（nbf），确保令牌在有效期内。

    检查其他声明：根据需要，可能还需要验证令牌中的其他声明，如角色（roles）、权限（permissions）等。

以下是一个使用.NET Core和System.IdentityModel.Tokens.Jwt库验证JWT令牌的示例：
```
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenValidator
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _secretKey;

    public JwtTokenValidator(string issuer, string audience, string secretKey)
    {
        _issuer = issuer;
        _audience = audience;
        _secretKey = secretKey;
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudience = _audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero // Optional: set to zero to prevent any time skew
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return principal;
        }
        catch (SecurityTokenException ex)
        {
            // Token validation failed
            throw new Exception("Token validation failed.", ex);
        }
    }
}

```
在这个示例中，JwtTokenValidator类负责验证JWT令牌。ValidateToken方法接受一个令牌字符串，并返回一个ClaimsPrincipal对象，该对象包含令牌中的声明。如果验证失败，将抛出一个异常。

要使用这个验证器，你可以创建一个JwtTokenValidator实例，并调用ValidateToken方法，传入要验证的令牌：
```
var validator = new JwtTokenValidator("hyg-wcs", "hyg-wcs-user", "你的密钥");
var principal = validator.ValidateToken("你的JWT令牌字符串");

// 现在，你可以使用principal对象来访问令牌中的声明
var username = principal.Identity.Name;

```
请确保将"你的密钥"和"你的JWT令牌字符串"替换为实际的密钥和令牌。

这个示例使用了对称密钥进行签名验证。如果你的JWT令牌使用的是非对称密钥（如RSA），则需要相应地调整密钥的加载和验证参数。

# git

.gitignore文件添加一行，忽略.vs文件夹
    .vs
否则报错

