﻿using System.ComponentModel.DataAnnotations;

namespace PortfolioTrackerShared.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}