using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assignment1
{
    public class Factory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public List<Unit> Units { get; set; } = new List<Unit>();
    }


}
