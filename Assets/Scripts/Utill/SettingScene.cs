using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Scene이 생성될 때 초기화
/// </summary>
public class SettingScene : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            GameObject thisScene = gameObject;
            Canvas thisCanvas = Utill.FindTransform(thisScene.transform, "Canvas").GetComponent<Canvas>();
            Utill.SetResolution(thisCanvas);
            GameManager.Instance.thisScene = thisScene;
            GameManager.Instance.thisCanvas = thisCanvas;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
