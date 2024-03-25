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

    public IActionResult Index(int pageNum, string projectType)
    {
        int pageSize = 2;

        var blah = new ProjectsListViewModel
        {
            Projects = _repo.Projects
                .Where(x => x.ProjectType == projectType || projectType == null)
                .OrderBy(x => x.ProjectName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),
            
            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum,
                ItemsPerPage = pageSize,
                TotalItems = projectType == null ?  _repo.Projects.Count() : _repo.Projects.Where(x => x.ProjectType == projectType).Count()
            },
            
            CurrentProjectType = projectType
        };
        
        return View(blah);
    }
    
}