using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YW.Model.Enums;

namespace YW.Model
{
    /// <summary>
    /// 统一返回值实体
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ResultModel()
        {
            Succeed = true;
            Msg = "";
            ResultCode = ResultCodeEnum.Succeed;
        }
        /// <summary> 操作/查询是否成功 </summary>
        public bool Succeed { get; set; }
        /// <summary> 提示消息 </summary>
        public string Msg { get; set; }
        /// <summary> 结果编码 </summary>
        public ResultCodeEnum ResultCode { get; set; }
        /// <summary> 返回一个操作成功结果 </summary>
        public static ResultModel Ok()
        {
            return new ResultModel
            {
                Succeed = true,
                ResultCode = ResultCodeEnum.Succeed
            };
        }
        /// <summary> 返回一个操作失败结果 </summary>
        public static ResultModel Failed(string msg)
        {
            return new ResultModel
            {
                Succeed = false,
                Msg = msg,
                ResultCode = ResultCodeEnum.ParamsError
            };
        }
        /// <summary> 返回一个操作失败结果 </summary>
        public static ResultModel Failed(string msg, ResultCodeEnum code)
        {
            return new ResultModel
            {
                Succeed = false,
                Msg = msg,
                ResultCode = code
            };
        }
    }

    /// <summary> 查询结果返回值实体 </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryResultModel<T> : ResultModel
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public QueryResultModel() : base()
        {

        }
        /// <summary> 查询结果实体 </summary>
        public T Body { get; set; }

        /// <summary> 返回一个操作失败结果 </summary>
        public static new QueryResultModel<T> Failed(string msg)
        {
            return new QueryResultModel<T>
            {
                Succeed = false,
                Msg = msg,
                ResultCode = ResultCodeEnum.ParamsError
            };
        }
        /// <summary> 返回一个操作失败结果 </summary>
        public static new QueryResultModel<T> Failed(string msg, ResultCodeEnum code)
        {
            return new QueryResultModel<T>
            {
                Succeed = false,
                Msg = msg,
                ResultCode = code
            };
        }
    }

    /// <summary> 查询结果返回值扩展 </summary>
    public static class ObjectExtend
    {
        /// <summary> 将当前对象转为查询成功结果 </summary>
        public static QueryResultModel<T> AsQueryResult<T>(this T obj)
        {
            return new QueryResultModel<T>
            {
                Succeed = true,
                Msg = "",
                ResultCode = ResultCodeEnum.Succeed,
                Body = obj
            };
        }
    }
    /// <summary>
    /// 分页结果实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResultModel<T> : QueryResultModel<IEnumerable<T>>
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PagedResultModel() : base()
        {

        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                return RowCount / PageSize + RowCount % PageSize > 0 ? 1 : 0;
            }
        }
        /// <summary>
        /// 总行数
        /// </summary>
        public int RowCount { get; set; }
    }
}
