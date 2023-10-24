using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace office
{
    public class Simulation
    {

        Office office;
        Cell cell;
        public Simulation()
        {
        }

        public bool loadFromJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            var jsonDoc = JsonDocument.Parse(jsonString);

            var officeElement = jsonDoc.RootElement;

            if (officeElement.GetProperty("type").ToString() != "office" || officeElement.GetProperty("width").GetInt32() <= 0 || officeElement.GetProperty("height").GetInt32() <= 0)
            {
                return false;
            }

            if (officeElement.GetProperty("cells").GetArrayLength() <= 0)
            {
                return false;
            }

            office = new Office(officeElement.GetProperty("width").GetInt32(), officeElement.GetProperty("height").GetInt32());


            var cellsElement = officeElement.GetProperty("cells");

            foreach (var cellElement in cellsElement.EnumerateArray())
            {
                if (!checkXY(cellElement))
                {
                    continue;

                }
                int propetyX = cellElement.GetProperty("x").GetInt32();
                int propetyY = cellElement.GetProperty("y").GetInt32();
                switch (cellElement.GetProperty("type").ToString())
                {

                    case "wall":
                        {
                            {
                                office.SetCell(propetyX, propetyY, new Wall());
                            }
                            break;

                        }
                    case "cabinet":
                        {
                            var cabinetElement = new Cabinet();
                            {
                                office.SetCell(propetyX, propetyY, cabinetElement);

                            }

                            var objectsElement = cellElement.GetProperty("objects");

                            foreach (var objectElement in objectsElement.EnumerateArray())
                            {
                                switch (objectElement.GetProperty("type").ToString())
                                {
                                    case "salary":
                                        {
                                            if (objectElement.GetProperty("value").GetUInt32() > 0)
                                            {


                                                cabinetElement.setBonus(new Salary());


                                            }
                                            break;
                                        }
                                    case "manager":
                                        {
                                            if (objectElement.GetProperty("money").GetUInt32() >= 0)
                                            {
                                                cabinetElement.addPerson(new Manager());
                                            }

                                            break;
                                        }
                                    case "worker":
                                        {
                                            if (objectElement.GetProperty("type").ToString() == "worker")
                                            {
                                                cabinetElement.addPerson(new Worker());
                                            }

                                            break;
                                        }
                                    case "work":
                                        {
                                            if (objectElement.GetProperty("type").ToString() == "work")
                                            {
                                                cabinetElement.setBonus(new Work());
                                            }

                                            break;
                                        }
                                    case "truancy":
                                        {
                                            if (objectElement.GetProperty("type").ToString() == "truancy")
                                            {
                                                cabinetElement.setBonus(new Truancy());
                                            }

                                            break;
                                        }
                                }
                            }
                            if (checkXY(cellElement))
                            {

                                var cabinet = new Cabinet();

                            }
                            break;
                        }

                }
            }
            return true;

        }
        private bool checkXY(JsonElement cellElement)
        {
            if (cellElement.GetProperty("x").GetInt32() >= 0 && cellElement.GetProperty("y").GetInt32() >= 0)
            {
                return true;
            }
            return false;

        }
    }
}


