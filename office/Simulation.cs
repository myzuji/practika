using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace office
{
    public class Simulation
    {

        Office office;
        public Simulation()
        {
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
                            if (checkXY(cellElement))
                            {

                                var wall = new Wall();
                            }
                            break;

                        }
                    case "cabinet":
                        {
                            var objectsElement = cellElement.GetProperty("object");
                            foreach (var objectElement in objectsElement.EnumerateArray())
                            {
                                switch (objectElement.GetProperty("type").ToString())
                                {
                                    case "salary":
                                        {
                                            if (objectElement.GetProperty("value").GetUInt32() > 100 && objectElement.GetProperty("value").GetUInt32() > 1000)
                                            {
                                                var salary = new Salary();
                                            }
                                            break;
                                        }
                                    case "manager":
                                        {
                                            if (objectElement.GetProperty("money").GetUInt32() == 3500)
                                            {
                                                var manager = new Manager();
                                            }

                                            break;
                                        }
                                    case "worker":
                                        {
                                            if (objectElement.GetProperty("type").ToString() == "worker")
                                            {
                                                var worker = new Worker();
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
    } }


 