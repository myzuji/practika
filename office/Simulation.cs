using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace office
{
    public class Simulation
    {

        Office office;
        List<Person> personsList = new List<Person>();
        Person person;

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
                            
                            office.SetCell(propetyX, propetyY, new Wall());
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
                                                var salary = new Salary();
                                                salary.value = 1000;

                                                cabinetElement.setBonus(salary);
                                            }
                                            break;
                                        }
                                    case "manager":
                                        {
                                            if (objectElement.GetProperty("money").GetUInt32() >= 0)
                                            {
                                                personsList.Add(person);
                                                var manager  = new Manager();
                                                manager.sumMoney = 3500;
                                                cabinetElement.addPerson(manager);
                                            }

                                            break;
                                        }
                                    case "worker":
                                        {
                                            if (objectElement.GetProperty("qualification").GetUInt32() >= 0 && objectElement.GetProperty("amountWork").GetUInt32() >= 0 &&
                                                objectElement.GetProperty("amountTruancy").GetUInt32() >= 0)
                                            {
                                                personsList.Add(person);
                                                var worker = new Worker();
                                                worker.sumMoney = 0;
                                                worker.qualification = 0;
                                                worker.amountWork = 0;
                                                worker.amountTruancy = 0;
                                                cabinetElement.addPerson(worker);
                                            }
                                            break;
                                        }
                                    case "work":
                                        {
                                            if (objectElement.GetProperty("difficulty").GetUInt32() <= 1 && objectElement.GetProperty("difficulty").GetUInt32() >= 10)
                                            {
                                                var work = new Work();
                                                work.difficulty = 1;
                                                cabinetElement.setBonus(work);
                                            }
                                            break;
                                        }
                                    case "truancy":
                                        {
                                            var truancy = new Truancy();
                                            truancy.skipMove = 1;
                                            cabinetElement.setBonus(truancy);
                                            break;
                                        }
                                }
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

        private bool nextStep (Person person)
        {
        
            return true;
        }
    }
}


