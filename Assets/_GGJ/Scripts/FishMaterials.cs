using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaterials : MonoBehaviour {
    public Material _Neutral;
    public Material _P1;
    public Material _P2;
    private static FishMaterials main;
    public static Material Neutral {
        get { return main._Neutral; }
    }
    public static Material P1 {
        get { return main._P1; }
    }
    public static Material P2 {
        get { return main._P2; }
    }
    void Awake() {
        main = this;
    }
}
