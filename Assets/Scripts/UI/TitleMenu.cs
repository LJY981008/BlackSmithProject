using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using TMPro;

/// <summary>
/// 타이틀 화면의 버튼들의 이벤트를 설정하는 스크립트
/// </summary>
public class TitleMenu : MonoBehaviour, IPointerDownHandler
{
    public List<TitleButton> btnList;
    public Image btnSetting;
    public TextMeshProUGUI startText;

    private void Awake()
    {
        btnList = new List<TitleButton>();
        for(int i = 0; i < transform.childCount; i++)
        {
            btnList.Add(transform.GetChild(i).GetComponent<TitleButton>());
        }
    }
    /// <summary>
    /// 타이틀 메뉴의 버튼 클릭 이벤트
    /// </summary>
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
            case "Btn_GameStart":
                SetScene("TownScene");
                break;
            case "Btn_GameSetting":
                btnSetting.gameObject.SetActive(true);
                break;
            case "Btn_GameExit":
                if (EditorApplication.isPlaying)
                    EditorApplication.isPlaying = false;
                else
                    Application.Quit();
                Debug.Log("exit");
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Scene 설정 코루틴 호출 함수
    /// </summary>
    /// <param name="_sceneName">호출할 Scene의 이름<!param>
    private void SetScene(string _sceneName)
    {
        foreach (var btn in btnList)
        {
            btn.gameObject.SetActive(false);
        }
        StartCoroutine(SceneLoading(_sceneName));
    }
    /// <summary>
    /// 씬로드 코루틴
    /// </summary>
    /// <param name="_name">호출할 Scene의 이름</param>
    private IEnumerator SceneLoading(string _name)
    {
        yield return null;
        Color tmpColor = new Color();
        float alphaValue = 0.005f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(_name);
        operation.allowSceneActivation = false;
        startText.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            // Scene로딩 %
            startText.text = "Loading" + (operation.progress * 100) + "%";
            if (operation.progress >= 0.9f)
            {
                // Scene로딩 완료시 알림
                startText.text = "Press Space to Start";
                tmpColor = startText.color;
                if(tmpColor.a >= 0.8f)
                    alphaValue = -0.005f;
                else if(tmpColor.a <= 0.2f)
                    alphaValue = 0.005f;
                tmpColor.a += alphaValue;
                startText.color = tmpColor;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startText.gameObject.SetActive(false);
                    foreach (var btn in btnList)
                    {
                        btn.gameObject.SetActive(true);
                    }
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
