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
    public GameObject interactionPanel;
    public GameObject smithyMenu;
    public Interaction interaction;


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

    /// <summary>
    /// 대장장이 작업 변수
    /// </summary>
    private Vector3 smithingPosition;
    private bool isSmithy = false;
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
            if(!isSmithy)
                Controlling();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen && interactionPanel.activeSelf)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    interactionPanel.SetActive(false);
                    isSmithy = true;
                    playerCharacter.isMove = false;
                    isJump = false;
                    playerCharacter.isJump = false;
                    interaction.enabled = false;
                    smithyMenu.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!isOpen)
                {
                    inventory.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    isOpen = true;
                }
                else
                {
                    inventory.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    isOpen = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                smithyMenu.SetActive(false);
                interaction.enabled = true;
                isSmithy = false;
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
