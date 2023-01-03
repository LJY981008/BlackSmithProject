using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ ������ ������ Json�� ����ȭ�ϴ� ����
/// </summary>
[Serializable]
public class SaveInfo
{
    //[SerializeField] float effectSound;
    [SerializeField] float backgroundSound;
    /*public float EFFECTSOUND
    {
        get { return effectSound; }
        set { effectSound = value; }
    }*/
    public float BACKGROUNDSOUND
    {
        get { return backgroundSound; }
        set { backgroundSound = value; }
    }
    public SaveInfo()
    {

    }
    public SaveInfo(/*float _effectSound,*/ float _backgroundSound)
    {
        //effectSound = _effectSound;
        backgroundSound = _backgroundSound;
    }

}
/// <summary>
/// ����ȭ�� �����͸� ����
/// </summary>
/// <typeparam name="T">����ȭ�� ���ִ� Ŭ����</typeparam>
[Serializable]
public class Serialization<T>
{
    [SerializeField]
    T _t;
    public T toReturn() { return _t; }
    public Serialization(T _tmp)
    {
        _t = _tmp;
    }
}