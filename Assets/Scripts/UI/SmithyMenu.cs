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
    public GameObject itemDetail;
    public Image detailInfoImage;
    public TextMeshProUGUI detailInfoName;
    public TextMeshProUGUI detailInfoStep;
    public TextMeshProUGUI resultText;

    private bool isSelected = false;
    private int percent;
    private int rate;
    private Transform selectItem;
    private bool textTrigger = false;
    private string converSation;
    private void Awake()
    {
        itemList = new List<TitleButton>();
    }
    private void OnDisable()
    {
        resultText.text = string.Empty;
    }
    IEnumerator SetText()
    {
        textTrigger = true;
        resultText.text = string.Empty;
        char[] arrayText = converSation.ToCharArray();
        Debug.Log(converSation);
        for (int i = 0; i < arrayText.Length; i++)
        {
            resultText.text += arrayText[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        for(int i = converSation.Length - 1; i > -1; i--)
        {
            converSation = converSation.Substring(0, converSation.Length - 1);
            resultText.text = converSation;
            yield return new WaitForSeconds(0.1f);
        }
        textTrigger = false;
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
                if (textTrigger)
                {
                    StopCoroutine(SetText());
                    resultText.text = string.Empty;
                }
                if (Item.Instance.CheakedItem(1))
                {
                    Item.Instance.UseItem(0, 1);
                    Item.Instance.UseItem(1, 1);
                    Item.Instance.UseItem(2, 1);
                    percent = Random.Range(1, 100);
                    Debug.Log(rate + ":" + percent);
                    if (rate >= percent)
                    {
                        Debug.Log("성공");
                        int s = int.Parse(detailInfoStep.text);
                        s++;
                        detailInfoStep.SetText(s.ToString());
                        converSation = "강화에 성공했습니다.";
                    }
                    else
                    {
                        Debug.Log("실패");
                        converSation = "강화에 실패했습니다.";
                    }
                }
                else
                {
                    converSation = "재료가 부족합니다.";
                    Debug.Log("재료부족");
                }
                StartCoroutine(SetText());
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
                        itemDetail.SetActive(true);
                        detailInfoName.text = Utill.FindTransform(selectItem, "Name").GetComponent<TextMeshProUGUI>().text;
                        detailInfoImage.sprite = Utill.FindTransform(selectItem, "Icon").GetComponent<Image>().sprite;
                        detailInfoStep.text = "+" + Utill.FindTransform(selectItem, "Step").GetComponent<TextMeshProUGUI>().text;
                        int.TryParse(Utill.FindTransform(selectItem, "Rate").GetComponent<TextMeshProUGUI>().text, out rate);
                        isSelected = true;
                    }
                }
            }
        }
    }
   
}
