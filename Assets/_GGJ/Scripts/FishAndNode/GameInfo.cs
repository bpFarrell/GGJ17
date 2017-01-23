using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

    public int amountMainNodes = 10;
    public int amountFishToWin = 1000;
    public int amountFishToSpawn = 1500;
    public int amountFishDelta = 0;

    public float distToInnerBound = 20;
    public float distToOuterBound = 40;

    public int pOneFish;
    public int pTwoFish;
    public int totalFishScored {
        get { return pOneFish + pTwoFish; }
    }

    Swirl swirl;

    CapsuleCollider capCollide;

    IList<NodeMain> capturedNodeMain = new List<NodeMain>();
    void Start() {
        capCollide = GetComponent<CapsuleCollider>();
        swirl = GetComponent<Swirl>();
    }
    void Update() {
        if (totalFishScored >= amountFishToWin) {
            GameStateManager._MAIN.ChangeState(GameStateManager.STATE.END);
        }
    }

    void OnTriggerEnter(Collider col) {
        
        if (col.GetComponent<NodeMain>()==null) return;
        NodeMain nm = col.GetComponent<NodeMain>();
        if (nm.state == NodeMain.State.mother) return;
        
        for (int i = 0; i < nm.fishNodes.Count; i++) {
            nm.fishNodes[i].state = NodeFish.State.mother;
        }
        if (nm.isPlayerOne)
        {
            pOneFish += nm.fishNodes.Count;
            GetComponent<AudioSource>().Play();
        //    nm.fishNodes.Clear();
        }
        else {
            pTwoFish += nm.fishNodes.Count;
            GetComponent<AudioSource>().Play();
        }

        if ((pOneFish + pTwoFish) >= amountFishToWin) {
            if (pOneFish > pTwoFish)
            {
                Debug.Log("Player One Wins");
            }
            else {
                Debug.Log("Player Two Wins");
            }
        }
        capturedNodeMain.Add(nm);
        nm.transform.SetParent(swirl.trackme);
        nm.trackTarget = swirl.trackme;
        nm.state = NodeMain.State.mother;
    }
}
