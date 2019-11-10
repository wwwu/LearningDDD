using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Core.Commands;
using MediatR;

namespace LearningDDD.Domain.Core.Bus
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
        Task<Unit> SendCommand<T>(T command) where T : BaseCommand;
    }
}
