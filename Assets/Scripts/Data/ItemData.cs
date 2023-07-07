using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private int uid;
    public int Uid { get { return uid; } }
    [SerializeField]
    private string name;
    public string Name{ get { return name; } }
    [SerializeField]
    private Sprite image;
    public Sprite Image { get { return image; } }

}
