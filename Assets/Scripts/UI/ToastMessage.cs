using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToastMessage : MonoBehaviour
{
    private TextMeshProUGUI toast;
    private float fadeInOutTime = 0.3f;
    private string msg;
    private List<string> msgList;
    private Color originColor;
    private void Awake()
    {
        toast = transform.GetComponent<TextMeshProUGUI>();
        msgList = new List<string>();
        msg = string.Empty;
        originColor = new Color();
        originColor = toast.color;
        msgList.Add("���������� ���ԵǾ����ϴ�");
        msgList.Add("��й�ȣ�� Ȯ�����ּ���");
        msgList.Add("������� �г����Դϴ�");
        msgList.Add("������� ���̵��Դϴ�");
        msgList.Add("������� �̸����Դϴ�");
        msgList.Add("������� ä���ּ���");
    }
    public void ShowMessage(int i, float duration)
    {
        toast.enabled = true;
        msg = msgList[i];
        StartCoroutine(showMessageCoroutine(msg, duration));
    }
    private IEnumerator fadeInOut(float _duration, bool inOut)
    {
        float start, end;
        if (inOut)
        {
            start = 0.0f;
            end = 1.0f;
        }
        else
        {
            start = 1.0f;
            end = 0f;
        }

        Color current = Color.clear; /* (0, 0, 0, 0) = ������ ����, ���� 100% */
        float elapsedTime = 0.0f;

        while (elapsedTime < _duration)
        {
            float alpha = Mathf.Lerp(start, end, elapsedTime / _duration);
            Color color = originColor;
            color.a = alpha;
            toast.color = color;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    private IEnumerator showMessageCoroutine(string _msg, float _duration)
    {
        toast.text = _msg;

        yield return fadeInOut(fadeInOutTime, true);

        float elapsedTime = 0.0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return fadeInOut(fadeInOutTime, false);
        toast.color = originColor;
    }
}
