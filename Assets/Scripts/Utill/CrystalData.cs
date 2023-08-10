using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalData : MonoBehaviour, Subject
{
    private List<Observer> listObserver = new List<Observer>();
    private string crystalName = null;
    private int itemName = -1;
    private int itemNum = -1;

    public void UpdateData(string crystalName, int name, int num)
    {
        this.crystalName = crystalName;
        itemName = name;
        itemNum = num;
        Notify();
    }

    public void Notify()
    {
        foreach (var observer in listObserver)
        {
            observer.ObserverUpdate(crystalName, itemName, itemNum);
        }
    }

    public void Remove(Observer observer)
    {
        listObserver.Remove(observer);
    }

    public void Resister(Observer observer)
    {
        listObserver.Add(observer);
    }
}
