using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// �α��θ޴�
/// </summary>
public class LoginMenu : MonoBehaviour, IPointerDownHandler
{
    public GameObject registMenu;
    private List<TitleButton> btnList;
    private void Awake()
    { 
        btnList = new List<TitleButton>();
        btnList.Add(Utill.FindTransform(transform, "Exit").GetComponent<TitleButton>());
        btnList.Add(Utill.FindTransform(transform, "Regist").GetComponent<TitleButton>());
        btnList.Add(Utill.FindTransform(transform, "Confirm").GetComponent<TitleButton>());
    }
    public void OnPointerDown(PointerEventData _eventData)
    {
        string selectedBtn = string.Empty;
        foreach (var btn in btnList)
        {
            if (btn.IsInRect(_eventData.position))
            {
                selectedBtn = btn.name;
            }
        }
        switch (selectedBtn)
        {
            case "Exit":
                {
                    gameObject.SetActive(false);
                }
                break;
            case "Regist":
                {
                    gameObject.SetActive(false);
                    registMenu.SetActive(true);

                }
                break;
            case "Confirm":
                {
                    //������ Json���� ���� �� �� �α���
                }
                break;
            default:
                break;
        }
    }
}
