using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 카메라가 바라보는 앞 방향을 이동량에 따른 Z축 방향으로 바꾸기위한 벡터
    /// </summary>
    private Vector3 reverseDistance;
    /// <summary>
    /// 카메라와 플레이어와의 거리
    /// </summary>
    private float distance;
    private void Awake()
    {
        reverseDistance = new Vector3();
        distance = 3f;
    }
    /// <summary>
    /// 카메라 회전 이벤트
    /// </summary>
    /// <param name="playerCharacter">플레이어 캐릭터 스크립트</param>
    /// <param name="_x">X축 값</param>
    /// <param name="_y">Y축 값</param>
    public void CameraRotation(PlayerCharacter playerCharacter, float _x, float _y)
    {
        Transform pivot = Utill.FindTransform(playerCharacter.transform, "Head_M");
        transform.rotation = Quaternion.Euler(_y, _x, 0);
        reverseDistance = new Vector3(0f, 0f, distance);
        transform.position = pivot.position - transform.rotation * reverseDistance;
    }

}
