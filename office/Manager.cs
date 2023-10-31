using System;

public class Manager : Person
{
	public int sumMoney = 0;
    public Bonus bonus;
    public Manager()
	{
	}
    override public void movementCells(Cell nextCell)
    {
        base.movementCells(nextCell);
    }
   public void ActingWithAWorker(Cell cell)
    {
        ;
    }

    public void GettingASalary(Cell cellBonus)
    {
        cellBonus.setBonus(bonus);
        sumMoney ++;
    }
}
