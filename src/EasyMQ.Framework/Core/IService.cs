using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Framework.Core
{
    public interface IService
    {
        bool Start();
        bool Stop();

    }
}
