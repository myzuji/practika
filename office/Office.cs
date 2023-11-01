using System;
using System.Collections.Generic;

public class Office
{
    public List<List<Cell>> officeArray { get; set; }
    public Office(int width, int height)
    {

        officeArray = new List<List<Cell>>();
        for (int i = 0; i < width; i++)
        {
            officeArray.Add(new List<Cell>());
            for (int k = 0; k < height; k++)
            {
                officeArray[i].Add(new Wall());

            }
        }
    }

    public void SetCell(int x, int y, Cell cell) 
    {
        cell.xCell = x;
        cell.yCell = y;
        officeArray[y][x] = cell;
    }

    


}
