# 基于SqlSugar框架针对Mysql数据库开发的DDD模型的ASP.NET MVC5快速开发框架

#### 项目结构：
* `Quick.Models`：数据层，包括领域驱动模型中的Application、Domain、Driver
* `Quick.Common`：公共层，包括AutoMapper、Hangfire的配置等
* `Quick.IService`：业务逻辑层接口
* `Quick.Service`：业务逻辑层
* `QuickWeb`：UI表现层

#### 关键技术
* 1.Asp.Net Mvc 5.2.7
* 2.SqlSugar
* 3.AutoFac
* 4.AutoMapper
* 5.log4net
* 6.System.ValueTuple
* 7.StackExchange.Redis
* 8.Hangfire
* 9.FluentScheduler

### 注意事项
* 1.`StackExchange.Redis`的版本必须保持为`1.2.6`，升降版本会导致RedisHelper报错
* 2.`RestSharp`的版本建议保持为`105.2.3`，升级版本会引入冗余依赖
* 3.`MySql.Data`的版本建议保持为`6.10.8`，最新版本8.0.15会引入冗余依赖
* 4.`Hangfire.MySqlStorage`的版本建议保持为`1.0.7`，最新版本2.0.0会引入冗余依赖

#### 优秀博客
* 1.[使用SqlSugar封装的数据层基类](https://www.cnblogs.com/bfyx/p/9125002.html)