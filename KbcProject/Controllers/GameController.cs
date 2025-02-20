using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KbcProject.Entities;
using KbcProject.Helpers;
using KbcProject.Models; // Import SessionExtensions

namespace KbcProject.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Random _random = new Random();

        public GameController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> StartGame()
        {
            var questions = await _context.Questions
                .OrderBy(q => Guid.NewGuid()) // Fetch random questions
                .Take(5) // Get 5 random questions
                .ToListAsync();

            HttpContext.Session.SetObject("Questions", questions);
            HttpContext.Session.SetInt32("Score", 0);
            HttpContext.Session.SetInt32("CurrentQuestionIndex", 0);
            HttpContext.Session.SetString("StartTime", DateTime.Now.ToString());

            return View("Game", questions.First());
        }

        [HttpPost]
        public IActionResult SubmitAnswer(string userAnswer)
        {
            var questions = HttpContext.Session.GetObject<List<Question>>("Questions");
            int index = HttpContext.Session.GetInt32("CurrentQuestionIndex") ?? 0;
            int score = HttpContext.Session.GetInt32("Score") ?? 0;

            if (questions[index].CorrectAnswer.Equals(userAnswer, StringComparison.OrdinalIgnoreCase))
            {
                score++;
                HttpContext.Session.SetInt32("Score", score);

                if (index + 1 < questions.Count)
                {
                    HttpContext.Session.SetInt32("CurrentQuestionIndex", index + 1);
                    return View("Game", questions[index + 1]);
                }
                else
                {
                    return RedirectToAction("GameOver");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }

        public IActionResult GameOver()
        {
            var startTime = DateTime.Parse(HttpContext.Session.GetString("StartTime"));
            var totalTime = DateTime.Now - startTime;
            int score = HttpContext.Session.GetInt32("Score") ?? 0;

            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var gameSession = new GameSession
            {
                UserId = userId,
                Score = score,
                StartedAt = startTime,
                EndedAt = DateTime.Now,
                TimeTaken = totalTime
            };

            _context.GameSessions.Add(gameSession);
            _context.SaveChanges();

            return RedirectToAction("GameResult");
        }

        public IActionResult GameResult()
        {
            int score = HttpContext.Session.GetInt32("Score") ?? 0;
            var startTime = DateTime.Parse(HttpContext.Session.GetString("StartTime"));
            var totalTime = DateTime.Now - startTime;

            var viewModel = new GameResultViewModel
            {
                Score = score,
                TimeTaken = totalTime
            };

            return View(viewModel);
        }
    }
}