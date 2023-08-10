using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, Observer
{
    private CrystalData crystalData = null;    
    public void Init(CrystalData _crystalData)
    {
        this.crystalData = _crystalData;
    } 

    public void ObserverUpdate(string crystalName, int name, int num)
    {
        if (transform.name == crystalName)
        {
            Item.Instance.myItem[name, 1] += num;
            Debug.Log($"æ∆¿Ã≈€{Item.Instance.myItem[name, 0]} = {Item.Instance.myItem[name, 1]}");
        }
    }
}
