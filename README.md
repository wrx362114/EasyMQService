# EasyMQService
rabbitmq的强类型快速开发框架,插件式开发.
服务插件实现iservice接口,将子服务与对应插件的所有dll都一起丢到执行目录下的plugins文件夹中(暂不支持子文件夹),有时间可以改成一个子服务一个文件夹的

# 创建一个定时任务
    new TimedTaskMsg{TaskId="CreateOrder_1",StartTime=DateTime.Now.AddDay(1)}
      .SetMsg(new CreateOrderMsg{})
      .Publish()


我去..这个玩意儿怎么都有9个star 了..你们激励到我了!
我要完善这个玩意儿.
