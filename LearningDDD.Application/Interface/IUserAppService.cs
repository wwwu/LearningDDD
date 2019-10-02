using LearningDDD.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Application.Interface
{
    public interface IUserAppService: IDisposable
    {
        void Add(UserVM userVM);

        IEnumerable<UserVM> GetAll();

        UserVM GetById(Guid id);

        void Update(UserVM userVM);

        void Remove(Guid id);
    }
}
