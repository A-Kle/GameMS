using FluentNHibernate.Mapping;
using GamesMS.Core.Models;

namespace GamesMS.Records
{
    public class BoardGameRecord : GameRecord
    {
        public virtual int MaxPlayersNumber { get; set; }
        public virtual int MinPlayersNumber { get; set; }
        public virtual int MinPlayerAge { get; set; }
    }


    public class BoardGameMapping : ClassMap<BoardGameRecord>, IRecordMapping
    {
        public BoardGameMapping()
        {
            Table("GamesMS_Records_BoardGameRecord");
            Id(g => g.Id).GeneratedBy.Identity();
            Map(g => g.Name).Length(255);
            Map(g => g.MaxPlayersNumber);
            Map(g => g.MinPlayersNumber);
            Map(g => g.MinPlayerAge);
        }
    }
}
