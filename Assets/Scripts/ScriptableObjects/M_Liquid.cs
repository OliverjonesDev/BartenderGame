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
    [Tooltip("Amount measured in CL")]
    [Range(.1f,2)]public float amount;
    [Tooltip("Strength normalized to 1")]
    [Range(0, 1)] public float strength;
    [Tooltip("Pour options: These affect the overall amount into the drink")]
    [Range(-1, 1)] public float overPour,underPour;
}
