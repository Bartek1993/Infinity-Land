using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using BayatGames.SaveGameFree;


public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    int cameraswitch;
    [SerializeField]
    float r, rUp;
    [SerializeField]
    float x, y;
    [SerializeField]
    float jumpheight;
    [SerializeField]
    bool pause;
    [SerializeField]
    private GameObject characterCamera;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Animator characterAnimator;
    CharacterController characterController;
    [SerializeField]
    private GameObject headRender;
    [SerializeField]
    private GameObject FirstPersonCam,ThirdPersonCam, TPSbuilderCam;
    [SerializeField]
    bool action, jump;
    public GameObject [] object_to_Instantiate;
    [SerializeField]
    Transform instantiatePositionTransform;
    [SerializeField]
    bool buildCam;
    public CinemachineVirtualCamera builderCam;
    [SerializeField]
    int itemcategory,itemnumber;
    
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponentsOnStart();
    }

    // Update is called once per frame
    void Update()
    {
        itemnumber = PlayerPrefs.GetInt("itemnumber");
        itemcategory = PlayerPrefs.GetInt("itemcategory");

        if (Input.GetKeyDown(KeyCode.V)) 
        {
            buildCam = !buildCam;
            
        }

        if (buildCam) 
        {
            builderCam.LookAt = instantiatePositionTransform;
            builderCam.Follow = instantiatePositionTransform;
            BuildMovement();
            instantiatePositionTransform.gameObject.SetActive(true);
            
            changeItemNumber();
        }
        else
        {
            instantiatePositionTransform.gameObject.SetActive(false);
            Movement(); 
            Jump();
            Action();
            PlayerPosition();

        }


        if (Input.GetKeyDown(KeyCode.B) && buildCam)
        {
           
            Build();
        }
      
        



    }

    private void changeItemNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            itemnumber = itemnumber - 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            itemnumber = itemnumber + 1;
        }

        if (itemnumber < 0) 
        {
            itemnumber = 0;
        }

        PlayerPrefs.SetInt("itemnumber",itemnumber);
    }

    private void Build()
    {
        int wall_count = PlayerPrefs.GetInt("wall_count");
        int structure_count = PlayerPrefs.GetInt("structure_count");
        float rotation_x = instantiatePositionTransform.eulerAngles.x;
        float rotation_y = instantiatePositionTransform.eulerAngles.y;
        float rotation_z = instantiatePositionTransform.eulerAngles.z;
        GameObject currentGameObject =  Instantiate(object_to_Instantiate[itemnumber],instantiatePositionTransform.position,Quaternion.Euler(new Vector3(rotation_x,rotation_y,rotation_z)));
        ObjectManager objectManager = GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<ObjectManager>();
        objectManager.AddToList(currentGameObject);
        //
        if (currentGameObject.tag == "Wall") 
        {
            PlayerPrefs.SetInt("wall_count",wall_count + 1);
        }
        if (currentGameObject.tag == "Structure")
        {
            PlayerPrefs.SetInt("structure_count", structure_count + 1);
        }
    }

    private void BuildMovement() 
    {
        float rotation_x = instantiatePositionTransform.eulerAngles.x;
        float rotation_y = instantiatePositionTransform.eulerAngles.y;
        float rotation_z = instantiatePositionTransform.eulerAngles.z;
        GameObject o = Instantiate(object_to_Instantiate[itemnumber], instantiatePositionTransform.position, Quaternion.Euler(new Vector3(rotation_x, rotation_y, rotation_z)));
        Destroy(o,0.15f);
        float rotationX = 0;
        float rotationY = 0;
        float rotationZ = 0;
        float z = Input.GetAxisRaw("Vertical") * 20f;
        float x = Input.GetAxisRaw("Horizontal") / 10f;
        float y = 0;
        if (Input.GetKey(KeyCode.Z))
        {
            rotationX -= 0.05f;
        }
        if (Input.GetKey(KeyCode.X))
        {
            rotationX += 0.05f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            y += 0.05f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            y -= 0.05f;
        }
        if (Input.GetKey(KeyCode.R)) 
        {
            rotationZ = Input.GetAxisRaw("Mouse Y") / 1.5f;
        }
       

        instantiatePositionTransform.Rotate(new Vector3(rotationY, rotationX, rotationZ) * 195f * Time.deltaTime);
        instantiatePositionTransform.Translate(x, y, z * 0.5f * Time.deltaTime);
    }


    private void OnDestroy()
    {
          
    }
    private void PlayerPosition()
    {
        float position_x = transform.position.x;
        float position_y = transform.position.y;
        float position_z = transform.position.z;
        PlayerPrefs.SetFloat("posX",position_x);
        PlayerPrefs.SetFloat("posY", position_y);
        PlayerPrefs.SetFloat("posZ", position_z);

    }

    private void FixedUpdate()
    {
        CameraSwitch();
    }
    private void Action()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            action = true;
           
        }
        else 
        {
            action = false;
        }
        characterAnimator.SetBool("action", action);
    }
   

    void Jump()
    {

        
        jump = Input.GetKeyDown(KeyCode.Space);
           
        //
        if (jump) 
        {
            Vector3 moveUp = transform.up * jumpheight;
            characterController.Move(moveUp * 1f * Time.deltaTime);
        }
            
        characterAnimator.SetBool("jump", jump);
       
       
       

    }

    private void CameraSwitch()
    {
            switch (buildCam)
            {
            case true:
                    ThirdPersonCam.SetActive(false);
                    FirstPersonCam.SetActive(false);
                    headRender.SetActive(true);
                    TPSbuilderCam.SetActive(true);
                    break;
            case false:
                    headRender.SetActive(false);
                    ThirdPersonCam.SetActive(false);
                    FirstPersonCam.SetActive(true);
                    TPSbuilderCam.SetActive(false);
                    break;
            }

      
           
    }

    private void Movement()
    {
        if (!buildCam)
        {
            float posX = PlayerPrefs.GetFloat("posX");
            float posY = PlayerPrefs.GetFloat("posY");
            float posZ = PlayerPrefs.GetFloat("posZ");
            Vector3 startPosition = new Vector3(posX, posY, posZ);
            transform.Translate(startPosition);
            r = Input.GetAxis("Mouse X");
            rUp = Input.GetAxis("Mouse Y");
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            characterAnimator.SetFloat("x", x);
            characterAnimator.SetFloat("y", y);
            Vector3 move = transform.right * x + transform.forward * y;
            characterController.Move(move * 10f * Time.deltaTime);
            transform.Rotate(new Vector3(0, r, 0) * 135f * Time.deltaTime);
            float ClampRotation = Mathf.Clamp(rUp, -45f, 45);
            characterCamera.transform.Rotate(new Vector3(ClampRotation, 0, 0) * 90f * Time.deltaTime);
        }
    }

    private void GetComponentsOnStart() 
    {
       
        instantiatePositionTransform = GameObject.FindGameObjectWithTag("objectposition").GetComponent<Transform>();
        jumpheight = 200;
        pause = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        cameraswitch = 0;
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        characterCamera = GameObject.FindGameObjectWithTag("MainCamera");
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

   
}
