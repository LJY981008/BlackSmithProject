using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Object/Character Data", order = int.MaxValue)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }
    [SerializeField]
    private float jumpPower;
    public float JumpPower { get { return jumpPower; } }
    [SerializeField]
    private float gravity;
    public float Gravity { get { return gravity; } }
}
