using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Slider slider;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider = Utill.FindTransform(transform, "Sound").GetComponent<Slider>();
        if (text.transform.name.Contains("ES"))
        {
            //효과음 초기값 넣어주기
            //text.text = "(" + Mathf.RoundToInt(SoundsManager.Instance.backgroundAudioSource.volume * 100) + "%)";
            text.text = "(" + Mathf.RoundToInt(100) + "%)";
        }
        else if (text.transform.name.Contains("BS"))
        {
            text.text = "(" + Mathf.RoundToInt(SoundsManager.Instance.backgroundAudioSource.volume * 100) + "%)";
        }
        slider.value = SoundsManager.Instance.backgroundAudioSource.volume;
    }

    public void VolumeUpdate(float value)
    {
        if (text.transform.name.Contains("ES"))
        {
            // 효과음 볼륨 넣어주기
            text.text = "(" + Mathf.RoundToInt(value * 100) + "%)";
        }
        else if (text.transform.name.Contains("BS"))
        {
            SoundsManager.Instance.backgroundAudioSource.volume = value;
            text.text = "(" + Mathf.RoundToInt(value * 100) + "%)";
        }
    }
}
