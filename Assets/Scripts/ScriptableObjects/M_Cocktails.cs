using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Cocktail base", menuName = "", order = 2)]
public class M_Cocktails : ScriptableObject
{
    public string name;

    public enum MethodStruct
    {
        Shake,Stir,Toss
    }
    public enum RimmedStruct
    {
        NA,Salted,Sugar
    }

    public MethodStruct method;
    public GarnishStruct garnish;
    //Could be broken down into different liquids, using different scriptable objects for these? will think about it.
    public List<M_Liquid> ingredients;
    public enum GarnishStruct
    {
        LimeWedge,LimeWheel,LemonSlice,PassionFruit,DriedOrange,DriedGrapefruit
    }
    [Range(0,1)]
    public float strength;
}