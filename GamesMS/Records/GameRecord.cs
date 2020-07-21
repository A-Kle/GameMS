using FluentNHibernate.Mapping;
using GamesMS.Core.Models;
using System.Collections.Generic;

namespace GamesMS.Records
{
    public abstract class GameRecord : BaseRecord
    {
        public virtual string Name { get; set; }
    }
}
