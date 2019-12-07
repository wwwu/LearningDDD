using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningDDD.Web.Models;
using LearningDDD.Application.Interface;
using LearningDDD.Application.Dto.User;
using MediatR;
using LearningDDD.Domain.Notifications;

namespace LearningDDD.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly DomainNotificationHandler _notificationHandler;

        public HomeController(IUserAppService userAppService,
            INotificationHandler<DomainNotification> notificationHandler)
        {
            _userAppService = userAppService;
            _notificationHandler = notificationHandler as DomainNotificationHandler;
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
        public async Task<IActionResult> Insert(CreateUserDto createUserDto)
        {
            var result = new BaseResult<object>();

            await _userAppService.AddAsync(createUserDto);
            if (_notificationHandler.HasNotifications())
            {
                result.IsSuccess = false;
                result.Data = _notificationHandler.GetNotifications();
            }

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
