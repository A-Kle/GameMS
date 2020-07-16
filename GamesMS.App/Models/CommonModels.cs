using System.ComponentModel.DataAnnotations;

namespace GamesMS.Models
{
    public enum EntityViewSource
    {
        [Display(Name = "webservice")]
        Webservice,
        [Display(Name = "www application")]
        Application
    }
}
