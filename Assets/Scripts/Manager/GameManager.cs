using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// °ÔÀÓ ÃÑ°ý ¸Å´ÏÀú
/// </summary>
public class GameManager : Singleton<GameManager>
{
    protected GameManager()
    {
    }
    [HideInInspector] public GameObject thisScene;
    [HideInInspector] public Canvas thisCanvas;
    [HideInInspector] public string currentScene;
    void Start()
    {
        SaveInfoToJson.LoadSetting();
        currentScene = "TitleScene";
    }
    void Update()
    {
        if(currentScene != "TitleScene")
        {

        }
    }
    
}
