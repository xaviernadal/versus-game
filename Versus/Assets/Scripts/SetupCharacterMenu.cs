using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System.Threading;
using TMPro;
using UnityEngine.UI;

public class SetupCharacterMenu : MonoBehaviour
{ 
    private InputActionAsset inputAsset;
    private InputActionMap selectionMap;
    private InputAction selectionAction;

    [SerializeField]
    private Camera myCamera;
    private Animator animations;
    
    [SerializeField]
    private GameObject maleA;
    [SerializeField]
    private GameObject femaleA;
    [SerializeField]
    private GameObject maleB;
    [SerializeField]
    private GameObject police;
    [SerializeField]
    private GameObject zombie;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button previousButton;
    [SerializeField]
    private GameObject selectedText;
    [SerializeField]
    private GameObject selectedText2;
    [SerializeField]
    private GameObject selectionArrows;


    public int characterIndex = 0;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    Vector3[] cameraPositions = {new Vector3(2.3f, 2, 0.5f), new Vector3(0, 2.5f, 0), new Vector3(0, 2.5f, 0), new Vector3(0.5f, 2.5f, 1), new Vector3(2.7f, 2.5f, 3)};
    Quaternion[] cameraRotations = {new Quaternion(0,0.559193134f,0,-0.829037488f), new Quaternion(0.0348994918f,0,0,0.999390841f),
    new Quaternion(0.0312327538f,0.445925921f,-0.0155720739f,0.894389212f), new Quaternion(0.0042221304f,0.970147908f,-0.0169340353f,0.241885111f), new Quaternion(-0.00582574494f,0.942497969f,-0.0164514035f,-0.333755881f)};
    
    private int playerIndex = 0;
    public void setPlayerIndex(int pi)
    {
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    private void Awake(){
        myCamera = Camera.main;
        inputAsset = this.GetComponent<PlayerInput>().actions;
        selectionMap = inputAsset.FindActionMap("Selection");
        animations = myCamera.GetComponent<Animator>();
    }
    private void Start(){
        selectedText.SetActive(false);
        selectedText2.SetActive(false);

        //myCamera.transform.position = new Vector3(2.29999995f,2,0.5f);
        //myCamera.transform.rotation = new Quaternion(0,0.559193134f,0,-0.829037488f);
    }
    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    private void OnEnable(){
        selectionMap.FindAction("Next").started += Next;
        selectionMap.FindAction("Previous").started += Previous;
        selectionMap.FindAction("Select").started += Select;
        selectionMap.Enable();
    }
    private void OnDisable(){
        selectionMap.FindAction("Next").started -= Next;
        selectionMap.FindAction("Previous").started -= Previous;
        selectionMap.FindAction("Select").started -= Select;
        selectionMap.Disable();
    }

    public void Next(CallbackContext context){
        if(!inputEnabled) { return; }
        fixPosition();
        characterIndex +=1;
        characterIndex = checkIndex(characterIndex);
        nextButton.Select();
        animations.SetTrigger("Next");
    }

    public void Previous(CallbackContext context){
        if(!inputEnabled) { return; }
        fixPosition();
        characterIndex -=1;
        characterIndex = checkIndex(characterIndex);
        previousButton.Select();
        animations.SetTrigger("Previous");
    }
    public int checkIndex(int characterIndex){
        if(characterIndex > 4) {
            return 0;
        }
        if (characterIndex < 0) {
            return 4;
        }
        Debug.Log(characterIndex);
        return characterIndex;
    }
    public void fixPosition(){
        myCamera.transform.position = cameraPositions[characterIndex];
        myCamera.transform.rotation = cameraRotations[characterIndex];
    }


    public void Select(CallbackContext context){
        if(!inputEnabled) { return; }
        GameObject prefabToSave = maleA;
        switch(characterIndex){
            case 0: 
                prefabToSave = this.maleA;
                break;
            case 1:
                prefabToSave = this.femaleA;
                break;
            case 2:
                prefabToSave = this.maleB;
                break;
            case 3:
                prefabToSave = this.zombie;
                break;
            case 4:
                prefabToSave = this.police;
                break;
        }
        
        PlayerConfigurationManager.Instance.SetPlayerColor(playerIndex, prefabToSave);
        selectedText.SetActive(true);
        if (playerIndex == 1){
            selectedText2.SetActive(true);
        }
        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        playerIndex +=1;
    }

}
