using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GunScript : MonoBehaviour
{
    public GameObject decalPrefab;
    public AudioSource fireSound;

    GameObject[] totalDecals;
    int actual_decals = 0;

    void Start()
    {
        totalDecals = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                if (totalDecals[actual_decals]??false)
                {
                    Destroy(totalDecals[actual_decals]);
                }
                totalDecals[actual_decals] = GameObject.Instantiate(
                    decalPrefab, hit.point + hit.normal * 0.01f, 
                    Quaternion.FromToRotation(Vector3.forward, -hit.normal));

                fireSound.Play();

                actual_decals = (actual_decals + 1) % 10;
            }
        }
    }
}
