using Microsoft.AspNetCore.Mvc;
using MVCTutorialWeb.Data;
using MVCTutorialWeb.Models;

namespace MVCTutorialWeb.Controllers
{
  public class CategoryController : Controller
  {
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
      _db = db;
    }
    public IActionResult Index()
    {
      IEnumerable<Category> objCategorylist = _db.Categories.ToList();
      return View(objCategorylist);
    }
    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
      if(obj.Name == obj.DisplayOrder.ToString())
      {
        ModelState.AddModelError("name", "The DisplayOrder cannot exaclty match the Name.");
      }
      if (ModelState.IsValid)
      {
        _db.Categories.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(obj);
    }

    public IActionResult Edit(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }
      var categoryFromDb = _db.Categories.Find(id);
      //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
      //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

      if(categoryFromDb==null)
      {
        return NotFound();
      }

      return View(categoryFromDb);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
      if (obj.Name == obj.DisplayOrder.ToString())
      {
        ModelState.AddModelError("name", "The DisplayOrder cannot exaclty match the Name.");
      }
      if (ModelState.IsValid)
      {
        _db.Categories.Update(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(obj);
    }

    public IActionResult Remove(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }
      var categoryFromDb = _db.Categories.Find(id);
      //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
      //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

      if (categoryFromDb == null)
      {
        return NotFound();
      }

      return View(categoryFromDb);
    }
    [HttpPost, ActionName("Remove")]
    [ValidateAntiForgeryToken]
    public IActionResult RemovePOST(int? id)
    {
      var obj = _db.Categories.Find(id);
      if(obj == null)
      {
        return NotFound();
      }
      _db.Categories.Remove(obj);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
