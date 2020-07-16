using System.ComponentModel.DataAnnotations;

namespace GamesMS.ViewModels
{
    public class BoardGameCreateUpdateViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(255, ErrorMessage = "Name length can not exceed 255 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        public int MaxPlayersNumber { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        public int MinPlayersNumber { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        public int MinPlayerAge { get; set; }
    }
}
