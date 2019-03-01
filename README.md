# 基于SqlSugar框架针对Mysql数据库开发的DDD模型的ASP.NET MVC5快速开发框架

#### 项目结构：
* `Quick.Models`：数据层，包括领域驱动模型中的Application、Domain、Driver
* `Quick.Common`：公共层，包括AutoMapper、Hangfire的配置等
* `Quick.IService`：业务逻辑层接口
* `Quick.Service`：业务逻辑层
* `QuickWeb`：UI表现层

#### 项目说明
`master`分支是基于ASP.NET MVC5 + SqlSuagr(数据库为：MySql，也可以自行切换到SQL Server数据库，更多配置详情请参考 [SqlSugar4文档](http://www.codeisbug.com/Doc/8/) )的快速开发框架，`snake`分支( 如有兴趣课移步：[snake快速后台](https://github.com/Run2948/Myql.QuickWeb/tree/snake) )附有一个完整的管理系统。
本项目旨在为大家提供一个可用的便捷的后台系统。
* 武汉一世计科软件有限公司 &copy;2017 - 2019  &nbsp;&nbsp;&nbsp;&nbsp; 联系QQ：1004850985

#### 关键技术
* 1.Asp.Net Mvc 5.2.7
* 2.SqlSugar
* 3.AutoFac
* 4.AutoMapper
* 5.log4net
* 6.System.ValueTuple
* 7.StackExchange.Redis
* 8.Hangfire

### 注意事项
* 1.`StackExchange.Redis`的版本必须保持为`1.2.6`，升降版本会导致RedisHelper报错
* 2.`RestSharp`的版本建议保持为`105.2.3`，升级版本会引入冗余依赖
* 3.`MySql.Data`的版本建议保持为`6.10.8`，最新版本8.0.15会引入冗余依赖
* 4.`Hangfire.MySqlStorage`的版本建议保持为`1.0.7`，最新版本2.0.0会引入冗余依赖
* 5.`SqlSugar`的源码中对`MySql.Data.dll 6.2.1.0`版本做了强引用，如果使用T4模板生成实体类，需要引入当前工程中`lib`目录下的`MySql.Data.dll 6.2.1.0`
* 6.[layer弹层遮罩挡住窗体解决](https://blog.csdn.net/q646926099/article/details/78797091)

* #### 优秀博客
* 1.[使用SqlSugar封装的数据层基类](https://www.cnblogs.com/bfyx/p/9125002.html)