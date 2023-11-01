using System;

public class Worker : Person
{
    public int sumMoney = 0;
    public int qualification = 0;
    public int amountWork = 0;
    public int amountTruancy = 0;
 

    public Worker()
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
            if (nextCell.bonusVariable is Truancy)
            {
                //var truancy = new Truancy();
                movementCells(nextCell);
                amountTruancy++;
            }
            if (nextCell.bonusVariable is Work)
            {
                var work = new Work();
                if (work.difficulty <= qualification)
                {
                    qualification += work.difficulty;
                    amountWork++;
                }

            }

        }
    }


}
