using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDDD.Web.Models
{
    public class BaseResult<T> : BaseResult
    {
        public BaseResult(bool isSuccess = true) : base(isSuccess)
        {
        }

        public BaseResult(bool isSuccess, string message) : base(isSuccess, message)
        {
        }

        public T Data { get; set; }
    }

    public class BaseResult
    {
        public BaseResult(bool isSuccess = true)
        {
            IsSuccess = isSuccess;
        }

        public BaseResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        /// <summary>
        /// 业务是否执行成功
        /// </summary>
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
