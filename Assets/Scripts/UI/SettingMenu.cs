using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SettingMenu : MonoBehaviour, IPointerDownHandler
{
    private TitleButton btnExit;

    private void Awake()
    {
        btnExit = Utill.FindTransform(transform, "Exit").GetComponent<TitleButton>();
    }
    /// <summary>
    /// 종료버튼
    /// </summary>
    public void OnPointerDown(PointerEventData _eventData)
    {
        if (btnExit.IsInRect(_eventData.position))
        {
            gameObject.SetActive(false);
        }
    }
}
