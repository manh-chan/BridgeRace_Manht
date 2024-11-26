using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : GameUnit
{
    public ColorType colorType;
    [SerializeField] private Renderer _renderer;
    public void ChangeColor(ColorType colorType) { 
        this.colorType = colorType;
        this._renderer.material = DataManager.Instance.colorData.GetColor(colorType);
    }
}
