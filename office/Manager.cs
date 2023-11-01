using System;
using System.Collections.Generic;
using System.Linq;

public class Manager : Person
{
    public int sumMoney = 0;
   
    public Manager()
    {
        Type = "manager";
    }
    override public void movementCells(Cell nextCell, bool isLoading = true)
    {
        base.movementCells(nextCell);
        if (isLoading)
        {
            return;
        }

        if (nextCell.bonusVariable != null)
        {

            if (nextCell.bonusVariable is Salary)
            {
                var salary = nextCell.bonusVariable as Salary;
                sumMoney += salary.value;
                nextCell.setBonus(null);
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
