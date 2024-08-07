﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hangmo.Repository.Data.Entities


{
    
    public enum GameResult
    {
        Win,
        Loss,
        None
    }
    
    public enum GameStatus
    {   InProgress,
        Ended
    }

    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public string AppUserId { get; set; }
        
        [Required]
        public int WordId { get; set; }
        
        public int PointsEarned { get; set; } = 0;

        public int WrongGuessCount { get; set; } = 0;

        public int RevealedCharactersCount { get; set; } = 0;

        public List<char> GuessHistory { get; set; } = new List<char>();

        public GameStatus Status { get; set; } 


        public GameResult Result { get; set; } 



        [ForeignKey("AppUserId")]
        [Required]
        public AppUser? AppUser { get; set; }

        [ForeignKey("WordId")]
        [Required]
        public Word? Word { get; set; }

        public Game(string appUserId, int wordId )
        {
            AppUserId = appUserId;
            WordId = wordId;
            Status = GameStatus.InProgress;
            Result= GameResult.None;
        }

    }
}
