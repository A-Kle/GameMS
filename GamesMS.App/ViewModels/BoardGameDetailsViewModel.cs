using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesMS.ViewModels
{
    public class BoardGameDetailsViewModel
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public int MaxPlayersNumber { get; set; }
        public int MinPlayersNumber { get; set; }
        public int MinPlayerAge { get; set; }

        public IList<GameStatisticViewModel> Statistics { get; set; }
    }

    public class GameStatisticViewModel
    {
        public DateTime ViewedDate { get; set; }
        public string Source { get; set; }
    }
}
