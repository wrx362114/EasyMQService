using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Service.RabbitMQ.Subscribe
{
    public interface IEventMsg
    {
        string Event { get; }
        int RetryCount { get; set; }
        Task PublishAsync();
    }
}
