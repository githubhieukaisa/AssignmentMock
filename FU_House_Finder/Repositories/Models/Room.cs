namespace FU_House_Finder.Repositories.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int HouseId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public float Area { get; set; }

        public int MaxPeople { get; set; }

        public RoomStatus Status { get; set; }

        public string Description { get; set; } = string.Empty;
    }

    public enum RoomStatus
    {
        Available = 0,
        Rented = 1
    }
}
