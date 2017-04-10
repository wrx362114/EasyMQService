using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model.Entitys
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class FromT4Attribute : Attribute
    {
    }
    public class RemarkAttribute : Attribute
    {
        private string remark;
        public RemarkAttribute(string _remark)
        {
            remark = _remark;
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
