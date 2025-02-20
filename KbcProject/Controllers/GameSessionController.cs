using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using KbcProject.Entities;
using KbcProject.Models;

namespace KbcProject.Controllers
{
    [Authorize] // Ensure only logged-in users access
    public class GameSessionController : Controller
    {
        private readonly AppDbContext _context;

        public GameSessionController(AppDbContext context)
        {
            _context = context;
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var gameSessions = await _context.GameSessions
                .Include(gs => gs.User) 
                .OrderByDescending(gs => gs.StartedAt)
                .ToListAsync();
            return View(gameSessions);
        }

        // Retrieve the logged-in user's game sessions
        public async Task<IActionResult> MySessions()
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value); // Fetch logged-in user ID
            var userSessions = await _context.GameSessions
                .Where(gs => gs.UserId == userId)
                .OrderByDescending(gs => gs.StartedAt)
                .ToListAsync();
            return View(userSessions);
        }

        // Save game session after the game ends
        [HttpPost]
        public async Task<IActionResult> SaveGameSession(int score, DateTime startTime)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var endTime = DateTime.Now;
            var timeTaken = endTime - startTime;

            var gameSession = new GameSession
            {
                UserId = userId,
                Score = score,
                StartedAt = startTime,
                EndedAt = endTime,
                TimeTaken = timeTaken
            };

            _context.GameSessions.Add(gameSession);
            await _context.SaveChangesAsync();

            return RedirectToAction("GameOver", "Game");
        }
    }
}

