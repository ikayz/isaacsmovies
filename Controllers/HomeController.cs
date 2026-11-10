using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class HomeController : Controller
{
    private readonly MvcMovieContext _context;

    public HomeController(MvcMovieContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movie
            .OrderByDescending(m => m.ReleaseDate)
            .ToListAsync();

        ViewData["TotalMovies"] = movies.Count;
        ViewData["Genres"] = movies
            .Where(m => !string.IsNullOrEmpty(m.Genre))
            .Select(m => m.Genre!)
            .Distinct()
            .OrderBy(g => g)
            .ToList();

        return View(movies.Take(6).ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
