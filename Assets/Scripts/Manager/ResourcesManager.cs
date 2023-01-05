using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ҽ� ���� �Ŵ���
/// </summary>
public class ResourcesManager : Singleton<ResourcesManager>
{
    protected ResourcesManager() { }
    [HideInInspector] public AudioClip backgroundAudio;

    private string bgmPath = "Audio/Warriors";
    private void Start()
    {
        backgroundAudio = Resources.Load<AudioClip>(bgmPath);
        SoundsManager.Instance.backgroundAudioSource.clip = backgroundAudio;
        SoundsManager.Instance.backgroundAudioSource.Play();
    }
}
