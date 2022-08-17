using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    [SerializeField]
    int tutorial;
    public Text tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        tutorial = PlayerPrefs.GetInt("tutorial");
        if (tutorial == 0)
        {
            StartCoroutine("movement_tutorial");
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    IEnumerator movement_tutorial() 
    {
        tutorialText.text = "Use WASD keys to move";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "Use MOUSE to look and navigate";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "Press SPACE KEY to jump";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "Press SHIT KEY to run";
        yield return new WaitForSeconds(5f);
        tutorialText.text = "Press LEFT MOUSE BUTTON to interract";
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("tutorial",1);
        
    }
}
