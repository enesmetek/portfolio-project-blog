using Azure;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping AddRequest to Tag Domain Model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await _tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = new Tag
            {
                ID = id,
                N
            } 

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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                ID = editTagRequest.ID,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag = await _blogDbContext.Tags.FindAsync(tag.ID);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
               await _blogDbContext.SaveChangesAsync();

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
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var existingTag = await _blogDbContext.Tags.FindAsync(editTagRequest.ID);
            if (existingTag != null)
            {
                _blogDbContext.Tags.Remove(existingTag);
                await _blogDbContext.SaveChangesAsync();
                //Show Succes Notification
                return RedirectToAction("List");
            }
            else
            {
                //Show Error Notification
                return RedirectToAction("Edit", new { id = editTagRequest.ID });
            }
        }
    }
}
