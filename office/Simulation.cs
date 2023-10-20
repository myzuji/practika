using System;
using System.IO;
using System.Text.Json;

namespace office
{
    public class Simulation
    {

        public class Rootobject
        {
            public string type { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public Cell[] cells { get; set; }
        }

        public class Cell
        {
            public string type { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public Object[] objects { get; set; }
        }

        public class Object
        {
            public string type { get; set; }
            public int value { get; set; }
            public int money { get; set; }
        }

        public Simulation()
        {
        }

        public bool loadFromJson(string filePath)
        {
            filePath = "test.json";
            string jsonString = File.ReadAllText(filePath);
            Simulation simulation = JsonSerializer.Deserialize<Simulation>(jsonString)!;
            
            return true;
        }
        
    }
}