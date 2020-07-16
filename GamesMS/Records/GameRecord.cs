using FluentNHibernate.Mapping;
using GamesMS.Core.Models;
using System.Collections.Generic;

namespace GamesMS.Records
{
    public abstract class GameRecord : BaseRecord
    {
        public virtual string Name { get; set; }
        public virtual IList<GameStatisticRecord> Statistics { get; set; }

        public virtual void AddStatistics(GameStatisticRecord statisticRecord)
        {
            if (Statistics.Contains(statisticRecord))
                return;

            Statistics.Add(statisticRecord);
        }
    }
}
