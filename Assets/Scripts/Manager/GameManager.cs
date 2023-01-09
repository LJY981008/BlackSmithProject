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
    private bool isFlying = false;
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
                Debug.Log("컨트롤러가 존재하지 않음");
                return;
            }
            if (controller.isGrounded)
            {
                moveDir = new Vector3(
                    Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDir = controller.transform.TransformDirection(moveDir);
                moveDir *= playerCharacter.characterData.Speed;
                if (isJump == false && Input.GetButton("Jump"))
                {
                    controller.transform.rotation = Quaternion.Euler(0, 45, 0);
                    isJump = true;
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

            if (!Input.GetButton("Jump"))
            {
                isJump = false;
                isFlying = false;
            }

            controller.Move(moveDir * Time.deltaTime);
        }
    }
}
