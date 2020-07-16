namespace GamesMS.Api.Models
{
    public class BoardGameDetailsModel : GameModel
    {
        public int MaxPlayersNumber { get; set; }
        public int MinPlayersNumber { get; set; }
        public int MinPlayerAge { get; set; }
    }
}
