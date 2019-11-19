using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobileList.Dtos.Manufacturer;
using MobileList.Dtos.Mobile;
using MobileList.Inf.Interfaces.Mobile;
using MobileList.Models.Paging;

namespace MobileList.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMobile _mobile;

        public HomeController(IMobile mobile)
        {
            _mobile = mobile;
        }

        [Route("")]
        public async Task<IActionResult> Index(int page = 1, string name = "", decimal minPrice = 0, decimal maxPrice = 10000, int manufacturer = 0)
        {
            var mobilesFiltered = await _mobile.GetFilteredMobiles(name, manufacturer, minPrice, maxPrice, page, 6);
            var manufacturers = await _mobile.GetManufacturers();
            var pm = new PagingModel { Data = mobilesFiltered.Mobiles, Count = mobilesFiltered.Count, SearchString = name, Manufacturer = manufacturer, Manufacturers = manufacturers, MinPrice = minPrice, MaxPrice = maxPrice, CurrentPage = page };
            return View(pm);
        }

        [Route("{id}")]
        public async Task<IActionResult> Single(int id)
        {
            var mobile = await _mobile.GetMobile(id);
            return View(mobile);
        }

        [Route("add")]
        public async Task<IActionResult> AddMobile()
        {
            var manufacturers = (await _mobile.GetManufacturers()).Select(m => new GetManufacturerDto
            {
                Id = m.Id,
                Name = m.Name
            }).ToList();

            return View(manufacturers);
        }

        public async Task<IActionResult> AddRedirect([FromForm]AddMobileDto model)
        {
            var addResult = await _mobile.AddMobile(model);
            return RedirectToAction(addResult.Equals("success") ? "Index" : "Error");
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
