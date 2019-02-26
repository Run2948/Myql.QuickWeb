
/* ==============================================================================
* 命名空间：Quick.Common.Systems 
* 类 名 称：Disposable
* 创 建 者：Qing
* 创建时间：2019-02-25 19:31:03
* CLR 版本：4.0.30319.42000
* 保存的文件名：Disposable
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common.Systems
{
    /// <summary>
    /// Disposable
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// 终结器
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            Dispose(true);
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        public abstract void Dispose(bool disposing);
    }
}
