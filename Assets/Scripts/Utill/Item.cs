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
    static int[,] myItem = new int[TYPE, NUMBER];
    public ItemData ingotData;
    public ItemData goldData;
    public ItemData diamondData;
    public GameObject itemIcon;
    private void Start()
    {
        myItem[0, 0] = ingotData.Uid;
        myItem[1, 0] = goldData.Uid;
        myItem[2, 0] = diamondData.Uid;
        myItem[0, 1] = 1;
        myItem[1, 1] = 1;
        myItem[2, 1] = 1;
    }
    /// <summary>
    /// 아이템 사용
    /// </summary>
    /// <param name="_uid">아이템 아이디</param>
    /// <param name="n">사용 갯수</param>
    public bool UseItem(int _uid, int n)
    {
        if (myItem[_uid, 1] < n)
            return false;
        myItem[_uid, 1] -= n;
        return true;
    }
    /// <summary>
    /// 아이템 습득
    /// 랜덤으로 생성
    /// </summary>
    public void GetItem()
    {
        int rand = Random.Range(1, 1000);
        if(rand < 700)
        {
            myItem[0, 1]++;
        }
        else if(rand < 900)
        {
            myItem[1, 1]++;
        }
        else
        {
            myItem[2, 1]++;
        }
    }
    public int ReturnItem(int _uid)
    {
        return myItem[_uid, 1];
    }
    /// <summary>
    /// 버그수정필요
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
