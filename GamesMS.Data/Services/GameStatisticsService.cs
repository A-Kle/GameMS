using GamesMS.Core.Helpers;
using GamesMS.Core.Services;
using GamesMS.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamesMS.Data.Services
{
    public interface IGameStatisticsService : IDependency
    {
        IDictionary<int, FixedSizeFiloCollection<GameStatistic>> GetStatistics();
        void SaveStatistics(int gameId, GameStatistic statistic);
        IEnumerable<GameStatistic> GetStatisticsForGame(int gameId);
    }

    public class GameStatisticsService : IGameStatisticsService
    {
        private readonly IMemoryCache memoryCache;

        public GameStatisticsService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IDictionary<int, FixedSizeFiloCollection<GameStatistic>> GetStatistics()
        {
            IDictionary<int, FixedSizeFiloCollection<GameStatistic>> statistics;

            if (!memoryCache.TryGetValue(GameStatisticsConsts.GamesStatisticsCacheKey, out statistics))
            {
                statistics = new Dictionary<int, FixedSizeFiloCollection<GameStatistic>>();

                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));

                memoryCache.Set(GameStatisticsConsts.GamesStatisticsCacheKey, statistics, cacheOptions);
            }

            return statistics;
        }

        public IEnumerable<GameStatistic> GetStatisticsForGame(int gameId)
        {
            var statistics = GetStatistics();

            if (!statistics.ContainsKey(gameId))
                return Enumerable.Empty<GameStatistic>();
            else
                return statistics[gameId];
        }

        public void SaveStatistics(int gameId, GameStatistic statistic)
        {
            IDictionary<int, FixedSizeFiloCollection<GameStatistic>> statistics;

            if (!memoryCache.TryGetValue(GameStatisticsConsts.GamesStatisticsCacheKey, out statistics))
            {
                statistics = new Dictionary<int, FixedSizeFiloCollection<GameStatistic>>();

                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));

                memoryCache.Set(GameStatisticsConsts.GamesStatisticsCacheKey, statistics, cacheOptions);
            }

            if(!statistics.ContainsKey(gameId))
                statistics.Add(gameId, new FixedSizeFiloCollection<GameStatistic>(GameStatisticsConsts.GamesStatisticsCollectionSize)); //ref updates in memory without set

            statistics[gameId].Push(statistic);
        }
    }
}
