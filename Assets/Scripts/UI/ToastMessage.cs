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
        msgList.Add("성공적으로 가입되었습니다");
        msgList.Add("비밀번호를 확인해주세요");
        msgList.Add("사용중인 닉네임입니다");
        msgList.Add("사용중인 아이디입니다");
        msgList.Add("사용중인 이메일입니다");
        msgList.Add("빈공간을 채워주세요");
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

        Color current = Color.clear; /* (0, 0, 0, 0) = 검은색 글자, 투명도 100% */
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
