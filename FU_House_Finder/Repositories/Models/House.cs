namespace FU_House_Finder.Repositories.Models
{
    public class House
    {
        public int Id { get; set; }

        public int LandlordId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string CampusName { get; set; } = string.Empty;

        public decimal PowerPrice { get; set; }

        public decimal WaterPrice { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
