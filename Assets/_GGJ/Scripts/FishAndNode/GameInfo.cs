using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

    public int amountMainNodes = 10;
    public int amountFishToWin = 1000;
    public int amountFishToSpawn = 1500;

    public float distToInnerBound = 20;
    public float distToOuterBound = 40;

    public int pOneFish;
    public int pTwoFish;

    CapsuleCollider capCollide;

    IList<NodeMain> capturedNodeMain = new List<NodeMain>();
    void Start() {
        capCollide = GetComponent<CapsuleCollider>();
    }

    void OnTriggerEnter(Collider col) {
        
        if (col.GetComponent<NodeMain>()==null) return;
        NodeMain nm = col.GetComponent<NodeMain>();
        if (nm.isPlayerOne) {
            pOneFish += nm.fishNodes.Count;
            nm.fishNodes.Clear();
        }
        capturedNodeMain.Add(nm);
    }
}
