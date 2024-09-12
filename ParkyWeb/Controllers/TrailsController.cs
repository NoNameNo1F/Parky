using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModel;
using ParkyWeb.Repository.IRepository;


namespace ParkyWeb.Controllers;
[Authorize]
public class TrailsController : Controller
{
    private readonly INationalParkRepository _npRepo;
    private readonly ITrailRepository _trailRepo;

    public TrailsController(INationalParkRepository npRepo, ITrailRepository trailRepo)
    {
        _npRepo = npRepo;
        _trailRepo = trailRepo;
    }

    public IActionResult Index()
    {
        return View(new Trail() { });
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Upsert(int? id)
    {
        IEnumerable<NationalPark> npList = await _npRepo.GetAllAsync(SD.NationalParkAPIPath, HttpContext.Session.GetString("JWToken"));
        //#region explaining_1 
        //// TrailsVM need 2 object: NationalParkList and Trail
        //// If it only have List NationalPark, this return object reference not set to an instance of an object 
        //// public class TrailsVM
        //// {
        ////     public IEnumerable<SelectListItem> NationalParkList { get; set; }
        ////     public Trail Trail { get; set; }
        //// }
        //#endregion

        TrailsVM objVM = new TrailsVM()
        {
            NationalParkList = npList.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            Trail = new Trail()
        };

        if (id == null)
        {
            // this will be true for Create and insert
            return View(objVM);
        }
        objVM.Trail = await _trailRepo.GetAsync(SD.TrailAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
        if (objVM.Trail == null)
        {
            return NotFound();
        }
        return View(objVM);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(TrailsVM obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.Trail.Id == 0)
            {
                await _trailRepo.CreateAsync(SD.TrailAPIPath, obj.Trail, HttpContext.Session.GetString("JWToken"));
            }
            else
            {
                await _trailRepo.UpdateAsync(SD.TrailAPIPath + obj.Trail.Id, obj.Trail, HttpContext.Session.GetString("JWToken"));
            }
            return RedirectToAction(nameof(Index));
        }
        else
        {
            IEnumerable<NationalPark> npList = await _npRepo.GetAllAsync(SD.NationalParkAPIPath, HttpContext.Session.GetString("JWToken"));
            TrailsVM objVM = new TrailsVM()
            {
                NationalParkList = npList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Trail = obj.Trail
            };
            return View(objVM);
        }
    }
    public async Task<IActionResult> GetAllTrail()
    {
        return Json(new { data = await _trailRepo.GetAllAsync(SD.TrailAPIPath, HttpContext.Session.GetString("JWToken")) });
    }
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var status = await _trailRepo.DeleteAsync(SD.TrailAPIPath, id, HttpContext.Session.GetString("JWToken"));
        if (status)
        {
            return Json(new { success = true, message = "Delete Successful" });
        }
        return Json(new { success = false, message = "Delete Failed" });

    }
}