using LearningDDD.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningDDD.Application.Interface
{
    public interface IUserAppService: IDisposable
    {
        Task AddAsync(UserVM userVM);

        IEnumerable<UserVM> GetAll();

        Task<UserVM> GetByIdAsync(Guid id);

        Task Update(UserVM userVM);

        Task RemoveAsync(Guid id);
    }
}
