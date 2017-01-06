using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Framework.Core
{
    /// <summary> 操作结果实体 </summary>
    public class CommandResult
    {
        /// <summary> 是否成功 </summary>
        public bool IsCompleted { get; set; }

        /// <summary> 提示消息 </summary>
        public string Message { get; set; }

        private Exception Exception { get; set; }

        /// <summary> 成功 </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static CommandResult Completed(string msg)
        {
            return new CommandResult
            {
                IsCompleted = true,
                Message = msg
            };
        }

        /// <summary> 成功 </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static CommandResult Completed()
        {
            return new CommandResult
            {
                IsCompleted = true,
                Message = "操作成功"
            };
        }
        /// <summary> 失败 </summary>
        public static CommandResult Failed(string msg)
        {
            return new CommandResult
            {
                IsCompleted = false,
                Message = msg
            };
        }

        public static CommandResult CatchException(string msg, Exception ex)
        {
            return new CommandResult
            {
                IsCompleted = false,
                Message = msg,
                Exception = ex
            };
        }
    }

    public class QueryResult<T> : CommandResult
    {
        public QueryResult(T result)
        {
            Body = result;
        }

        public T Body { get; private set; }
    }
}
