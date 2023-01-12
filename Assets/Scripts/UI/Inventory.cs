using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Inventory : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public List<TitleButton> itemList;
    public Image selectedIcon;
    private Transform selectItem;
    

    private void Awake()
    {
        itemList = new List<TitleButton>();
        selectedIcon.gameObject.SetActive(false);
        for(int i = 0; i < 48; i++)
        {
            itemList.Add(Utill.FindTransform(transform, $"item ({i})").GetComponent<TitleButton>());
        }

    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        foreach (var item in itemList)
        {
            if (item.IsInRect(_eventData.position))
            {
                selectItem = item.transform;
                selectedIcon.gameObject.SetActive(true);
                // 컬러 대신 이미지를 가져와 적용 빈슬롯에는 작동 안하게 적용
                selectedIcon.color = Color.black;
                selectedIcon.rectTransform.position = _eventData.position;
                break;
            }
        }
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        selectedIcon.gameObject.SetActive(false);
        foreach (var item in itemList)
        {
            if (item.IsInRect(_eventData.position))
            {
                Vector3 temp = selectItem.position;
                selectItem.position = item.transform.position;
                item.transform.position = temp;
                break;
            }
        }
    }

    public void OnDrag(PointerEventData _eventData)
    {
        if (selectedIcon.enabled)
            selectedIcon.rectTransform.position = _eventData.position;
    }
}
