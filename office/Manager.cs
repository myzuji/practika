using System;
using System.Collections.Generic;
using System.Linq;

public class Manager : Person
{
    public int sumMoney = 0;
    List<Person> personList = new List<Person>();
    public Manager()
    {
    }
    override public void movementCells(Cell nextCell)
    {
        base.movementCells(nextCell);
        if (nextCell.bonusVariable != null)
        {

            if (nextCell.bonusVariable is Salary)
            {
                var salary = new Salary();
                sumMoney += salary.value;
            }
            var worker = new Worker();
            if (nextCell.personList.Contains(worker))
            {
                if (worker.amountWork < worker.amountTruancy)
                {
                    var penalty = (worker.amountWork - worker.amountTruancy) * 100;
                    worker.sumMoney -= penalty;
                }

                if (worker.amountWork > worker.amountTruancy)
                {
                    var prize = (worker.amountWork - worker.amountTruancy) * 100;
                    worker.sumMoney += prize;
                }
            }
        }

    }

}
