using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaController : MonoBehaviour {
    public float radius = 512;
    public bool spawn;
    float lastSpawn = -20;
    GameObject orcaPrefab;
	void Start () {
        orcaPrefab = Resources.Load<GameObject>("Orca") as GameObject;
	}
	void Update () {
        if (spawn) {
            spawn = false;
            Spawn();
        }
        if (lastSpawn+20 < Time.time) {
            lastSpawn = Time.time;
            Spawn();
        }
	}
    void Spawn() {
        int count = Mathf.FloorToInt(Random.Range(3, 5));
        Vector3 dir = (Random.insideUnitCircle.normalized)*radius;
        dir.z = dir.y;
        dir.y = 0;
        Vector3 cross = Vector3.Cross(dir.normalized, Vector3.up);
        Vector3 point = dir + transform.position + cross * -900 + transform.up * -20;
        for (int x = 0; x < count; x++) {
            Vector3 offset = Random.onUnitSphere * 70;
            GameObject go = Instantiate(orcaPrefab,
                point+offset,
                Quaternion.LookRotation(cross, Vector3.up));
        }
    }
}
