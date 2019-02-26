# 基于DDD模型的ASP.NET MVC5快速开发框架

#### 项目结构：
* `Quick.Models`：数据层，包括领域驱动模型中的Application、Domain、Driver
* `Quick.Common`：公共层，包括AutoMapper、Hangfire的配置等
* `Quick.IRepository`：数据仓储层接口
* `Quick.IRepository`：数据仓储层
* `Quick.IService`：业务逻辑层接口
* `Quick.Service`：业务逻辑层
* `QuickWeb`：UI表现层

#### 关键技术
* 1.Asp.Net Mvc 5.2.7
* 2.MySqlSugar
* 3.AutoFac
* 4.AutoMapper
* 5.log4net
* 6.System.ValueTuple
* 7.StackExchange.Redis
* 8.Hangfire
* 9.FluentScheduler