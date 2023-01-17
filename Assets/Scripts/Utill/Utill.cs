using UnityEngine;
using UnityEngine.UI;
public static class Utill
{
    /// <summary>
    /// ���� ���������� ����ĳ��Ʈ
    /// </summary>
    /// <param name="_origin"> �߽���ġ </param>
    /// <param name="rangeX"> x�� �ݰ� </param>
    /// <param name="rangeZ"> z�� �ݰ� </param>
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
    /// �ٴ�üũ ����
    /// </summary>
    /// <param name="_controller">ĳ���� ��Ʈ�ѷ�</param>
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
    /// Ŀ�� ��ġ�� ��ȣ�ۿ��� ������ ������Ʈ�� �ִ��� üũ
    /// </summary>
    /// <param name="_player">�÷��̾�</param>
    /// <param name="_distance">�����Ÿ�</param>
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
