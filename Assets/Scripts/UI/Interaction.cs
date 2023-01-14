using System.Collections;
using System.Collections.Generic;
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
        if(colls != null)
        {
            temp = colls.Find(o => Vector3.Distance(o.transform.position, transform.position) <= 2f
                                && o.tag != "Terrain" && o.tag != "Player");
            if(temp != null)
            {
                if(coll == temp)
                {
                    colls.Clear();
                }
                else
                {
                    coll = temp;
                    colls.Clear();
                }
            }
        }
        if(coll != null)
        {
            panel.gameObject.SetActive(SetUI());
            panel.transform.position = Camera.main.WorldToScreenPoint(coll.transform.position + panelOffset);
        }
    }
    /// <summary>
    /// 오브젝트와의 거리가 충족여부와 카메라 시야안에 있는지 체크
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
            if (Vector3.Distance(coll.transform.position, transform.position) < 2f)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 일정 시간마다 주변 오브젝트 탐색
    /// </summary>
    /// <returns></returns>
    public IEnumerator SearchInteractionObject()
    {
        while (true)
        {
            colls = Physics.OverlapSphere(transform.position, 30f).ToList();
            yield return new WaitForSeconds(5f);
        }
    }
}
