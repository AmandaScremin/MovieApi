﻿using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Session
    {
        [Required]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        [Required]
        public int CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }

    }
}
