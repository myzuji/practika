using System;

public class Worker : Person
{
	public int sumMoney = 0;
	public int qualification = 0;
	public int amountWork = 0;
	public int amountTruancy = 0;
    public Bonus bonus;

    public Worker()
	{
	}
	override public void movementCells(Cell nextCell)
	{ 
		base.movementCells(nextCell);
	}

	public void PerformanceWork(Cell cellBonus)
	{
        cellBonus.setBonus(bonus);
		qualification++;
		amountWork++;
	}

    public void Truancy(Cell cellBonus)
	{
        cellBonus.setBonus(bonus);
        amountTruancy++;
	}

    public void GettingASalary(Cell cellBonus)
	{
        cellBonus.setBonus(bonus);
        sumMoney++;
	}
}
