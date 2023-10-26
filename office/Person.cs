using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

public class Person
{
    public Cell cells = null;


    public Person()
    {
    }

    public void movementCells(Cell nextCell)
    {
        nextCell.addPerson(this);
        cells.removePerson(this);
        cells = nextCell;

    }



}
