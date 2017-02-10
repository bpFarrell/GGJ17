using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

    public int amountMainNodes = 10;
    public int amountFishToWin = 1000;
    public int amountFishToSpawn = 1500;
    public int amountFishDelta = 0;

    public float materialTransitionSpeed = 0.5f;
    public float distToInnerBound = 20;
    public float distToOuterBound = 40;

    public int pOneFish;
    public int pTwoFish;
    public float fishRation {
        get {
            return ((float)pOneFish) / ((float)totalFishScored);
        }
    }
    public int totalFishScored {
        get { return pOneFish + pTwoFish; }
    }
    public static int victor=0;
    Swirl swirl;

    CapsuleCollider capCollide;

    public static GameInfo main;

    IList<NodeMain> capturedNodeMain = new List<NodeMain>();
    void Awake() {
        main = this;
        Shader.SetGlobalColor("_G_Glow", Color.white);
    }
    void Start() {
        capCollide = GetComponent<CapsuleCollider>();
        swirl = GetComponent<Swirl>();
    }
    void Update() {
        if (totalFishScored >= amountFishToWin) {
            //GameStateManager._MAIN.ChangeState(GameStateManager.STATE.END);
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
            AudioSource audio = GetComponent<AudioSource>();
            if(audio!= null) {
                audio.Play();
            }
        }

        if ((pOneFish + pTwoFish) >= amountFishToWin) {
            if (pOneFish > pTwoFish)
            {
                Debug.Log("Player One Wins");
                victor = 1;
            }
            else {
                Debug.Log("Player Two Wins");
                victor = 1;
            }
			GameStateMachine._Main.ChangeState(GameStateMachine.STATE.END);
        }
        capturedNodeMain.Add(nm);
        nm.transform.SetParent(swirl.trackme);
        nm.trackTarget = swirl.trackme;
        nm.state = NodeMain.State.mother;
    }
}
