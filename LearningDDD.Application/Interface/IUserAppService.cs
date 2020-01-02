using LearningDDD.Application.Dto.User;
using LearningDDD.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningDDD.Application.Interface
{
    public interface IUserAppService: IDisposable
    {
        Task<BaseResult> AddAsync(CreateUserDto dto);

        IEnumerable<UserDto> GetAll();

        Task<UserDto> GetByIdAsync(Guid id);

        Task<BaseResult> Update(UpdateUserDto dto);

        Task<bool> RemoveAsync(Guid id);
    }
}
