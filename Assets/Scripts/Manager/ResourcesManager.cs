using UnityEngine;
/// <summary>
/// ���ҽ� ���� �Ŵ���
/// </summary>
public class ResourcesManager : Singleton<ResourcesManager>
{
    protected ResourcesManager() { }
    public GameObject itemIcon;
    [HideInInspector] public AudioClip backgroundAudio;
    private string bgmPath = "Audio/Warriors";
    private string itemIconPath = "UI/ItemIcon";
    private void Start()
    {
        backgroundAudio = Resources.Load<AudioClip>(bgmPath);
        SoundsManager.Instance.backgroundAudioSource.clip = backgroundAudio;
        SoundsManager.Instance.backgroundAudioSource.Play();
        itemIcon = Resources.Load<GameObject>(itemIconPath);
        Item.Instance.itemIcon = itemIcon;
    }
}
