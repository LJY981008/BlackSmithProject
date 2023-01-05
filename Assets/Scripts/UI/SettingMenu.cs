using UnityEngine;
using UnityEngine.EventSystems;
public class SettingMenu : MonoBehaviour, IPointerDownHandler
{
    private TitleButton btnExit;

    private void Awake()
    {
        btnExit = Utill.FindTransform(transform, "Exit").GetComponent<TitleButton>();
    }
    /// <summary>
    /// �����ư
    /// </summary>
    public void OnPointerDown(PointerEventData _eventData)
    {
        if (btnExit.IsInRect(_eventData.position))
        {
            SaveInfoToJson.SaveSetting();
            gameObject.SetActive(false);
        }
    }
}
