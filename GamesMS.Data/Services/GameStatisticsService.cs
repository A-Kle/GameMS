using GamesMS.Core.Helpers;
using GamesMS.Core.Services;
using GamesMS.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace GamesMS.Data.Services
{
    public interface IGameStatisticsService : IDependency
    {
        IEnumerable<GameStatistic> GetStatistics();
        void SaveStatistics(GameStatistic statistic);
    }

    public class GameStatisticsService : IGameStatisticsService
    {
        private readonly IMemoryCache memoryCache;

        public GameStatisticsService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IEnumerable<GameStatistic> GetStatistics()
        {
            FixedSizeFiloCollection<GameStatistic> statistics;

            if (!memoryCache.TryGetValue(GameStatisticsConsts.GamesStatisticsCacheKey, out statistics))
            {
                statistics = new FixedSizeFiloCollection<GameStatistic>(GameStatisticsConsts.GamesStatisticsCollectionSize);

                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));

                memoryCache.Set(GameStatisticsConsts.GamesStatisticsCacheKey, statistics, cacheOptions);
            }

            return statistics;
        }

        public void SaveStatistics(GameStatistic statistic)
        {
            FixedSizeFiloCollection<GameStatistic> statistics;

            if (!memoryCache.TryGetValue(GameStatisticsConsts.GamesStatisticsCacheKey, out statistics))
            {
                statistics = new FixedSizeFiloCollection<GameStatistic>(GameStatisticsConsts.GamesStatisticsCollectionSize);

                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));

                memoryCache.Set(GameStatisticsConsts.GamesStatisticsCacheKey, statistics, cacheOptions);
            }

            statistics.Push(statistic); //ref updates in memory without set
        }
    }
}
