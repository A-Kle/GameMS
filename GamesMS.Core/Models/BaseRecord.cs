using System;
using System.Collections.Generic;
using System.Text;

namespace GamesMS.Core.Models
{
    public interface IRecord
    {
        int Id { get; set; }
    }

    public interface IRecordMapping { }

    public abstract class BaseRecord : IRecord
    {
        public virtual int Id { get; set; }
    }
}
