using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScript : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    [SerializeField]
    int stone_count;
    [SerializeField]
    Text stone_text;
    // Update is called once per frame
    void Update()
    {
        stone_count = PlayerPrefs.GetInt("stone_count");
        stone_text.text = stone_count.ToString();
    }
}
