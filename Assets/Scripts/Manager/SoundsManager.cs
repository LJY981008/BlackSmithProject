using UnityEngine;
/// <summary>
/// ���� ���� �Ŵ���
/// </summary>
public class SoundsManager : Singleton<SoundsManager>
{
    protected SoundsManager() { }
    public AudioSource backgroundAudioSource;
    //public AudioSource effectAudioSource;
    private void OnDestroy()
    {
    }
}
