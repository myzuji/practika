using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

public class Person
{
    public Cell cells = null;
    public string Type { get; set; }

    public Person()
    {
    }

    virtual public void movementCells(Cell nextCell, bool isLoading = true)
    {
        nextCell.addPerson(this);
        if (cells != null)
        {
            cells.removePerson(this);
        }
        cells = nextCell;


    }



}
