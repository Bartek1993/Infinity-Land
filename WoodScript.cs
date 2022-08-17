using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int wood_count;
    [SerializeField]
    Text wood_text;
    // Update is called once per frame
    void Update()
    {
        wood_count = PlayerPrefs.GetInt("wood_count");
        wood_text.text = wood_count.ToString(); 
    }
}
