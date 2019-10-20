using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningDDD.Web.Models;
using LearningDDD.Application.Interface;
using LearningDDD.Application.ViewModels;

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
            if (ModelState.IsValid)
            {
                await _userAppService.AddAsync(userVM);
            }
            else
            {
                return Error();
            }
            return NoContent();
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
