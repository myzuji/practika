using office;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

public class Cell
{
    Bonus[] bonusArray = null;
    Person[] personArray = null;

    // List<string> person = new List<string>();

    public Cell(int attribute, string post)
    {


        bonusArray = new Bonus[attribute];

        //for (int i = 0; i < x; i++)
        //{
        for (int i = 0; i < attribute; i++)
        {
            bonusArray[attribute] = new Bonus();

        }

        int postPerson = Convert.ToInt32(post);
        personArray = new Person[postPerson];
        for (int i = 0; i < postPerson; i++)
        {
            personArray[postPerson] = new Person();

        }

        //}

    }
    public void setBonus(int value, Bonus bonus)
    {
        bonusArray[value] = bonus;

    }

    public void setPerson(int post, Person person)
    {
        personArray[post] = person;

    }
}



