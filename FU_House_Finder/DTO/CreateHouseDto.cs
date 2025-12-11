using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.DTO
{
    public class CreateHouseDto
    {
        [Required(ErrorMessage = "Tên nhà tr? là b?t bu?c")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "??a ch? là b?t bu?c")]
        public string Address { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên campus là b?t bu?c")]
        public string CampusName { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Giá ?i?n ph?i l?n h?n ho?c b?ng 0")]
        public decimal PowerPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá n??c ph?i l?n h?n ho?c b?ng 0")]
        public decimal WaterPrice { get; set; }
    }
}
