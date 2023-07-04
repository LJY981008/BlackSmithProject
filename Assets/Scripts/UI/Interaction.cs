using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Interaction : MonoBehaviour
{
    public Image panel;
    List<Collider> colls;
    Collider coll;
    Collider temp;
    Vector3 viewPos;
    Vector3 panelOffset;
    private void Awake()
    {
        colls = new List<Collider>();
        viewPos = new Vector3();
        panelOffset = new Vector3(0.8f, 0, 0);
    }
    void Start()
    {
        panel.gameObject.SetActive(false);
        StartCoroutine(SearchInteractionObject());
    }
    private void Update()
    {
        
        if (colls.Count > 0)
        {
            temp = colls[0];
            if (coll == temp)
            {
                colls.Clear();
            }
            else
            {
                coll = temp;
                colls.Clear();
            }

        }
        if(coll != null)
        {
            panel.gameObject.SetActive(SetUI());
            if (panel.gameObject.activeSelf)
            {
                GameManager.Instance.interactionTarget = coll.transform;
                panel.transform.position = Camera.main.WorldToScreenPoint(coll.transform.position + panelOffset);
            }
        }
    }
    /// <summary>
    /// ������Ʈ���� �Ÿ��� �������ο� ī�޶� �þ߾ȿ� �ִ��� üũ
    /// </summary>
    /// <returns>True, False</returns>
    public bool SetUI()
    {
        viewPos = Camera.main.WorldToViewportPoint(coll.transform.position);
        if (viewPos.x >= 0 &&
            viewPos.x <= 1 &&
            viewPos.y >= 0 &&
            viewPos.y <= 1)
        {
            if (Vector3.Distance(coll.transform.position, transform.position) < 5f)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// ���� �ð����� �ֺ� ������Ʈ Ž��
    /// </summary>
    /// <returns></returns>
    public IEnumerator SearchInteractionObject()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Interaction");
        while (true)
        {
            colls = Physics.OverlapSphere(transform.position, 30f, layerMask).ToList();
            if(colls.Count > 1)
            {
                colls = DistanceCompare(colls);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    /// <summary>
    /// ����Ʈ�� ������ �÷��̾���� �Ÿ� ������ ����
    /// </summary>
    /// <param name="_list">������ ����Ʈ</param>
    /// <returns></returns>
    public List<Collider> DistanceCompare(List<Collider> _list)
    {
        List<Collider> list = _list;
        list.Sort(new Comparison<Collider>((n1, n2) => 
                        Vector3.Distance(n1.transform.position, transform.position)
                        .CompareTo(Vector3.Distance(n2.transform.position, transform.position))));
        return list;
    }
}
