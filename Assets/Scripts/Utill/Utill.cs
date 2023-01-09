using UnityEngine;
using UnityEngine.UI;
public static class Utill
{

    public static bool CheakedGrounded(float _y, float _targetY)
    {
        bool cheaked = true;
        if (_targetY - _y >= 0.1f)
            cheaked = false;
        return cheaked;
    }

    /// <summary>
    /// ������Ʈ�� ã�� ����Լ�
    /// </summary>
    /// <param name="_tr">�ֻ��� ������Ʈ�� Transform</param>
    /// <param name="objName">ã�� ������Ʈ�� �̸�</param>
    /// <returns>�������� ���� = null</returns>
    public static Transform FindTransform(Transform _tr, string objName)
    {
        if (_tr.name == objName)
            return _tr;
        for (int i = 0; i < _tr.childCount; i++)
        {
            Transform findTr = FindTransform(_tr.GetChild(i), objName);
            if (findTr != null)
                return findTr;
        }
        return null;
    }

    /// <summary>
    /// �ػ� ũ�⸦ ����ڿ��� ���߾� ����
    /// </summary>
    /// <param name="_canvas">���� Scene�� �������� Canvas</param>
    public static void SetResolution(Canvas _canvas)
    {
        CanvasScaler thisCanvasScaler = _canvas.GetComponent<CanvasScaler>();
        //Default �ػ� ����
        float fixedAspectRatio = 9f / 16f;
        //���� �ػ��� ����
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        //���� �ػ� ���� ������ �� �� ���
        if (currentAspectRatio > fixedAspectRatio) thisCanvasScaler.matchWidthOrHeight = 0;
        //���� �ػ��� ���� ������ �� �� ���
        else if (currentAspectRatio < fixedAspectRatio) thisCanvasScaler.matchWidthOrHeight = 1;
    }
}
