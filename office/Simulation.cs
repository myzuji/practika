﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Windows.Media.Media3D;

namespace office
{
    public class Simulation
    {

        public Office office { get; set; }
        List<Person> personsList { get; set; }
        int index = 0;

        public Simulation()
        {
            personsList = new List<Person>();
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

                JsonElement temp;
                int propetyX = cellElement.GetProperty("x").GetInt32();
                int propetyY = cellElement.GetProperty("y").GetInt32();

                switch (cellElement.GetProperty("type").ToString())
                {

                    case "wall":
                        {
                            var wallElement = new Wall();
                            office.SetCell(propetyX, propetyY, wallElement);
                            break;

                        }
                    case "cabinet":
                        {
                            var cabinetElement = new Cabinet();
                            office.SetCell(propetyX, propetyY, cabinetElement);

                            if (!cellElement.TryGetProperty("objects", out temp))
                                continue;

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
                                                manager.movementCells(cabinetElement);
                                                personsList.Add(manager);
                                            }

                                            break;
                                        }
                                    case "worker":
                                        {
                                            if (objectElement.TryGetProperty("qualification", out temp) && objectElement.GetProperty("qualification").GetInt32() >= 0)
                                            {
                                                var worker = new Worker();
                                                if (objectElement.TryGetProperty("money", out temp) && objectElement.GetProperty("money").GetInt32() >= 0)
                                                    worker.sumMoney = objectElement.GetProperty("money").GetInt32();
                                                worker.qualification = objectElement.GetProperty("qualification").GetInt32();
                                                worker.movementCells(cabinetElement);
                                                personsList.Add(worker);
                                            }
                                            break;
                                        }
                                    case "work":
                                        {
                                            if (objectElement.TryGetProperty("difficulty", out temp) && objectElement.GetProperty("difficulty").GetInt32() >= 1 &&
                                                objectElement.GetProperty("difficulty").GetInt32() <= 10)
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

        public void nextStep()
        {


            Person person = personsList[index];

            index++;

            if (index >= personsList.Count)
            {
                index = 0;
            }

            List<Cell> cellsList = new List<Cell>();

            var x = person.cells.xCell;
            var y = person.cells.yCell;

            for (int i = y - 1; i <= y + 1; i++)
            {
                if (i < 0 || i >= office.officeArray.Count)
                    continue;

                if (i != y && office.officeArray[i][x] is Cabinet)
                {
                    cellsList.Add(office.officeArray[i][x]);
                }
            }

            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= office.officeArray[y].Count)
                    continue;

                if (i != x && office.officeArray[y][i] is Cabinet)
                {
                    cellsList.Add(office.officeArray[y][i]);
                }
            }

            if (cellsList.Count == 0)
            {
                cellsList.Add(person.cells);
            }

            Random rand = new Random();
            var cellIndex = rand.Next(0, cellsList.Count);
            person.movementCells(cellsList[cellIndex], false);


        }

    }

}










