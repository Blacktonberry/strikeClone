using UnityEngine;
using System.Collections;

public class CamFolow : MonoBehaviour {
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private Vector3 Distance;

	// Update is called once per frame
	void Update () {
        transform.position = Player.position+Distance;
	}
}
