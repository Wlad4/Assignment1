using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assignment1
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }
        [JsonIgnore]
        public List<Tank> Tanks { get; set; } = new List<Tank>();
    }
}
