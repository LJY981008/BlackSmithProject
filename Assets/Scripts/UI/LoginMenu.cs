using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System.Collections;
/// <summary>
/// 로그인메뉴
/// </summary>
delegate void Del();
public class LoginMenu : MonoBehaviour, IPointerDownHandler
{
    public GameObject registMenu;
    public List<TitleButton> btnList;
    public List<TMP_InputField> loginField;
    private ToastMessage toast;
    Del del;
    private void Awake()
    {
        toast = Utill.FindTransform(transform.root, "Toast").GetComponent<ToastMessage>();
        
    }
    /// <summary>
    /// 로그인 대기 코루틴
    /// </summary>
    private IEnumerator Loging()
    {
        List<string> _list = new List<string>();
        string id = loginField[0].text;
        string pw = loginField[1].text;
        if (id.Trim() == string.Empty ||
            pw.Trim() == string.Empty)
        {
            toast.ShowMessage(5, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            _list.Add(id);
            _list.Add(pw);
            ClientManager.Instance.Login(_list);
            while (ClientManager.Instance.triggerLogin == -1)
            {
                toast.ShowMessage(8, 0.3f);
                yield return new WaitForSeconds(0.3f);
            }
            if (ClientManager.Instance.triggerLogin == 0)
            {
                gameObject.SetActive(false);
                toast.ShowMessage(6, 1f);
            }
            else if (ClientManager.Instance.triggerLogin == 1)
            {
                toast.ShowMessage(1, 1f);
                ClientManager.Instance.triggerLogin = -1;
            }
            else if (ClientManager.Instance.triggerLogin == 2)
            {
                toast.ShowMessage(7, 1f);
                ClientManager.Instance.triggerLogin = -1;
            }
            
        }
        del -= SetCorutine;
    }
    private void SetCorutine()
    {
        StartCoroutine(Loging());
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
                    if (del==null)
                    {
                        del = new Del(SetCorutine);
                        del();
                    }
                }
                break;
            default:
                break;
        }
    }
}
