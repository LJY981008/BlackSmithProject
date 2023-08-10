using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningObserver : MonoBehaviour
{
    private CrystalData crystalData = null;
    private List<Crystal> crystals = new List<Crystal>();
    private string[] itemName = { "ingot", "Gold", "Diamond" };

    private void Start()
    {
        Item.Instance.observer = this;
        crystalData = transform.GetComponent<CrystalData>();

        for(int i = 0; i < transform.childCount; i++)
        {
            crystals.Add(transform.GetChild(i).GetComponent<Crystal>());
            crystals[i].Init(crystalData);
            crystalData.Resister(crystals[i]);            
        }
    }
    public void SetCrystal(string crystalName)
    {
        int randItem = Random.Range(0, 2);
        int randNum = Random.Range(1, 3);
        crystalData.UpdateData(crystalName, randItem, randNum);
    }
}
