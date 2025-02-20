using KbcProject.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KbcProject.Models
{
    public class GameSession
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key reference to UserAccount
        public int Score { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }

        [ForeignKey("UserId")]
        public UserAccount User { get; set; }


    }
}
