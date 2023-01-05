using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class RegistMenu : MonoBehaviour, IPointerDownHandler
{
    private List<TitleButton> btnList;
    private ToastMessage toast;
    private GameObject loginMenu;
    public List<TMP_InputField> infoList;
    private void Awake()
    {
        btnList = new List<TitleButton>();
        btnList.Add(Utill.FindTransform(transform, "Confirm").GetComponent<TitleButton>());
        btnList.Add(Utill.FindTransform(transform, "Cancel").GetComponent<TitleButton>());
        toast = Utill.FindTransform(transform.root, "Toast").GetComponent<ToastMessage>();
        loginMenu = Utill.FindTransform(transform.root, "Login").gameObject;
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
            case "Confirm":
                {
                    string inputNickName = infoList[0].text;
                    string inputID = infoList[1].text;
                    string inputPW = infoList[2].text;
                    string inputPWConfirm = infoList[3].text;
                    string inputEmail = infoList[4].text;
                    if (inputNickName.Trim() == string.Empty || 
                        inputID.Trim() == string.Empty ||
                        inputPW.Trim() == string.Empty ||
                        inputPWConfirm.Trim() == string.Empty ||
                        inputEmail.Trim() == string.Empty)
                    {
                        toast.ShowMessage(5, 1f);
                    }
                    else if (inputPW != inputPWConfirm)
                    {
                        toast.ShowMessage(1, 1f);
                    }
                    else
                    {
                        //서버 Json에 저장
                        //중복 닉네임, 아이디, 이메일 확인
                        toast.ShowMessage(0, 1f);
                        foreach(var item in infoList)
                        {
                            item.text = string.Empty;
                        }
                        loginMenu.SetActive(true);
                        gameObject.SetActive(false);
                    }
                }
                break;
            case "Cancel":
                {
                    foreach (var item in infoList)
                    {
                        item.text = string.Empty;
                    }
                    loginMenu.SetActive(true);
                    gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
