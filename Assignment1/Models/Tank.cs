namespace Assignment1
{
    public class Tank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Volume { get; set; }
        public double MaxVolume { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
