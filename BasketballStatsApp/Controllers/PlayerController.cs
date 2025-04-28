using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasketballStatsApp.Data;
using BasketballStatsApp.Models;
using Microsoft.AspNetCore.Authorization; 

public class PlayerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public PlayerController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // GET: Player
    public async Task<IActionResult> Index()
    {

        var players = await _context.Players
            .AsNoTracking()  
            .Include(p => p.Team)
            .ToListAsync();

        return View(players);
    }

    // GET: Player/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var player = await _context.Players
            .Include(p => p.Team)
            .FirstOrDefaultAsync(p => p.PlayerId == id);

        if (player == null) return NotFound();

        return View(player);
    }

    // GET: Player/Create
    [Authorize] 
    public IActionResult Create()
    {
        ViewBag.TeamId = new SelectList(_context.Teams.OrderBy(t => t.Name), "TeamId", "Name");
        return View();
    }

    // POST: Player/Create
    [Authorize] 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("PlayerId,FullName,Age,Position,PointsPerGame,AssistsPerGame,ReboundsPerGame,TeamId")]
        Player player,
        IFormFile ImageFile)
    {
        if (!ModelState.IsValid)
        {
            foreach (var key in ModelState.Keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    Console.WriteLine($"Model error in '{key}': {error.ErrorMessage}");
                }
            }

            ViewBag.TeamId = new SelectList(_context.Teams.OrderBy(t => t.Name), "TeamId", "Name", player.TeamId);
            return View(player);
        }

        // Handle image upload
        if (ImageFile != null && ImageFile.Length > 0)
        {
            try
            {
                string uploadPath = Path.Combine(_environment.WebRootPath, "images");
                Directory.CreateDirectory(uploadPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                player.ImageFileName = fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Image upload failed: " + ex.Message);
                ModelState.AddModelError("ImageFile", "Image upload failed.");
                ViewBag.TeamId = new SelectList(_context.Teams.OrderBy(t => t.Name), "TeamId", "Name", player.TeamId);
                return View(player);
            }
        }

        _context.Add(player);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

        

    }


    // GET: Player/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var player = await _context.Players
            .AsNoTracking() // make sure fresh read
            .FirstOrDefaultAsync(p => p.PlayerId == id);

        if (player == null)
            return NotFound();

        ViewBag.TeamId = new SelectList(_context.Teams, "TeamId", "Name", player.TeamId);
        return View(player);
    }

    // POST: Player/Edit/5
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PlayerId,FullName,Age,Position,PointsPerGame,AssistsPerGame,ReboundsPerGame,TeamId")] Player formPlayer, IFormFile ImageFile)
    {
        if (id != formPlayer.PlayerId)
        {
            return NotFound();
        }

        var playerInDb = await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == id);
        if (playerInDb == null)
        {
            return NotFound();
        }

        //  Correctly copy new values over
        playerInDb.FullName = formPlayer.FullName;
        playerInDb.Position = formPlayer.Position;
        playerInDb.Age = formPlayer.Age;
        playerInDb.PointsPerGame = formPlayer.PointsPerGame;
        playerInDb.AssistsPerGame = formPlayer.AssistsPerGame;
        playerInDb.ReboundsPerGame = formPlayer.ReboundsPerGame;
        playerInDb.TeamId = formPlayer.TeamId;

        //  Handle image upload 
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var uploadPath = Path.Combine(_environment.WebRootPath, "images");
            Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            playerInDb.ImageFileName = fileName;
        }

        await _context.SaveChangesAsync(); //  Save it for real

        return RedirectToAction("Index", "Player"); //  Back to list
    }






    // GET: Player/Delete/5
    [Authorize] 
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var player = await _context.Players
            .Include(p => p.Team)
            .FirstOrDefaultAsync(p => p.PlayerId == id);

        if (player == null) return NotFound();

        return View(player);
    }

    // POST: Player/Delete/5
    [Authorize] 
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player != null)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // POST: Player/DeleteConfirmedAjax
    [Authorize] 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAjax(int id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
