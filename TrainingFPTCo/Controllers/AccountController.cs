using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;

namespace TrainingFPTCo.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountQuery _accountQuery;

        public AccountController(AccountQuery accountQuery)
        {
            _accountQuery = accountQuery;
        }

        // Action để hiển thị danh sách người dùng
        public IActionResult Index()
        {
            List<AccountViewModel> users = _accountQuery.GetAllUsers();
            return View(users);
        }

        // Action để hiển thị form thêm người dùng
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Action để xử lý việc thêm người dùng
        [HttpPost]
        public IActionResult Create(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accountQuery.CreateUser(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error while adding user: " + ex.Message);
                }
            }
            return View(model);
        }

        // Action để hiển thị form sửa thông tin người dùng
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AccountViewModel user = _accountQuery.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Action để xử lý việc sửa thông tin người dùng
        [HttpPost]
        public IActionResult Edit(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accountQuery.UpdateUser(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error while editing user: " + ex.Message);
                }
            }
            return View(model);
        }

        // Action để xử lý việc xóa người dùng
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _accountQuery.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { error = ex.Message });
            }
        }
    }
}
