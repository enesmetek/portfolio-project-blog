using Azure;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BlogDbContext _blogDbContext;
        public AdminTagsController(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //Mapping AddRequest to Tag Domain Model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            _blogDbContext.Tags.Add(tag);
            _blogDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var tags = _blogDbContext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = _blogDbContext.Tags.Find(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                ID = editTagRequest.ID,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag = _blogDbContext.Tags.Find(tag.ID);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                _blogDbContext.SaveChanges();

                //Show Success Notification
                return RedirectToAction("Edit", new { id = editTagRequest.ID });
            }
            else
            {
                //Show Error Notification
                return RedirectToAction("Edit", new { id = editTagRequest.ID});
            }
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var existingTag = _blogDbContext.Tags.Find(editTagRequest.ID);
            if (existingTag != null)
            {
                _blogDbContext.Tags.Remove(existingTag);
                _blogDbContext.SaveChanges();
                //Show Succes Notification
                return RedirectToAction("List");
            }
            else
            {
                //Show Error Notification
                return RedirectToAction("Edit", new { id = editTagRequest.ID });
            }


            return View();
        }
    }
}
