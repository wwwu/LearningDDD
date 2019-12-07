using LearningDDD.Application.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningDDD.Application.Interface
{
    public interface IUserAppService: IDisposable
    {
        Task AddAsync(CreateUserDto dto);

        IEnumerable<UserDto> GetAll();

        Task<UserDto> GetByIdAsync(Guid id);

        Task Update(UpdateUserDto dto);

        Task RemoveAsync(Guid id);
    }
}
