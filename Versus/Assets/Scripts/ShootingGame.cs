using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ShootingGame : MonoBehaviour
{
    //input fields
    private InputActionAsset inputAsset;
    private InputActionMap shootingGame;
    private InputAction move;
    private InputActionMap playerMovement;
    private Animator animator;

    private void Awake(){
        
        inputAsset = this.GetComponent<PlayerInput>().actions;
        shootingGame = inputAsset.FindActionMap("Shooting");
        animator = this.GetComponent<Animator>();
    }

    private void OnEnable(){
        shootingGame.FindAction("Shoot").started += DoShoot;
        shootingGame.Enable();
    }
    private void OnDisable(){
        shootingGame.FindAction("Shoot").started -= DoShoot;
        shootingGame.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DoShoot(InputAction.CallbackContext obj){
        animator.SetTrigger("Shoot");
        checkTimer();

    }
    private void checkTimer(){
        Debug.Log("HOLA");
    }

    
}
