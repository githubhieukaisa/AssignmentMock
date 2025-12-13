namespace FU_House_Finder.DTO
{
    public class HouseDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CampusName { get; set; } = string.Empty;
        public decimal PowerPrice { get; set; }
        public decimal WaterPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<RoomDto> Rooms { get; set; } = new();
    }
}
