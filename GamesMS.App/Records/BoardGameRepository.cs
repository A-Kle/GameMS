using GamesMS.Core.Services;

namespace GamesMS.Records
{
    public interface IBoardGameRepository : IRepository<BoardGameRecord>, IDependency
    {
    }

    public class BoardGameRepository : Repository<BoardGameRecord>, IBoardGameRepository
    {
        public BoardGameRepository(ISessionService sessionService) : base(sessionService)
        {
        }
    }
}
