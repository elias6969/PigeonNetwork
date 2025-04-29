namespace PigeonPostApi.Models
{
    public class Pigeon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HomeRoostId { get; set; }
        public Roost? HomeRoost { get; set; }
        public bool IsAvailable { get; set; }
    }
}
