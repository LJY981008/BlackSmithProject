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
    /// �������� ����
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
            // ���� ����
            Directory.CreateDirectory(pathJson);
            File.WriteAllText(pathJson + jsonName, jsonData);
        }
    }
    // �������� ȣ��
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
            // ����� ������ X
            Debug.Log(e.Message);
        }
    }
}
