using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Subject
{
    void Resister(Observer observer);
    void Remove(Observer observer);
    void Notify();
}
public interface Observer
{
    void ObserverUpdate(string crystalName, int name, int num);
}
