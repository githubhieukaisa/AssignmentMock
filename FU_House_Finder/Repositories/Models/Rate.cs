using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Repositories.Models
{
    public class Rate
    {
        public int Id { get; set; }

        public int HouseId { get; set; }

        public int StudentId { get; set; }

        [Range(1, 5, ErrorMessage = "Star rating must be between 1 and 5")]
        public int Star { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
