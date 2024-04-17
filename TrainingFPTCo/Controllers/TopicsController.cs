using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;
using Microsoft.AspNetCore.Http; // Import IFormFile
using System.IO;
using TrainingFPTCo.Helpers; // Import Path

namespace TrainingFPTCo.Controllers
{
    public class TopicsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string searchString, string status)
        {
            TopicViewModel topicViewModel = new TopicViewModel();
            topicViewModel.TopicDetailsList = new List<TopicDetail>();
            var dataTopics = new TopicQuery().GetAllTopics(searchString,status);
            foreach (var data in dataTopics)
            {
                topicViewModel.TopicDetailsList.Add(new TopicDetail
                {
                    Id = data.Id,
                    Name = data.Name,
                    ViewCourseName = data.CourseName,
                    CourseId = data.CourseId,
                    Description = data.Description,
                    Status = data.Status,
                    Documents = data.Documents,
                    ViewDocuments = data.ViewDocuments,
                    ViewAttachFiles = data.ViewAttachFiles,
                    ViewPosterTopic = data.ViewPosterTopic,
                    CreatedAt = data.CreatedAt,
                    TypeDocument = data.TypeDocument
                });
            }
            return View(topicViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            TopicDetail topicDetail = new TopicDetail();

            List<SelectListItem> itemCourses = new List<SelectListItem>();
            var dataCourses = new CourseQuery().GetAllDataCourses(null, null);
            foreach (var item in dataCourses)
            {
                itemCourses.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Courses = itemCourses;
            return View(topicDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TopicDetail topicDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lưu tệp đính kèm vào thư mục images
                    string attachDocument = string.Empty;
                    string InsertposterTopic = string.Empty;
                    string nameAttachFiles = string.Empty;
                    if (topicDetail.Documents != null)
                    {
                        attachDocument = FileUploader.UploadFile(topicDetail.Documents);
                    }
                    if (topicDetail.PosterTopic != null)
                    {
                        InsertposterTopic = FileUploader.UploadFile(topicDetail.PosterTopic);
                    }
                    if(topicDetail.AttachFiles != null)
                    {
                        nameAttachFiles = UploadFileHelper.UpLoadFile(topicDetail.AttachFiles);
                    }
                    // Process inserting topic into the database
                    int topicId = new TopicQuery().InsertTopic(
                        topicDetail.Name,
                        topicDetail.CourseId,
                        topicDetail.Description,
                        topicDetail.Status,
                        attachDocument,
                        InsertposterTopic,
                        nameAttachFiles,
                        topicDetail.TypeDocument); // Thêm TypeDocument vào phương thức InsertTopic
                    if (topicId > 0)
                    {
                        TempData["saveStatus"] = true;
                    }
                    else
                    {
                        TempData["saveStatus"] = false;
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            else
            {
                TempData["saveStatus"] = false;
            }

            List<SelectListItem> itemCourses = new List<SelectListItem>();
            var dataCourses = new CourseQuery().GetAllDataCourses(null, null);
            foreach (var item in dataCourses)
            {
                itemCourses.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Courses = itemCourses;
            return View(topicDetail);
        }


        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            bool deleteTopic = new TopicQuery().DeleteTopicById(id);
            if (deleteTopic)
            {
                TempData["statusDelete"] = true;
            }
            else
            {
                TempData["statusDelete"] = false;
            }
            return RedirectToAction(nameof(TopicsController.Index), "Topics");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            TopicDetail topicDetail = new TopicQuery().GetTopicById(id);

            if (topicDetail == null)
            {
                return NotFound();
            }

            List<SelectListItem> itemCourses = new List<SelectListItem>();
            var dataCourses = new CourseQuery().GetAllDataCourses(null,null);
            foreach (var item in dataCourses)
            {
                itemCourses.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                    Selected = (item.Id == topicDetail.CourseId)
                });
            }
            ViewBag.Courses = itemCourses;
            return View(topicDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TopicDetail topicDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var detail = new TopicQuery().GetTopicById(topicDetail.Id);
                    if(detail == null)
                    {
                        TempData["updateStatus"] = false;
                        return RedirectToAction(nameof(Index));
                    }
                    string attachDocument = detail.ViewDocuments;
                    string InsertposterTopic = detail.ViewPosterTopic;
                    string nameAttachFiles = detail.ViewAttachFiles;
                    if (topicDetail.Documents != null)
                    {
                        attachDocument = FileUploader.UploadFile(topicDetail.Documents);
                    }
                    if (topicDetail.PosterTopic != null)
                    {
                        InsertposterTopic = FileUploader.UploadFile(topicDetail.PosterTopic);
                    }
                    if (topicDetail.AttachFiles != null)
                    {
                        nameAttachFiles = UploadFileHelper.UpLoadFile(topicDetail.AttachFiles);
                    }
                    bool updateStatus = new TopicQuery().UpdateTopic(
                        topicDetail.Id,
                        topicDetail.Name,
                        topicDetail.CourseId,
                        topicDetail.Description,
                        topicDetail.Status,
                        attachDocument,
                        InsertposterTopic,
                        nameAttachFiles,
                        topicDetail.TypeDocument);

                    if (updateStatus)
                    {
                        TempData["updateStatus"] = true;
                    }
                    else
                    {
                        TempData["updateStatus"] = false;
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }

            List<SelectListItem> itemCourses = new List<SelectListItem>();
            var dataCourses = new CourseQuery().GetAllDataCourses(null, null);
            foreach (var item in dataCourses)
            {
                itemCourses.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.Courses = itemCourses;
            return View(topicDetail);
        }
    }

}
