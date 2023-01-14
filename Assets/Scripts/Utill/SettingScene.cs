using System;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Scene이 생성될 때 초기화
/// </summary>
public class SettingScene : MonoBehaviour
{
    public Canvas canvas;
    public PlayerCharacter playerCharacter;
    public CameraController cameraController;

    Scene scene;
    private void Awake()
    {
        try
        {
            // 씬이 변경되어 스폰될 때 현재 씬의 정보를 매니저에 세팅
            scene = SceneManager.GetActiveScene();
            GameObject thisScene = gameObject;
            Utill.SetResolution(canvas);
            // 내용 작성
            GameManager.Instance.thisScene = thisScene;
            GameManager.Instance.thisCanvas = canvas;
            SoundsManager.Instance.backgroundAudioSource = Camera.main.transform.GetComponent<AudioSource>();
            if (scene.name.Contains("Town"))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameManager.Instance.cameraController = cameraController;
                GameManager.Instance.playerCharacter = playerCharacter;
                GameManager.Instance.controller = playerCharacter.controller;
                GameManager.Instance.playerData = playerCharacter.characterData;
                GameManager.Instance.inventory = Utill.FindTransform(transform, "Inventory").gameObject;
                GameManager.Instance.InteractionPanel = Utill.FindTransform(transform, "Interaction").gameObject;
            }
            //세팅이 끝나면 현재씬의 정보 변경
            GameManager.Instance.currentScene = scene.name;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
