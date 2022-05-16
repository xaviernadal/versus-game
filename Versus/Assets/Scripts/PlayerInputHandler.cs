using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private Mover mover;
    private Shooter shooter;
    private PlayerControls controls;
    private InputActionAsset inputAsset;
    private InputActionMap shootingGame;
    private PlayerInput playerInput;
    private bool alreadyShot = false;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        shootingGame = inputAsset.FindActionMap("Shooting");
        mover = GetComponent<Mover>();
        controls = new PlayerControls();
        shooter = GetComponent<Shooter>();

    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
    }   

    private void Update(){

    }   
    private void OnEnable(){
        shootingGame.FindAction("Shoot").started += DoShoot;
        shootingGame.FindAction("PlayAgain").started += DoPlayAgain;
        shootingGame.Enable();
    }
    private void OnDisable(){
        shootingGame.FindAction("Shoot").started -= DoShoot;
        shootingGame.FindAction("PlayAgain").started -= DoPlayAgain;
        shootingGame.Disable();
    }

    public void OnMove(CallbackContext context)
    {
        if(mover != null)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }

    public void DoShoot(CallbackContext context){
        if(!alreadyShot)
            if(shooter != null){
                shooter.SetShot(context.control.device.ToString());
                alreadyShot = true;
            }
    }
    public void DoPlayAgain(CallbackContext context){
        SceneManager.LoadScene("ShootingGame");
    }
}