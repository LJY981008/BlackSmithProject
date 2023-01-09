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
    /// 오브젝트를 찾는 재귀함수
    /// </summary>
    /// <param name="_tr">최상위 오브젝트의 Transform</param>
    /// <param name="objName">찾는 오브젝트의 이름</param>
    /// <returns>존재하지 않음 = null</returns>
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
    /// 해상도 크기를 사용자에게 맞추어 적용
    /// </summary>
    /// <param name="_canvas">현재 Scene에 적용중인 Canvas</param>
    public static void SetResolution(Canvas _canvas)
    {
        CanvasScaler thisCanvasScaler = _canvas.GetComponent<CanvasScaler>();
        //Default 해상도 비율
        float fixedAspectRatio = 9f / 16f;
        //현재 해상도의 비율
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        //현재 해상도 가로 비율이 더 길 경우
        if (currentAspectRatio > fixedAspectRatio) thisCanvasScaler.matchWidthOrHeight = 0;
        //현재 해상도의 세로 비율이 더 길 경우
        else if (currentAspectRatio < fixedAspectRatio) thisCanvasScaler.matchWidthOrHeight = 1;
    }
}
