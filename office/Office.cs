using System;

public class Office
{
    public Cell[,] officeArray = null;
    public Office(int width, int height)
    {

        officeArray = new Cell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < height; k++)
            {
                officeArray[i, k] = new Wall();

            }
        }
    }

    public void SetCell(int x, int y, Cell cell) 
    {
        cell.xCell = x;
        cell.yCell = y;
        officeArray[x, y] = cell;
    }

    


}
