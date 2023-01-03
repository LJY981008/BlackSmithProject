using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveInfoToJson
{
    static string pathJson = Path.Combine(Application.dataPath, "/Documents/BlackSmith/");
    static string infoJson = "Info.json";
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
            File.WriteAllText(pathJson + infoJson, jsonData);
        }
        catch(Exception e)
        {
            // ���� ����
            Directory.CreateDirectory(pathJson);
            File.WriteAllText(pathJson + infoJson, jsonData);
        }
    }
    // �������� ȣ��
    public static void LoadSetting()
    {
        try
        {
            SaveInfo info = new SaveInfo();
            string loadJson = File.ReadAllText(pathJson + infoJson);
            info = JsonUtility.FromJson<Serialization<SaveInfo>>(loadJson).toReturn();
            Debug.Log(loadJson + " : " + info.BACKGROUNDSOUND );
            SoundsManager.Instance.backgroundAudioSource.volume = (float)(info.BACKGROUNDSOUND);
        }
        catch (Exception e)
        {
            // ����� ������ X
            Debug.Log(e.Message);
        }
    }
}