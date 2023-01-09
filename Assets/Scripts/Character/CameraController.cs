using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// ī�޶� �ٶ󺸴� �� ������ �̵����� ���� Z�� �������� �ٲٱ����� ����
    /// </summary>
    private Vector3 reverseDistance;
    /// <summary>
    /// ī�޶�� �÷��̾���� �Ÿ�
    /// </summary>
    private float distance;
    private void Awake()
    {
        reverseDistance = new Vector3();
        distance = 3f;
    }
    /// <summary>
    /// ī�޶� ȸ�� �̺�Ʈ
    /// </summary>
    /// <param name="playerCharacter">�÷��̾� ĳ���� ��ũ��Ʈ</param>
    /// <param name="_x">X�� ��</param>
    /// <param name="_y">Y�� ��</param>
    public void CameraRotation(PlayerCharacter playerCharacter, float _x, float _y)
    {
        Transform pivot = Utill.FindTransform(playerCharacter.transform, "Head_M");
        transform.rotation = Quaternion.Euler(_y, _x, 0);
        reverseDistance = new Vector3(0f, 0f, distance);
        transform.position = pivot.position - transform.rotation * reverseDistance;
    }

}
