using System;

public class Worker : Person
{
    public int sumMoney = 0;
    public int qualification = 0;
    public int amountWork = 0;
    public int amountTruancy = 0;


    public Worker()
    {
        Type = "worker";
    }
    override public void movementCells(Cell nextCell, bool isLoading = true)
    {

        base.movementCells(nextCell);

        if (isLoading)
            return;

        bool skipStep = true;
        if (skipStep)
        {
            skipStep = false;
            return;
        }
        if (nextCell.bonusVariable != null)
        {

            if (nextCell.bonusVariable is Salary)
            {
                var salary = nextCell.bonusVariable as Salary;
                sumMoney += salary.value;
            }
            else if (nextCell.bonusVariable is Truancy)
            {
                amountTruancy++;
                skipStep= true;
            }
            else if (nextCell.bonusVariable is Work)
            {
                var work = new Work();
                if (work.difficulty <= qualification)
                {
                    qualification ++;
                    amountWork++;
                }
                
            }
            nextCell.bonusVariable = null;

        }
    }


}
