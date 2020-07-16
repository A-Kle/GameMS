using GamesMS.Core.Services;

namespace GamesMS.Records
{
    public interface IGameStatisticRepository : IRepository<GameStatisticRecord>, IDependency
    {
    }

    public class GameStatisticRepository : Repository<GameStatisticRecord>, IGameStatisticRepository
    {
        public GameStatisticRepository(ISessionService sessionService) : base(sessionService)
        {
        }

        public override void CreateOrUpdate(GameStatisticRecord gameStatisticRecord)
        {
            gameStatisticRecord.Game.AddStatistics(gameStatisticRecord);
            base.CreateOrUpdate(gameStatisticRecord);
        }
    }
}

