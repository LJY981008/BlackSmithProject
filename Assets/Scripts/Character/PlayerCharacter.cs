using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어 캐릭터 스크립트
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    public CharacterController controller;
    public CharacterData characterData;
    public GameObject terrain;
    public Animator animator;

    public bool isJump = false;
    public bool isMove = false;
    private void Update()
    {
        animator.SetBool("isMove", isMove);
        if (!isJump) {
            animator.SetBool("isGround", true);
            animator.SetBool("isJump", isJump);
        }
        else {
            animator.SetBool("isGround", false);
            animator.SetBool("isJump", isJump);
        }
    }
}
