using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Windows.Media.Media3D;

namespace office
{
    public class Simulation
    {

        Office office;
        List<Person> personsList = new List<Person>();
        int index = 0;


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
                                            if (objectElement.GetProperty("value").GetInt32() > 0)
                                            {
                                                var salary = new Salary();
                                                salary.value = objectElement.GetProperty("value").GetInt32();

                                                cabinetElement.setBonus(salary);
                                            }
                                            break;
                                        }
                                    case "manager":
                                        {
                                            if (objectElement.GetProperty("money").GetInt32() >= 0)
                                            {
                                                var manager = new Manager();
                                                manager.sumMoney = objectElement.GetProperty("money").GetInt32();
                                                //cabinetElement.addPerson(manager);
                                                Person.movementCells(manager);
                                                personsList.Add(manager);
                                            }

                                            break;
                                        }
                                    case "worker":
                                        {
                                            if (objectElement.GetProperty("qualification").GetInt32() >= 0)
                                            {
                                                var worker = new Worker();
                                                worker.sumMoney = objectElement.GetProperty("sumMoney").GetInt32();
                                                worker.qualification = objectElement.GetProperty("qualification").GetInt32();
                                                //cabinetElement.addPerson(worker);
                                                Person.movementCells(worker);
                                                personsList.Add(worker);
                                            }
                                            break;
                                        }
                                    case "work":
                                        {
                                            if (objectElement.GetProperty("difficulty").GetInt32() >= 1 && objectElement.GetProperty("difficulty").GetInt32() >= 10)
                                            {
                                                var work = new Work();
                                                work.difficulty = objectElement.GetProperty("difficulty").GetInt32();
                                                cabinetElement.setBonus(work);
                                            }
                                            break;
                                        }
                                    case "truancy":
                                        {
                                            cabinetElement.setBonus(new Truancy());
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
        
        private void nextStep()
        {

            Person person = personsList[index];
            index++;

            if (index >= personsList.Count)
            {
                index = 0;
            }
           
            if (office.officeArray[x, y] is Wall)
            {
                ;
            }
            
            if (office.officeArray[x,y] is Cabinet)
            {
                Person.movementCells(person);
            }
        }



    }


}
   



