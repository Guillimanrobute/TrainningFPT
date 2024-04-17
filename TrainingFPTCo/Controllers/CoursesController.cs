using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingFPTCo.Helpers;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;

namespace TrainingFPTCo.Controllers
{
    public class CoursesController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? searchString, string? status)
        {
            CourseViewModel course = new CourseViewModel();
            course.CourseDetailsList = new List<CourseDetail>();
            var dataCourses = new CourseQuery().GetAllDataCourses(searchString, status);
            foreach (var data in dataCourses)
            {
                course.CourseDetailsList.Add(new CourseDetail
                {
                    Id = data.Id,
                    Name = data.Name,
                    CategoryId = data.CategoryId,
                    Description = data.Description,
                    Status = data.Status,
                    ViewStartDate = data.ViewStartDate,
                    ViewEndDate = data.ViewEndDate,
                    StartDate = data.StartDate,
                    EndDate = data.EndDate,
                    ViewImageCouser = data.ViewImageCouser,
                    ViewCategoryName = data.ViewCategoryName
                });
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult Add()
        {
            CourseDetail course = new CourseDetail();

            List<SelectListItem> itemCategories = new List<SelectListItem>();
            var dataCategory = new CategoryQuery().GetAllCategories(null, null);
            foreach (var item in dataCategory)
            {
                itemCategories.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Categories = itemCategories;
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CourseDetail course, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                // xu ly insert course vao database
                try
                {
                    string nameImageCourse = UploadFileHelper.UpLoadFile(Image);
                    int idCourse = new CourseQuery().InsertCourse(
                        course.Name,
                        course.CategoryId,
                        course.Description,
                        course.StartDate,
                        course.EndDate,
                        course.Status,
                        nameImageCourse
                    );
                    if (idCourse > 0)
                    {
                        TempData["saveStatus"] = true;
                    }
                    else
                    {
                        TempData["saveStatus"] = false;
                    }
                    // quay lai trang danh sach courses
                    return RedirectToAction(nameof(CoursesController.Index), "Courses");
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }

            List<SelectListItem> itemCategories = new List<SelectListItem>();
            var dataCategory = new CategoryQuery().GetAllCategories(null, null);
            foreach (var item in dataCategory)
            {
                itemCategories.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Categories = itemCategories;
            return View(course);
        }

        [HttpPost]
        public JsonResult Delete(int id = 0)
        {
            bool deleteCourse = new CourseQuery().DeleteCourseById(id);
            if (deleteCourse)
            {
                return Json(new {cod = 200, message = "Successfully"});
            }
            return Json(new { cod = 500, message = "Failure" });
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            CourseDetail detail = new CourseQuery().GetDetailCourseById(id);
            List<SelectListItem> itemCategories = new List<SelectListItem>();
            var dataCategory = new CategoryQuery().GetAllCategories(null, null);
            foreach (var item in dataCategory)
            {
                itemCategories.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Categories = itemCategories;
            return View(detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CourseDetail course, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra nếu có hình ảnh mới được tải lên
                    if (Image != null)
                    {
                        string nameImageCourse = UploadFileHelper.UpLoadFile(Image);
                        course.ViewImageCouser = nameImageCourse;
                    }

                    // Cập nhật thông tin khóa học vào cơ sở dữ liệu
                    bool updateResult = new CourseQuery().UpdateCourse(
                        course.Id,
                        course.Name,
                        course.CategoryId,
                        course.Description,
                        course.StartDate,
                        course.EndDate,
                        course.Status,
                        course.ViewImageCouser
                    );

                    if (updateResult)
                    {
                        TempData["updateStatus"] = true;
                    }
                    else
                    {
                        TempData["updateStatus"] = false;
                    }

                    // Quay lại trang danh sách khóa học
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Xử lý nếu có lỗi xảy ra
                    TempData["updateStatus"] = false; // Gán trạng thái cập nhật là false
                    ModelState.AddModelError("", "Error while updating course: " + ex.Message);
                    // Trả về view với model và các thông tin lỗi nếu có
                    List<SelectListItem> itemCategoriesforUpdate = new List<SelectListItem>();
                    var categoryDataforUpdate = new CategoryQuery().GetAllCategories(null, null);
                    foreach (var item in categoryDataforUpdate)
                    {
                        itemCategoriesforUpdate.Add(new SelectListItem
                        {
                            Value = item.Id.ToString(),
                            Text = item.Name
                        });
                    }
                    ViewBag.Categories = itemCategoriesforUpdate;
                    return View(course);
                }
            }

            // Nếu dữ liệu không hợp lệ, hiển thị lại view với thông tin lỗi
            List<SelectListItem> itemCategories = new List<SelectListItem>();
            var categoryData = new CategoryQuery().GetAllCategories(null, null);
            foreach (var item in categoryData)
            {
                itemCategories.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Categories = itemCategories;
            return View(course);
        }


    }
}
