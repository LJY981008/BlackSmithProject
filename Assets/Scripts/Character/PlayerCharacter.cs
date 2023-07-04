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
            animator.SetBool("isGround", !isJump);
            animator.SetBool("isJump", isJump);
        }
        else {
            animator.SetBool("isGround", !isJump);
            animator.SetBool("isJump", isJump);
        }
    }
    public void Mining()
    {
        StopAllCoroutines();
        animator.Play("Mining");
        StartCoroutine(MiningLoop());
    }
    public IEnumerator MiningLoop()
    {
        yield return new WaitForSeconds(3f);
        animator.Play("Idle");
    }
}
