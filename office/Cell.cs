using office;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

public class Cell : INotifyPropertyChanged
{
   public Bonus bonusVariable { get; set; }
    public int xCell = 0;
    public int yCell = 0;
    public ObservableCollection<Person> personList { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public Cell()
    {
        personList = new ObservableCollection<Person>();
    }
    public void setBonus(Bonus bonus)
    {
        bonusVariable = bonus;
        OnPropertyChanged("bonusVariable");
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



