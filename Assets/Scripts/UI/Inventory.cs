using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class Inventory : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public List<TitleButton> itemList;
    public Image selectedIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDetail;
    private Transform selectItem;
    private Vector3 defaultPos;

    private void Awake()
    {
        defaultPos = Vector3.zero;
        itemList = new List<TitleButton>();
        selectedIcon.gameObject.SetActive(false);
        for(int i = 0; i < 48; i++)
        {
            itemList.Add(Utill.FindTransform(transform, $"item ({i})").GetComponent<TitleButton>());
        }
        itemName.text = "";
        itemDetail.text = "";
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].IsInRect(_eventData.position))
            {
                selectItem = itemList[i].transform;
                if (selectItem.childCount > 0)
                {
                    Image _icon = selectItem.GetChild(0).GetComponent<Image>(); 
                    selectedIcon.gameObject.SetActive(true);
                    selectedIcon.sprite = _icon.sprite;
                    selectedIcon.color = _icon.color;
                    selectedIcon.rectTransform.position = _eventData.position;
                }
                break;
            }
        }
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        selectedIcon.gameObject.SetActive(false);
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].IsInRect(_eventData.position))
            {
                if (itemList[i].transform == selectItem)
                {
                    if(selectItem.childCount > 0)
                        SetComment(itemList[i].transform.GetChild(0).GetComponent<Image>());
                }
                else if(itemList[i].transform.childCount > 0)
                {
                    Transform destSlot = itemList[i].transform.GetChild(0);
                    Transform startSlot = selectItem.GetChild(0);
                    destSlot.SetParent(selectItem);
                    startSlot.SetParent(itemList[i].transform);
                    destSlot.localPosition = defaultPos;
                    startSlot.localPosition = defaultPos;
                }
                else
                {
                    Transform startSlot = selectItem.GetChild(0);
                    startSlot.SetParent(itemList[i].transform);
                    startSlot.localPosition = defaultPos;
                }
                break;
            }
        }
    }

    public void OnDrag(PointerEventData _eventData)
    {
        if (selectedIcon.enabled)
            selectedIcon.rectTransform.position = _eventData.position;
    }
    public void SetComment(Image _selected)
    {
        string _name = _selected.sprite.name;
        switch (_name)
        {
            case "ingots":
                {
                    itemName.text = "√∂";
                    itemDetail.text = "√∂±§ºÆ¿Ã¥Ÿ";
                }
                break;
            default:
                {
                    itemName.text = "???";
                    itemDetail.text = "??????";
                }
                break;
        }
    }
}
