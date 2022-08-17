using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class WallScript : MonoBehaviour
{
    [SerializeField]
    public GameObject visual;
    [SerializeField]
    float px, py, pz;
    [SerializeField]
    GameObject spawnPosition;

    private void Start()
    {
        spawnPosition = GameObject.FindGameObjectWithTag("objectposition");

    }

    


  

   
}
