using System;

public class Office
{
    Cell[,] officeArray = null;
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
        officeArray[x, y] = cell;
        
    }

    public void SetCabinet(int x, int y,  Cabinet cabinet) 
    {
        officeArray[x, y] = cabinet;
    }

    public void SetPerson(int x, int y, Person person)
    {
        officeArray[x, y] = person;
    }

}
