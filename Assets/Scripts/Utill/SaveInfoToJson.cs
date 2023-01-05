using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveInfoToJson
{
    static string pathJson = Path.Combine(Application.dataPath, "/Documents/BlackSmith/");
    static string jsonName = "Info.json";
    /// <summary>
    /// 유저세팅 저장
    /// </summary>
    public static void SaveSetting()
    {
        SaveInfo info = new SaveInfo();
        info.BACKGROUNDSOUND = SoundsManager.Instance.backgroundAudioSource.volume;
        string jsonData = JsonUtility.ToJson(new Serialization<SaveInfo>(info));
        Debug.Log(jsonData);
        try
        {
            File.WriteAllText(pathJson + jsonName, jsonData);
        }
        catch(Exception e)
        {
            // 최초 저장
            Directory.CreateDirectory(pathJson);
            File.WriteAllText(pathJson + jsonName, jsonData);
        }
    }
    // 유저세팅 호출
    public static void LoadSetting()
    {
        try
        {
            SaveInfo info = new SaveInfo();
            string loadJson = File.ReadAllText(pathJson + jsonName);
            info = JsonUtility.FromJson<Serialization<SaveInfo>>(loadJson).toReturn();
            SoundsManager.Instance.backgroundAudioSource.volume = (float)(info.BACKGROUNDSOUND);
        }
        catch (Exception e)
        {
            // 저장된 데이터 X
            Debug.Log(e.Message);
        }
    }
}
