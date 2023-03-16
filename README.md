# Demo.MicroService平台产品线开发架构说明文档
+ 整体采用简洁的CRUD多层架构风格；
+ 每个服务使用目录分层即不再新建类库；
+ 消息队列RabbitMQ 
+ 分布式日志 ： EFK(Elasticsearch+Filebeat+Kibana)
+ 链路&性能 ：skywalking
+ ORM : Sqlsugar
+ 分布式配置中心服务：Nacos
+ 鉴权中心：JWT和identityserver4
+ 网关：ocelot
+ 服务注册：consul和nacos
+ 服务治理：polly
+ 服务通信：http通信，采用json格式
+ 部署：docker-compose 多环境都可以
+ 分布式事务：CAP


### :mushroom: 整体介绍


### src目录
+ src目录为源码存放位置，零售产品线每个服务都保存在此目录中，彼此为平行关系；
+ 服务命名规范：Demo.MicroService.{ServiceName}Api，其中ServiceName为服务名称，如：Demo.MicroService.CustomerApi；


### test目录
+ test目录为单元测试存放目录，每个服务对应一个单元测试项目；
+ 单元测试命名规范：Demo.MicroService.{ServiceName}Api.Test，其中ServiceName为服务名称，如：Demo.MicroService.CustomerApi.Test；


### :mushroom: 目录结构
#### Repositories 仓储层
+ Context 数据库SqlSugarClient
+ Demo.MicroService.Repository.Repository 仓储实现类
+ DTO和Model 数据映射采用ValueInjection
+ 仓储接口统一存放到Demo.MicroService.Repository.IRepository目录中
#### Entities 实体层
+ 实体统一存放到Demo.MicroService.BusinessModel，不再使用目录区分；
#### Services 业务逻辑层
+ Demo.MicroService.BusinessDomain.Server 业务层实现类
+ 业务层接口统一存放到Demo.MicroService.BusinessDomain.IServer目录中
#### Models视图模型层（存放数据传输对象）
+ Demo.MicroService.BusinessModel.Dto 存放数据传输对象
+ Dto子目录为不同实体对应的DTO对象，目录名称推荐使用实体类名称；
#### Controllers Api接口
存放服务中所有的Controller，不再使用目录区分；
#### ServiceSetup
Program中存放应用启动所需配置项，一般来讲文件相对固定，如下：

#### :mushroom:  快速开始

##### 接入分布式日志系统
  - 安装Microsoft.Extensions.Logging.Log4Net.AspNetCore包（内部nuget）
  - 在根目录中添加log4net.config，右键属性设置“如果较新则复制“
  - Program中添加以下设置
```js
    //...
    public void ConfigureServices(IServiceCollection services)
    {
        builder.Logging.AddLog4Net();
    }
    
    //...
```
##### 日志的配置文件log4net.config
```js
<?xml version="1.0" encoding="utf-8" ?>
<log4net>
  <!--源码地址：https://github.com/apache/logging-log4net-->
  
  <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender">
    <file type="log4net.Util.PatternString">
      <converter>
        <name value="hostname" />
        <type value="Demo.MicroService.Core.HostnamePatternConverter, Demo.MicroService.Core" />
      </converter>
      <conversionPattern value="logs/%hostname-" />
    </file>
    <appendToFile value="true" />
    <rollingStyle value="Date" />
    <datePattern value="yyyyMMdd&quot;.log&quot;" />
    <staticLogFileName value="false" />
    <layout type="Demo.MicroService.Core.LocalDiskLogLayout,Demo.MicroService.Core">
      <conversionPattern value="%date [%thread] %-5level %logger - %message%newline" />
      <appid value="UserMicroservice" />
      <!--<IncludeServerContext value="true" />
      <IncludeAppContext value="true" />
      <IncludeHttpContext value="true" />
      <IncludeProcessContext value="true" />-->
    </layout>
  </appender>
  <appender name="KafkaAppender" type="Demo.MicroService.Core.KafkaAppender, Demo.MicroService.Core">
    <KafkaSettings>
      <broker value="192.168.11.49:9092" />
      <topic value="log-elk" />
    </KafkaSettings>
    <layout type="Demo.MicroService.Core.KafkaLogLayout,Demo.MicroService.Core." >
      <appid value="UserMicroservice" />
    </layout>
  </appender>
  <appender name="MongodbAppender" type="Demo.MicroService.Core.MongodbAppender, Demo.MicroService.Core.">
    <MongodbSettings>
      <MongodbServer value="mongodb://192.168.11.49:27017" />
      <DbName value="handday_logs" />
    </MongodbSettings>
    <layout type="Demo.MicroService.Core.MongodbLogLayout,Demo.MicroService.Core." >
      <appid value="UserMicroservice" />
    </layout>
  </appender>
  <root>
    <level value="All"/>
    <appender-ref ref="RollingFileAppender"/>
  </root>
</log4net>



```
##### 打印日志
  - 引入 Microsoft.Extensions.Logging
  - 注入ILogger
  - 使用示例如下：
  ```js
    /// <summary>
    /// ILogger
    /// </summary>
    private readonly ILogger<UserController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("pagelist")]
    public async Task<IPagedList<UserDto>> GetPageList(int pageIndex = 1, int pageSize = 15)
    {
        //记录日志信息
        _logger.LogInformation("enter pagelist method...");
    }
  ```
#### :mushroom: 添加 SqlSugarCore
- 添加Demo.MicroService.Core项目引用；
#### :mushroom: 接入统一身份认证中心
- 服务器安装Microsoft.AspNetCore.Authentication.JwtBearer包；
- 添加JWT支持： builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>{});
- 在控制器（Controller）添加 [Authorize]特性；
- 在添加身份认证时，默认已经注入了以下内容
  > builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
  > builder.Services.AddScoped<IUser, AspNetUser>();
- 如何获取当前用户的信息？
  > 在需要获取用户信息类中使用ApplicationContext即可；
#### :mushroom: 使用Swagger生成在线接口文档
+ 添加builder.AddSwaggerGenExt();和app.UseSwaggerExt();
+ 多版本控制：在controller上面添加 [ApiVersion("2")]

#### :mushroom:  服务通信（EventBus事件订阅与消费）
##### 接入方式
+ 服务通信借助开源中间件CAP实现（RabbitMQ&MongoDB）; 
+ 安装包：DotNetCore.CAP&DotNetCore.CAP.MongoDB&DotNetCore.CAP.RabbitMQ
+ 进行以下设置&初始化
```js
    services.AddCap(option =>
    {
        option.UseRabbitMQ(config =>
        {
            //以下为读取本地配置
            config.HostName = Configuration["CAP:RabbitMQ:Host"];
            config.VirtualHost = Configuration["CAP:RabbitMQ:VirtualHost"];
            config.Port = Convert.ToInt32(Configuration["CAP:RabbitMQ:Port"]);
            config.UserName = Configuration["CAP:RabbitMQ:UserName"];
            config.Password = Configuration["CAP:RabbitMQ:Password"];
            //以下为获取配置中心设置
            //config.HostName = capConfig.Host;
            //config.VirtualHost = capConfig.VirtualHost;
            //config.Port = capConfig.Port;
            //config.UserName = capConfig.UserName;
            //config.Password = capConfig.Password;
        });

        option.FailedRetryCount = 3;
        option.FailedRetryInterval = 5;
    });
```
##### 事件发布&订阅消费
发送消息
```js
    private readonly ICapPublisher _eventBus;
    public HomeController()
    {
        _eventBus = EventBusConfig.Services.ServiceProvider.GetRequiredService<ICapPublisher>();
    }   
    //注意事项：假如有其他数据保存，cap使用必须与数据库事务保持一致；
    //建议使用封装的cap推送方式
    _eventBus.PublishAsync(_unitOfWorkManager.Current, "test.services.update.user", "dd"); 
```
接收消息
```js
    public class SubscribeServiceSample : ICapSubscribe
    {
        [CapSubscribe("test.services.update.user")]
        public string ReceivedMessage(string username)
        {
            Console.WriteLine(username);
            return username;
        }
    }
```
关于CAP的更多使用及设置请参考官方文档。传送：https://github.com/dotnetcore/CAP
#### :mushroom: API返回值统一格式化
+ 接入：services.AddControllers().AddMvcApiResult();
+ 增、删、改操作推荐返回ResponseResult类型；
+ 列表（不分页）推荐返回ResponseResult类型; 
+ 列表（分页）推荐需要另外定义一个类暂未实现; 
+ 框架会自动帮我们进行格式化：
```json
  {
	"DataResult": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidHNldCIsIkVNYWlsIjoic3RyaW5nIiwiVXNlcklEIjoiZmRhZTQwNjItNDIwMC00MDBmLWJiNGEtMjI5ZGFlM2Y2ODBjIiwiTW9iaWxlIjoic3RyaW5nIiwiTVRlbmFudElEIjoiZTAxNzllN2QtMTM0ZC00OGZjLWE2OTEtNjk4MDc5OGI1MDczIiwibmJmIjoxNjc4OTMwODY5LCJleHAiOjE2Nzg5MzE0NjksImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTcyNiIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTcyNiJ9.0PcETn67t4KmeGt5VAGaP5fn4NZz23YAO_oadZQsCpU",
	"StatusCode": 200,
	"IsSuccess": true,
	"Value": null,
	"OtherValue": null,
	"Message": "Token颁发成功"
  }
```
+ 判断请求成功与否，请使用statusCode，只有statusCode等于200时才表示正常请求成功；
+ statusCode遵守标准HTTP状态码；
#### :mushroom: 统一异常处理
+ 统一异常处理功能
+ 添加全局属常自动处理功能
  > app.ExceptionHandlerMiddlewareExtensions();

#### :mushroom: 链路跟踪&性能监控
+ DEMO暂未实现，实现思路就是：搭建skywalking环境，然后添加skyapm.json配置文件，然后nuget包SkyAPM.Agent.AspNetCore。注册builder.Services.AddSkyApmExtensions(); // 添加Skywalking相关配置
#endregion 

#### :mushroom: 配置中心
安装：nacos-sdk-csharp.AspNetCore和nacos-sdk-csharp.Extensions.Configuration
+ 登陆nacos新增命名空间
+ 在命名空间只能添加配置
+ 在Program中接入

```js
	#region Nacos配置
	// 注册服务到Nacos
	builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //默认节点Nacos
	//builder.Host.UseNacosConfig(section: "NacosConfig");
	builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
	#endregion
```

+ 在程序中通过IConfiguration读取配置
```js
var capConfig = System.Text.Json.JsonSerializer.Deserialize<MicroServiceCommon.ServiceSetup.CAP.RabbitMQ>(Configuration.GetSection("CAP").Value);
```

+ 在appsettings.json添加配置
```json
  "NacosConfig": {
    "Listeners": [
      {
        "Optional": false,
        "DataId": "UserMicroservice",
        "Group": "DEFAULT_GROUP"
      }
    ],
    "Optional": false,
    "Namespace": "Demo_MicroService",
    "ServerAddresses": [ "http://192.168.1.9:8848/" ],
    "ServiceName": "UserMicroservice"
  }
```
+ nacos服务发现的添加配置
```json
  "nacos": {
    "ServerAddresses": [ "http://192.168.1.9:8848" ],
    "DefaultTimeOut": 15000,
    "Namespace": "Demo_MicroService", 
    "ListenInterval": 1000,
    "ServiceName": "UserService",
    "GroupName": "DEFAULT_GROUP",
    "ClusterName": "DEFAULT",
    "RegisterEnabled": true,
    "InstanceEnabled": true,
    "Ephemeral": true,
    "ConfigUseRpc": true,
    "NamingUseRpc": true,
    "LBStrategy": "WeightRoundRobin" 
  }
```
#### :mushroom: 文件存储（待设计）

#### :mushroom: 开发规范（完善中）
##### 命名规则
+ 【强制】统一代码格式，必须处理完VS编辑器内警告及以上提示； 
+ 【强制】统一代码格式，cs单文件代码行数不超过1000；函数参数个数不超过4；单行代码不宜过长，主动折行； 
+ 【强制】代码中的命名严禁使用拼音与英文混合的方式，更不允许直接使用中文的方式。 
说明：正确的英文拼写和语法可以让阅读者易于理解，避免歧义。注意，即使纯拼音命名方式 也要避免采用。
+ 【强制】类名使用 UpperCamelCase 风格，必须遵从驼峰形式。
+ 【强制】参数名、成员变量、局部变量都统一使用 lowerCamelCase 风格，必须遵从 驼峰形式。
+ 【强制】常量命名全部大写，单词间用下划线隔开，力求语义表达完整清楚，不要嫌名字长。 正例：MAX_STOCK_COUNT
+ 【强制】杜绝完全不规范的缩写，避免望文不知义。 
反例：AbstractClass“缩写”命名成 AbsClass；condition“缩写”命名成 condi，此类随 意缩写严重降低了代码的可阅读性。
+ 【强制】枚举类名建议带上 Enum 后缀，枚举成员名称驼峰规则
正例：枚举名字为 ProcessStatusEnum 的成员名称：Success/ UnkwonReason。
+ 【强制】各层命名规约： 
+  A) Controller 层方法命名规约 
+ 采用restful风格命名
+ 1） 获取单个对象的方法用 get/{id} 格式，例如：api/user/get/1。 
+ 2） 获取多个对象或者分页的方法用 list 格式，例如：api/user/list。
+ 3） 插入的方法用 add格式，例如：api/user/add。 
+ 4） 删除的方法用 delete/{id} 格式，例如：api/user/delete/1。 
+ 5） 修改的方法用 update 格式，例如：api/user/update。 
+ 6） 如果出现多个单词时，例如：api/externalcontact/get_unassigned_list
+ B) Services/Repository 层方法命名规约 
+ 1） 获取单个对象的方法用 Get 做前缀，例如：GetUser。 
+ 2） 获取多个对象的方法用 GetXXXList 格式，例如：GetUserList。
+ 3） 获取分页数据的方法用GetXXXPagedList格式，例如：GetUserPagedList 
+ 3） 获取统计值的方法用 Count 做后缀，例如：GetUserCount。 
+ 4） 插入的方法用 Add 做前缀。 
+ 5） 删除的方法用 Delete 做前缀。 
+ 6） 修改的方法用 Update 做前缀。 

##### 异常日志

##### 单元测试

##### 数据库
###### 建库规则
###### 建表规则
###### 索引规则
###### 语句规则

