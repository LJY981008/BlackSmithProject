using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 유저가 설정한 내용을 Json에 직렬화하는 과정
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
/// 직렬화된 데이터를 저장
/// </summary>
/// <typeparam name="T">직렬화를 해주는 클래스</typeparam>
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