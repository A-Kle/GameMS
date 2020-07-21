using System;
using System.ComponentModel.DataAnnotations;

namespace GamesMS.Data.Models
{
    public class GameStatistic
    {
        public int GameId { get; set; }
        public DateTime ViewedDate { get; set; }
        public EntityViewSource Source { get; set; }
    }

    public enum EntityViewSource
    {
        [Display(Name = "webservice")]
        Webservice,
        [Display(Name = "www application")]
        Application
    }
}
