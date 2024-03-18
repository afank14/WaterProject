using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WaterProject.Models;
using WaterProject.Models.ViewModels;

namespace WaterProject.Controllers;

public class HomeController : Controller
{
    private IWaterRepository _repo;

    public HomeController(IWaterRepository temp)
    {
        _repo = temp;
    }

    public IActionResult Index(int pageNum)
    {
        int pageSize = 2;

        var blah = new ProjectsListViewModel
        {
            Projects = _repo.Projects
                .OrderBy(x => x.ProjectName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),
            
            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum,
                ItemsPerPage = pageSize,
                TotalItems = _repo.Projects.Count()
            }
        };
        
        return View(blah);
    }
    
}