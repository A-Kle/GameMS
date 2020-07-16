using System.Collections.Generic;
using System.ComponentModel;

namespace GamesMS.ViewModels
{
    public class GamesIndexViewModel
    {
        public GamesIndexViewModel()
        {
            GameModels = new List<GamesModel>();
        }

        public IList<GamesModel> GameModels { get; set; }
        [DefaultValue(1)]
        public int PageNumber { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }

    public class GamesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
