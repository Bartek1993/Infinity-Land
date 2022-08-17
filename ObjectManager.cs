using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;
using BayatGames.SaveGameFree;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    string path = "Assets/SAVEDASSETS";
    [SerializeField]
    string object_name;
    [SerializeField]
    string object_id;
    [SerializeField]
    List<GameObject> objects;
    [SerializeField]
    GameObject[] loadedObjects;
    [SerializeField] 
    List<GameObject> startList;
    public WallScript [] walls;
    [SerializeField]
    float x, y, z;
    float getX, getY, getZ;
    [SerializeField]
    int wall_counter;

    // Start is called before the first frame update
    void Start()
    {
        ////here I'm trying to instantiate walls
        try
        {
            walls = Resources.LoadAll<WallScript>("");
            //wall_counter = PlayerPrefs.GetInt("wall_counter");
            
            for(int i = 0; i< walls.Length; i++) 
            {
                
                Instantiate(walls[i]);
                Debug.Log("Instantiated " + walls.Length + "walls");  
               
            }
            
            
        }
        catch (Exception a) 
        {
            Debug.Log(a + "No data to load");
        }

        
            
            
        
     

    }
    private void Update()
    {
        walls = Resources.LoadAll<WallScript>("");

        x = GameObject.FindGameObjectWithTag("objectposition").gameObject.transform.position.x;
        y = GameObject.FindGameObjectWithTag("objectposition").gameObject.transform.position.y;
        z = GameObject.FindGameObjectWithTag("objectposition").gameObject.transform.position.z;
        
    }
    // Update is called once per frame

    public void AddToList(GameObject addedGameObject) 
    {
       
        for(int i = walls.Length; i <= walls.Length; i += walls.Length +  1) 
        {
            object_id = i.ToString();
        }
        object_name = addedGameObject.transform.name + object_id;
        addedGameObject.name = object_name;
        addedGameObject.tag = "Wall";
        objects.Add(addedGameObject);
        ///////SAVE INSTANTIATED OBJECT COORDINATES//////////
            SaveGame.Save<float>(addedGameObject.name + "Xcordinated",x);
            SaveGame.Save<float>(addedGameObject.name + "Ycordinated", y);
            SaveGame.Save<float>(addedGameObject.name + "Zcordinated", z);
            string localPath = "Assets/Resources/" + addedGameObject.name +object_id + ".prefab";
            bool prefabSuccess;
            PrefabUtility.SaveAsPrefabAssetAndConnect(addedGameObject, localPath, InteractionMode.UserAction, out prefabSuccess);
            if (prefabSuccess)
            {
                
                Debug.Log("SUCCESS");
            }
            else
            {
                Debug.Log("FAILED TO CREATE PREFAB");
            }
        }
        
    }


    
    

