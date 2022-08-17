using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float distance;
    [SerializeField]
    int damage, tree_health, wood_count;
    [SerializeField]
    int is_destroyed;
    [SerializeField]
    string treename;
    // Start is called before the first frame update
    void Start()
    {
        is_destroyed = PlayerPrefs.GetInt(treename);
        if (is_destroyed == 1)
        {
            Destroy(gameObject);
        }

        treename = gameObject.transform.name;
        tree_health = Random.Range(15, 20);
        damage = Random.Range(2, 4);
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if (distance < 8)
        {
            takeDamage(damage);
        }

        if (tree_health <= 0) 
        {
            is_destroyed = 1;
            PlayerPrefs.SetInt(treename, is_destroyed);
            Destroy(gameObject);

            
           
        }
        
    }

    private void takeDamage(int Damage) 
    {
        tree_health -= damage;
        wood_count = PlayerPrefs.GetInt("wood_count");
        int add_wood = wood_count + 1;
        PlayerPrefs.SetInt("wood_count",add_wood);
    }

}
