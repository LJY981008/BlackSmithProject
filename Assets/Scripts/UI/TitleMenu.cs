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
/// Ÿ��Ʋ ȭ���� ��ư���� �̺�Ʈ�� �����ϴ� ��ũ��Ʈ
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
    /// Ÿ��Ʋ �޴��� ��ư Ŭ�� �̺�Ʈ
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
    /// Scene ���� �ڷ�ƾ ȣ�� �Լ�
    /// </summary>
    /// <param name="_sceneName">ȣ���� Scene�� �̸�<!param>
    private void SetScene(string _sceneName)
    {
        foreach (var btn in btnList)
        {
            btn.gameObject.SetActive(false);
        }
        StartCoroutine(SceneLoading(_sceneName));
    }
    /// <summary>
    /// ���ε� �ڷ�ƾ
    /// </summary>
    /// <param name="_name">ȣ���� Scene�� �̸�</param>
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
            // Scene�ε� %
            startText.text = "Loading" + (operation.progress * 100) + "%";
            if (operation.progress >= 0.9f)
            {
                // Scene�ε� �Ϸ�� �˸�
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
