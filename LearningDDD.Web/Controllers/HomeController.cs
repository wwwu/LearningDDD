using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningDDD.Web.Models;
using LearningDDD.Application.Interface;
using LearningDDD.Application.Dto.User;

namespace LearningDDD.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAppService _userAppService;

        public HomeController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            var model = _userAppService.GetAll();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CreateUserDto createUserDto)
        {
            var result = await _userAppService.AddAsync(createUserDto);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            var result = await _userAppService.Update(updateUserDto);
            return new JsonResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userAppService.RemoveAsync(id);
            return Ok();
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

    }
}
