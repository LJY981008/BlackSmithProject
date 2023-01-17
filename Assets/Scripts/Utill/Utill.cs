using UnityEngine;
using UnityEngine.UI;
public static class Utill
{
    /// <summary>
    /// 랜덤 포지션으로 레이캐스트
    /// </summary>
    /// <param name="_origin"> 중심위치 </param>
    /// <param name="rangeX"> x축 반경 </param>
    /// <param name="rangeZ"> z축 반경 </param>
    /// <returns></returns>
    public static Vector3 RandomPos(Vector3 _origin, float rangeX, float rangeZ)
    {
        Vector3 origin = _origin;
        float randX = Random.Range(origin.x - rangeX, origin.x + rangeX);
        float randZ = Random.Range(origin.z - rangeZ, origin.z + rangeZ);
        origin.x = randX;
        origin.z = randZ;
        origin.y += 200f;
        int layerMask = (1 << LayerMask.NameToLayer("Terrain")) + (1 << LayerMask.NameToLayer("Item"));
        RaycastHit hitInfo;
        if (Physics.Raycast(origin, Vector3.down, out hitInfo, Mathf.Infinity, layerMask))
        {
            if (hitInfo.transform.gameObject.layer == (1 << LayerMask.NameToLayer("Item")))
                return RandomPos(_origin, rangeX, rangeZ);
            else
                return hitInfo.point;
        }
        return RandomPos(_origin, rangeX, rangeZ);
    }
    /// <summary>
    /// 바닥체크 개선
    /// </summary>
    /// <param name="_controller">캐릭터 컨트롤러</param>
    /// <returns></returns>
    public static bool CheckGround(CharacterController _controller)
    {
        if (_controller.isGrounded)
            return true;
        var ray = new Ray(_controller.transform.position + Vector3.up * 0.1f, Vector3.down);
        var maxDistance = 0.3f;
        return Physics.Raycast(ray, maxDistance, 3);
    }
    /// <summary>
    /// 커서 위치에 상호작용이 가능한 오브젝트가 있는지 체크
    /// </summary>
    /// <param name="_player">플레이어</param>
    /// <param name="_distance">측정거리</param>
    /// <returns></returns>
    public static bool CheckInteraction(GameObject _player, float _distance)
    {
        bool cheak = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _distance))
        {
            Debug.Log(hit.collider.tag);
        }
        return cheak;
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
