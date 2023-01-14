using System;
using UnityEngine;
/// <summary>
/// 게임 총괄 매니저
/// </summary>
public class GameManager : Singleton<GameManager>
{
    protected GameManager()
    {
    }
    public GameObject thisScene;
    public Canvas thisCanvas;
    public string currentScene;
    public PlayerCharacter playerCharacter;
    public CharacterController controller;
    public CharacterData playerData;
    public CameraController cameraController;
    public GameObject inventory;
    public GameObject InteractionPanel;

    /// <summary>
    /// 카메라 이동량 변수
    /// </summary>
    private float cameraXmove = 0f;
    private float cameraYmove = 0f;
    /// <summary>
    /// 캐릭터 조작 변수
    /// </summary>
    private Vector3 moveDir = Vector3.zero;
    private bool isJump = false;
    private bool isGround = true;

    /// <summary>
    /// 인벤토리 변수
    /// </summary>
    private bool isOpen = false;
    void Start()
    {
        SaveInfoToJson.LoadSetting();
        currentScene = "TitleScene";
    }
    void Update()
    {
        if (currentScene == "TitleScene")
            return;
        if (currentScene == "TownScene")
        {
            Controlling();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen && InteractionPanel.activeSelf)
                {
                    InteractionPanel.SetActive(true);
                    Debug.Log("대장간");
                }
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!isOpen)
                {
                    inventory.SetActive(true);
                    isOpen = true;
                }
                else
                {
                    inventory.SetActive(false);
                    isOpen = false;
                }
            }
        }
    }
    /// <summary>
    /// 이동, 점프, 카메라 컨트롤
    /// </summary>
    private void Controlling()
    {
        isGround = controller.isGrounded;
        cameraXmove += Input.GetAxis("Mouse X");
        cameraYmove -= Input.GetAxis("Mouse Y");
        cameraController.CameraRotation(playerCharacter, cameraXmove, cameraYmove);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            var offset = cameraController.transform.forward;
            offset.y = 0;
            playerCharacter.transform.LookAt(controller.transform.position + offset);
        }

        if (isGround)
        {
            moveDir = new Vector3(
                Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = controller.transform.TransformDirection(moveDir);
            moveDir *= playerCharacter.characterData.Speed;
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                playerCharacter.isMove = true;
            else
                playerCharacter.isMove = false;
            if (!isJump && Input.GetButton("Jump"))
            {
                isJump = true;
                playerCharacter.isJump = true;
                moveDir.y = playerCharacter.characterData.JumpPower;
            }
        }
        else
        {
            isGround = Utill.CheckGround(controller);
            if (!isGround)
                moveDir.y -= playerData.Gravity * Time.deltaTime;
        }

        if (!Input.GetButton("Jump") &&
            isGround)
        {
            isJump = false;
            playerCharacter.isJump = false;
        }
        controller.Move(moveDir * Time.deltaTime);
    }
}
