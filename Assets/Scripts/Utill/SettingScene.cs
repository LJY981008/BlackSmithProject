using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Scene�� ������ �� �ʱ�ȭ
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
            // ���� ����Ǿ� ������ �� ���� ���� ������ �Ŵ����� ����
            scene = SceneManager.GetActiveScene();
            GameObject thisScene = gameObject;
            Utill.SetResolution(canvas);
            // ���� �ۼ�
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
                GameManager.Instance.interactionPanel = Utill.FindTransform(transform, "Interaction").gameObject;
                GameManager.Instance.smithyMenu = Utill.FindTransform(transform, "SmithyMenu").gameObject;
                GameManager.Instance.interaction = playerCharacter.transform.GetComponent<Interaction>();
            }
            else if (scene.name.Contains("Cave"))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameManager.Instance.cameraController = cameraController;
                GameManager.Instance.playerCharacter = playerCharacter;
                GameManager.Instance.controller = playerCharacter.controller;
                GameManager.Instance.playerData = playerCharacter.characterData;
                //GameManager.Instance.inventory = Utill.FindTransform(transform, "Inventory").gameObject;
                GameManager.Instance.interactionPanel = Utill.FindTransform(transform, "Interaction").gameObject;
                //GameManager.Instance.smithyMenu = Utill.FindTransform(transform, "SmithyMenu").gameObject;
                GameManager.Instance.interaction = playerCharacter.transform.GetComponent<Interaction>();
                GameManager.Instance.miningGageObj = Utill.FindTransform(transform, "MiningGage").gameObject;
                GameManager.Instance.miningGage = GameManager.Instance.miningGageObj.transform.GetChild(0).GetComponent<Image>();
                GameManager.Instance.dropText = Utill.FindTransform(transform, "RewordText").gameObject.GetComponent<TextMeshProUGUI>();
                GameManager.Instance.objectPooling = Utill.FindTransform(transform, "Mineral").GetComponent<ObjectPooling>();
            }
            //������ ������ ������� ���� ����
            GameManager.Instance.currentScene = scene.name;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
