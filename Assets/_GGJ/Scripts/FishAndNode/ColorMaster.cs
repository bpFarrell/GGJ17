using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaster : MonoBehaviour {

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color clrNuetrual;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color clrPlayerOne;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color clrPlayerTwo;

    [SerializeField]
    Material defaultFishMaterial;
    public Material getDefaultMaterial {
        get {
            if(defaultFishMaterial == null) defaultFishMaterial = Resources.Load("matFishDefault") as Material;
            return defaultFishMaterial;
            
        }
    }
    public static ColorMaster instance;

    // Use this for initialization
    void Awake() {
        instance = this;
    }

}
