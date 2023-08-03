using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Liquid Base", menuName = "", order = 1)]
public class M_Liquid : ScriptableObject
{
    public string name;
    public enum _LiquidType
    {
        Spirit,Citrus,Soda,Mixer,Sugar
    }
    public _LiquidType liquidType;
    [Tooltip("Strength normalized to 1")]
    [Range(0, 1)] public float strength;
}
