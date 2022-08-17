using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVolume : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float volume, distance;
    [SerializeField]
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 120)
        {
            source.volume += 0.0005f;
        }
        else 
        {
            source.volume -= 0.0005f;
        }

        if (source.volume < .05) 
        {
            source.volume = .05f;
        }

        if (source.volume > .5)
        {
            source.volume = .5f;
        }

    }
}
