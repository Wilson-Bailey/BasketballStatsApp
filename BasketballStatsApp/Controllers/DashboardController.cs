using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasketballStatsApp.Data;
using BasketballStatsApp.Models;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalPlayers = await _context.Players.CountAsync();
        var totalTeams = await _context.Teams.CountAsync();

        var avgPPG = await _context.Players.AverageAsync(p => p.PointsPerGame);
        var avgAPG = await _context.Players.AverageAsync(p => p.AssistsPerGame);
        var avgRPG = await _context.Players.AverageAsync(p => p.ReboundsPerGame);

        var viewModel = new DashboardViewModel
        {
            TotalPlayers = totalPlayers,
            TotalTeams = totalTeams,
            AveragePPG = Math.Round(avgPPG, 1),
            AverageAPG = Math.Round(avgAPG, 1),
            AverageRPG = Math.Round(avgRPG, 1)
        };

        return View(viewModel);
    }
}
