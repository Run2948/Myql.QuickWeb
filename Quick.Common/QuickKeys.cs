﻿
/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：QuickKeys
* 创 建 者：Qing
* 创建时间：2018-12-18 13:46:26
* CLR 版本：4.0.30319.42000
* 保存的文件名：QuickKeys
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common
{
    public class QuickKeys
    {
        /// <summary>
        /// 用户Session键
        /// </summary>
        public const string UserSession = "userinfo";
        /// <summary>
        /// 用户登录地址
        /// </summary>
        public const string UserLogin = "/UserLogin";
        /// <summary>
        /// 管理员中心
        /// </summary>
        public const string AdminHome = "/Admin/Home";
        /// <summary>
        /// 战训专家中心
        /// </summary>
        public const string ExpertHome = "/Expert";
        /// <summary>
        /// 支队指挥员
        /// </summary>
        public const string DetachHome = "/Detach";
        /// <summary>
        /// 大队指挥员
        /// </summary>
        public const string BrigadeHome = "/Brigade";
        /// <summary>
        /// 中队指挥员
        /// </summary>
        public const string SquadronHome = "/Squadron";
        /// <summary>
        /// 战斗员
        /// </summary>
        public const string FighterHome = "/Fighter";
        
    }
}
