using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.Areas.ProductManager.Models;
using WebProject.Models;
using WebProject.Services.MappingCommodityServices;

namespace WebProject.Areas.ProductManager.Controllers
{


    [Area("ProductManager")]
    public class CommodityController : Controller
    {


        private readonly ICommodityServices _commodityServices;

        private readonly AppDbContext _context;
        public CommodityController(ICommodityServices commodityServices , AppDbContext context)
        {
          
            _commodityServices = commodityServices;

            _context = context;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _commodityServices.GetAll();

            return View(items);
        }

        [HttpGet]
        public IActionResult CreateCommodity()
        {
            return View("CreateCommodity", new CommodityData());
        }

        [HttpGet]
        public async Task<IActionResult> EditCommodity(Guid? id)
        {

            var item = await _commodityServices.GetCommodity(id);

            if (item != null)
            {
                var commodity = new CommodityData()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,

                };

                return View("EditCommodity", commodity);
            }


            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> EditCommodity([FromForm]CommodityData commodity)
        {
            if (commodity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = new Commodity()
                {

                    Id = commodity.Id,
                    Code = commodity.Code,
                    Name = commodity.Name
                };

                var result = await _commodityServices.Editor(data);

                if (result.Succeeded)
                {
                    StatusMessage = $"Chỉnh sửa thành công mặt hàng Mã :{data.Code} Tên: {data.Name} ";
                    
                    return RedirectToAction("Index");

                }
                result.Errors.ToList().ForEach(erorr => {
                    ModelState.AddModelError(string.Empty, erorr.Description);
                });
            }
          
                     
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommodity([FromForm]CommodityData commodity)
        {
            if (commodity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var item = new Commodity()
                {
                    Code = commodity.Code,
                    Name = commodity.Name
                };

                var result = await _commodityServices.Create(item);

                if (result.Succeeded)
                {
                    StatusMessage = $"Thêm thành công mặt hàng Tên: {item.Name} Mã: {item.Code}";
                    return RedirectToAction("index");
                }

                result.Errors.ToList().ForEach(erorr => {

                    ModelState.AddModelError(string.Empty, erorr.Description);
                });
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCommodity(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();

            }
            var data = await _commodityServices.GetCommodity(Id);

            if(data != null)
            {
                return View("DeleteCommodity", new CommodityData { 
                
                        Id = data.Id,
                        Code = data.Code,
                        Name = data.Name,
                } );
            }

            return NotFound();

         }

        [HttpPost]
        public async Task<IActionResult> SubmitCommodity(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();

            }
            var result = await _commodityServices.Delete(Id);

            if (result.Succeeded)
            {
                StatusMessage = $"Xóa thành công Id: {Id}";
                return RedirectToAction("index");
            }

            result.Errors.ToList().ForEach(error => {

                ModelState.AddModelError(string.Empty, error.Description);
            
            });
            

            return View("DeleteCommodity");
        }

        [HttpGet]
        public async Task<IActionResult> GetOptionCommoditys()
        {
            var items = await (from c in _context.commodities
                               select new CommodityData
                               {
                                   Id = c.Id,
                                   Code = c.Code,
                                   Name = c.Name

                               }).ToArrayAsync();
            

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCommoditys([FromForm] OptionCommoditys getOptionCommoditys)
        {

            if (getOptionCommoditys.commodityDatas == null)
            {
                return NotFound();
            }
            StatusMessage = string.Format($"Đã xóa thành công ");
            
            var count = 0;
            
            foreach (var item in getOptionCommoditys.commodityDatas)
            {
                if (item.Option)
                {
                    var result = await _commodityServices.Delete(item.Id);
                    if (result.Succeeded)
                    {
                        StatusMessage += string.Format($"- {item.Code} ");
                        count += 1;
                    }
                }
            }
            StatusMessage += string.Format($": tổng {count} chỉ mục");
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> FilterOptionCommoditys([FromForm] CommodityData[] commodityDatas)
        {
            var dataItems = new OptionCommoditys();

            dataItems.commodityDatas = (from c in commodityDatas
                                        where c.Option == true
                                        select c).ToArray();
            
            if(dataItems.commodityDatas.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Chưa chọn dữ liệu !!!");                
                var items = await(from c in _context.commodities
                                  select new CommodityData
                                  {
                                      Id = c.Id,
                                      Code = c.Code,
                                      Name = c.Name

                                  }).ToArrayAsync();

                return View("GetOptionCommoditys", items);
            }
            return View("DeleteCommoditys", dataItems);

        }

        [TempData]
        public string StatusMessage { get; set; }
    }
}
