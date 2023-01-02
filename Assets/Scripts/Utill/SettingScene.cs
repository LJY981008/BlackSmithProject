using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Scene�� ������ �� �ʱ�ȭ
/// </summary>
public class SettingScene : MonoBehaviour
{
    Scene scene;
    private void Awake()
    {
        try
        {
            scene = SceneManager.GetActiveScene();
            GameObject thisScene = gameObject;
            Canvas thisCanvas = Utill.FindTransform(thisScene.transform, "Canvas").GetComponent<Canvas>();
            Utill.SetResolution(thisCanvas);
            GameManager.Instance.thisScene = thisScene;
            GameManager.Instance.thisCanvas = thisCanvas;
            GameManager.Instance.currentScene = scene.name;
            SoundsManager.Instance.backgroundAudioSource = Camera.main.transform.GetComponent<AudioSource>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
