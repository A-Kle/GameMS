using FluentNHibernate.Mapping;
using GamesMS.Core.Models;
using GamesMS.Models;
using System;

namespace GamesMS.Records
{
    public class GameStatisticRecord : BaseRecord
    {
        public virtual BoardGameRecord Game { get; set; }
        public virtual DateTime ViewedDate { get; set; }
        public virtual EntityViewSource Source { get; set; }
    }

    public class GameStatisticMapping : ClassMap<GameStatisticRecord>, IRecordMapping
    {
        public GameStatisticMapping()
        {
            Table("GamesMS_Records_GameStatisticRecord");
            Id(g => g.Id).GeneratedBy.Identity(); //due to the record type and usage this could be replaced with guid or other generated string, to avoid unlimited incremental
            References(x => x.Game, "GameId");
            Map(g => g.ViewedDate);
            Map(g => g.Source);
        }
    }
}
