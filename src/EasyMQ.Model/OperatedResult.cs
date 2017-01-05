using System; 
namespace EasyMQ.Model
{
    /// <summary> 操作结果实体 </summary>
    public class OperatedResult
    {
        /// <summary> 是否成功 </summary>
        public bool IsCompleted { get; set; }

        /// <summary> 提示消息 </summary>
        public string Message { get; set; }

        private Exception Exception { get; set; }

        /// <summary> 成功 </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperatedResult Completed(string msg)
        {
            return new OperatedResult
            {
                IsCompleted = true,
                Message = msg
            };
        }

        /// <summary> 成功 </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperatedResult Completed()
        {
            return new OperatedResult
            {
                IsCompleted = true,
                Message = "操作成功"
            };
        }
        /// <summary> 失败 </summary>
        public static OperatedResult Failed(string msg)
        {
            return new OperatedResult
            {
                IsCompleted = false,
                Message = msg
            };
        }

        public static OperatedResult CatchException(string msg, Exception ex)
        {
            return new OperatedResult
            {
                IsCompleted = false,
                Message = msg,
                Exception = ex
            };
        }
    }

    public class ValueResult<T> : OperatedResult
    {
        public ValueResult(T result)
        {
            Result = result;
        }

        public T Result { get; private set; }
    }
}
