using UnityEngine;
/// <summary>
/// 荤款靛 包府 概聪历
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
