using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorData : ScriptableObject
{
    [SerializeField] private Material[] colorMaterial;

    public Material GetColor(ColorType colorType) 
    { 
        return colorMaterial[(int)colorType];
    }
}
