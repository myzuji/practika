using office;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

public class Cell
{
    Bonus bonusVariable = null;

    List<Person> personList = new List<Person>();

    public Cell()
    {
    }
    public void setBonus(Bonus bonus)
    {
        bonusVariable = bonus;

    }

    public void addPerson(Person person)
    {
        personList.Add(person);

    }

    public void removePerson(Person person)
    {
        personList.Remove(person);

    }
}



