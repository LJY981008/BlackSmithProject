using UnityEngine;
/// <summary>
/// 사운드 관리 매니저
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
