using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningDDD.Web.Models;
using LearningDDD.Application.Interface;
using LearningDDD.Application.ViewModels.User;
using LearningDDD.Domain.Commands.User;
using AutoMapper;

namespace LearningDDD.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly IMapper _mapper;

        public HomeController(IUserAppService userAppService
            , IMapper mapper)
        {
            _userAppService = userAppService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = _userAppService.GetAll();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region User

        [HttpPost]
        public async Task<IActionResult> Insert(UserVM userVM)
        {
            var result = new BaseResult<List<string>>();
            ////视图模型验证
            //if (!ModelState.IsValid)
            //{
            //    return Error();
            //}

            ////命令模型验证
            //var createUserCommand = new CreateUserCommand(_mapper.Map<Domain.Models.User>(userVM));
            //if (!createUserCommand.IsValid())
            //{
            //    result.IsSuccess = false;
            //    result.Data = createUserCommand.ValidationResult.Errors
            //        .Select(s => s.ErrorMessage)
            //        .ToList();
            //    return new JsonResult(result);
            //}

            await _userAppService.AddAsync(userVM);

            return new JsonResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userAppService.RemoveAsync(id);
            return Ok();
        }

        #endregion
    }
}
