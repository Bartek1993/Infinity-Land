using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float distance;
    [SerializeField]
    int damage, stone_health, stone_count, randomvoiceclip;
    [SerializeField]
    int is_destroyed;
    [SerializeField]
    string stoneename;
    // Start is called before the first frame update
    void Start()
    {
       
        source = GetComponent<AudioSource>();
        stoneename = gameObject.transform.name + gameObject.transform.position.x + gameObject.transform.position.y;
        
        stone_health = Random.Range(20, 25);
        damage = Random.Range(2, 4);
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        is_destroyed = PlayerPrefs.GetInt(stoneename);
        if (is_destroyed == 1)
        {
            Destroy(gameObject,1);
            
        }
        else 
        {
        
        }
        randomvoiceclip = Random.Range(0,1005); 
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
       
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if (distance < 10)
        {
            takeDamage(damage);
        }

        if (stone_health <= 0)
        {
            GameObject ob = gameObject;
            is_destroyed = 1;
            Destroy(gameObject,1);



        }

    }

    private void OnMouseOver()
    {
        if (distance < 10 && randomvoiceclip == 102 && !source.isPlaying)
        {
            source.pitch = 0.95f;
            source.Play();
        }
    }

    private void takeDamage(int Damage)
    {
        stone_health -= damage;
        stone_count = PlayerPrefs.GetInt("stone_count");
        int add_wood = stone_count + 1;
        PlayerPrefs.SetInt("stone_count", add_wood);
    }

    private void OnDestroy()
    {
        is_destroyed = 1;
        //PlayerPrefs.SetInt(stoneename, is_destroyed);
    }
}
