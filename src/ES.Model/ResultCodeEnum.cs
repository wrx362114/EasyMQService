using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model
{
    /// <summary>
    /// 同意操作/查询结果编码
    /// </summary>
    public enum ResultCodeEnum
    {
        /// <summary> 成功 </summary>
        Succeed,
        /// <summary> 未知错误 </summary>
        UndefinedError,
        /// <summary> 参数错误 </summary>
        ParamsError,
        /// <summary> 操作频率超出限制 </summary>
        FreqOutOfLimit,
        /// <summary> 没有权限 </summary>
        Unauthorized,
        /// <summary> 外部服务不可用 </summary>
        ExternalServiceUnavailable,
        /// <summary> 服务器未捕获异常 </summary>
        UnCatchError,
        /// <summary> APP版本已禁用 </summary>
        VersionDisable
    }
}
