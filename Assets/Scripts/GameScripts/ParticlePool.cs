using UnityEngine;
using System.Collections;

public class ParticlePool : MonoBehaviour {
    [SerializeField]
    private ParticleSystem system;
    private bool playing = false;
    public bool isPlaying { get { return playing; } }

    public void init()
    {
        gameObject.SetActive(false);
        playing = false;
    }

    public void Spawn(Transform location)
    {
        gameObject.SetActive(true);
        transform.position = location.position;
        transform.rotation = location.rotation;
        playing = true;
        Invoke("Reset", 3f);
    }

    private void Reset()
    {
        playing = false;
        transform.position = new Vector3();
        transform.rotation = new Quaternion();
        gameObject.SetActive(false);
    }


}
