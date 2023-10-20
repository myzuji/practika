using System;
using System.IO;
using System.Text.Json;

namespace office
{
    public class Simulation
    {

        public Simulation()
        {
        Office office;
        }

        public bool loadFromJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            var jsonDoc = JsonDocument.Parse(jsonString);

            var officeElement = jsonDoc.RootElement;

            if (officeElement.GetProperty("type").ToString() != "office" && officeElement.GetProperty("width").GetUInt32() <= 0 && officeElement.GetProperty("height").GetUInt32() <= 0)
            {
                return false;
            }

            if (officeElement.GetProperty("cells").GetArrayLength() <= 0)
            {
                return false;
            }

            office = new Office();

            var cellsElement = officeElement.GetProperty("cells");

            foreach (var cellElement in cellsElement.EnumerateArray())
            {
                switch (cellElement.GetProperty("type").ToString())
                {
                    case "wall":
                        {
                            if (cellElement.GetProperty("x").GetInt32() >= 0 && cellElement.GetProperty("y").GetInt32() >= 0)
                            {
                                var wall = new Wall();
                            }
                            break;
                        }
                }
            }

            return true;
        }
    }
}


 