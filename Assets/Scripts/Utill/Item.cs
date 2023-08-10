using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : Singleton<Item>
{
    protected Item()
    {
    }
    const int TYPE = 3;
    const int NUMBER = 2;
    public int[,] myItem = new int[TYPE, NUMBER];
    public ItemData ingotData;
    public ItemData goldData;
    public ItemData diamondData;
    public GameObject itemIcon;
    public RunningObserver observer;
    public string cristalName = null;
    private void Start()
    {
        myItem[0, 0] = ingotData.Uid;
        myItem[1, 0] = goldData.Uid;
        myItem[2, 0] = diamondData.Uid;
        myItem[0, 1] = 5;
        myItem[1, 1] = 5;
        myItem[2, 1] = 5;
    }
    /// <summary>
    /// ������ ���
    /// </summary>
    /// <param name="_uid">������ ���̵�</param>
    /// <param name="n">��� ����</param>
    public void UseItem(int _uid, int n)
    {
        myItem[_uid, 1] -= n;
    }
    /// <summary>
    /// ������ ���� üũ
    /// </summary>
    /// <param name="n">����� ������ ����</param>
    /// <returns></returns>
    public bool CheakedItem(int n)
    {
        for (int i = 0; i < 3; i++)
        {
            if (myItem[i, 1] < n) return false;
        }
        return true;
    }
    /// <summary>
    /// ������ ����
    /// �������� ����
    /// </summary>
    public void GetItem()
    {
        Debug.Log(cristalName);
        observer.SetCrystal(cristalName);
        
    }
    public int ReturnItem(int _uid)
    {
        return myItem[_uid, 1];
    }
    /// <summary>
    /// ���׼����ʿ�
    /// </summary>
    public void SetInventory()
    {
        GameObject temp = Instantiate<GameObject>(itemIcon, Vector3.zero, Quaternion.identity);
        temp.transform.parent = Utill.FindTransform(GameManager.Instance.thisCanvas.transform, "item (0)");
        temp.GetComponent<Image>().sprite = ingotData.Image;
        RectTransform rect = temp.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(50, 50);
        rect.localPosition = Vector3.zero;

        temp = Instantiate<GameObject>(itemIcon, Vector3.zero, Quaternion.identity);
        temp.transform.parent = Utill.FindTransform(GameManager.Instance.thisCanvas.transform, "item (1)");
        temp.GetComponent<Image>().sprite = goldData.Image;
        rect = temp.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(50, 50);
        rect.localPosition = Vector3.zero;

        temp = Instantiate<GameObject>(itemIcon, Vector3.zero, Quaternion.identity);
        temp.transform.parent = Utill.FindTransform(GameManager.Instance.thisCanvas.transform, "item (2)");
        temp.GetComponent<Image>().sprite = diamondData.Image;
        rect = temp.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(50, 50);
        rect.localPosition = Vector3.zero;
    }
}
