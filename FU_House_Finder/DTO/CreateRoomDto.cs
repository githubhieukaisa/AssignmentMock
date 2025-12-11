namespace FU_House_Finder.DTO
{
    public class CreateRoomDto
    {
        public int HouseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public float Area { get; set; }
        public int MaxPeople { get; set; }
        public int Status { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
