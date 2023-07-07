using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SmithyMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public List<TitleButton> itemList;
    public TitleButton buyBtn;
    public TitleButton upgradeBtn;
    public GameObject itemInfo;
    public Image detailInfoImage;
    public TextMeshProUGUI detailInfoName;
    public TextMeshProUGUI detailInfoStep;

    private bool isSelected = false;
    private int percent;
    private int rate;
    private Transform selectItem;
    private void Awake()
    {
        itemList = new List<TitleButton>();
    }
    public void OnPointerDown(PointerEventData _eventData)
    {
        if (buyBtn.IsInRect(_eventData.position))
            selectItem = buyBtn.transform;
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].IsInRect(_eventData.position))
                {
                    selectItem = itemList[i].transform;
                }
            }
        }
    }
    public void OnPointerUp(PointerEventData _eventData)
    {
        if (buyBtn.IsInRect(_eventData.position))
        {
            GameObject info = Instantiate(itemInfo);
            info.transform.parent = Utill.FindTransform(transform, "SmithContent");
            info.transform.localScale = Vector3.one;
            itemList.Add(info.GetComponent<TitleButton>());
            return;
        }
        else if (upgradeBtn.IsInRect(_eventData.position))
        {
            if (!isSelected) return;
            else
            {
                percent = Random.Range(1, 100);
                Debug.Log(rate + ":" + percent);
                if (rate >= percent)
                {
                    Debug.Log("성공");
                    int s = int.Parse(detailInfoStep.text);
                    s++;
                    detailInfoStep.SetText(s.ToString());
                }
                else Debug.Log("실패");
                return;
            }
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].IsInRect(_eventData.position))
                {
                    if (itemList[i].transform == selectItem)
                    {
                        detailInfoName.text = Utill.FindTransform(selectItem, "Name").GetComponent<TextMeshProUGUI>().text;
                        detailInfoImage.sprite = Utill.FindTransform(selectItem, "Icon").GetComponent<Image>().sprite;
                        detailInfoStep.text = Utill.FindTransform(selectItem, "Step").GetComponent<TextMeshProUGUI>().text;
                        int.TryParse(Utill.FindTransform(selectItem, "Rate").GetComponent<TextMeshProUGUI>().text, out rate);
                        isSelected = true;
                    }
                }
            }
        }
    }
   
}
