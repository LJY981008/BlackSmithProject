using System;
using UnityEngine;
/// <summary>
/// ���� �Ѱ� �Ŵ���
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

    /// <summary>
    /// ī�޶� �̵��� ����
    /// </summary>
    private float cameraXmove = 0f;
    private float cameraYmove = 0f;
    /// <summary>
    /// ĳ���� ���� ����
    /// </summary>
    private Vector3 moveDir = Vector3.zero;
    private bool isJump = false;
    private bool isFlying = false;

    /// <summary>
    /// �κ��丮 ����
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
            cameraXmove += Input.GetAxis("Mouse X");
            cameraYmove -= Input.GetAxis("Mouse Y");
            cameraController.CameraRotation(playerCharacter, cameraXmove, cameraYmove);
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                var offset = cameraController.transform.forward;
                offset.y = 0;
                playerCharacter.transform.LookAt(controller.transform.position + offset);
            }

            if (controller == null)
            {
                Debug.Log("��Ʈ�ѷ��� �������� ����");
                return;
            }
            
            if (controller.isGrounded)
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
                if (moveDir.y < 0 && !isJump && Input.GetButton("Jump"))
                    isFlying = true;
                if (isFlying)
                {
                    isJump = true;
                    moveDir.y *= 0.95f;
                    if (moveDir.y > -1) moveDir.y = -1;
                    moveDir.x = Input.GetAxis("Horizontal");
                    moveDir.z = Input.GetAxis("Vertical");
                }
                else
                {
                    moveDir.y -= playerData.Gravity * Time.deltaTime;
                }
            }

            if (!Input.GetButton("Jump") &&
                controller.isGrounded)
            {
                isJump = false;
                isFlying = false;
                playerCharacter.isJump = false;
            }
            controller.Move(moveDir * Time.deltaTime);
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
}
