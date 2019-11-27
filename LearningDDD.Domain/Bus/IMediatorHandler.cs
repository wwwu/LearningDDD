using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Commands;
using LearningDDD.Domain.Events;
using MediatR;

namespace LearningDDD.Domain.Bus
{
    /// <summary>
    /// 中介处理程序接口
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// 发布命令，将我们的命令模型发布到中介者模块
        /// </summary>
        /// <typeparam name="T">基础BaseCommand的命令模型类型</typeparam>
        /// <param name="command">命令模型</param>
        /// <returns></returns>
        Task<Unit> SendCommand<T>(T command) where T : Command;

        /// <summary>
        /// 引发事件，通过总线，发布事件
        /// </summary>
        /// <typeparam name="T">Event：INotification</typeparam>
        /// <param name="event">Event事件模型</param>
        /// <returns></returns>
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
