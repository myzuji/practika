using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

public class Person
{
	Cell cells = null;
    Cabinet cabinet;
    
    public Person()
    {
        cells = new Cell();
        cabinet = new Cabinet();
        

    }

	public void movementCells(Person person)
	{
        
        while (cells == cabinet)
        {
            cells.addPerson(person);
            var cabinet = new Person();
            cells = cabinet.cells;
           
          
        }
            
        //else if(cells != cabinet)
        //{
        //    cells.removePerson(person);
        //}

          
            
    }
}
