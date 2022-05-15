using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    private Animator animator;
    private Shooter[] players;
    private string deviceUsed;
    private string keyboardLeft = "Keyboard:/Left";
    private float startTime;
    private ShowEarlyMessage showMessage;
    private PlayerConfiguration[] playerConfigs;
    // Start is called before the first frame update
    private void Awake() {
        showMessage = FindObjectOfType<ShowEarlyMessage>();
        animator = this.GetComponent<Animator>();
        players = FindObjectsOfType<Shooter>();
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        Debug.Log("Aqui" + showMessage);
    }
    void Start(){
       startTime = Time.time;
    }

    public void SetShot(string device){
        if (players.Length==2){
            if(checkTimer(device)){
                if(playerConfigs[0].Input.devices[0].ToString() == keyboardLeft){
                    if (device == keyboardLeft) {
                        LeftShoots();
                    } else {
                        RightShoots();
                    }
                } else {
                    if (device == keyboardLeft) {
                        LeftShoots();
                    } else {
                        RightShoots();
                    }
                }
            }
        }
    }

    private bool checkTimer(string device){
        if((Time.time - startTime) > 8f) {
            return true;
        } 
        if(playerConfigs[0].Input.devices[0].ToString() == keyboardLeft){
            if (device == keyboardLeft) {
                players[1].animator.SetTrigger("Death");
            } else {
                players[0].animator.SetTrigger("Death");
            }
        } else {
            if (device == keyboardLeft) {
                players[1].animator.SetTrigger("Death");
            } else {
                players[0].animator.SetTrigger("Death");
            }
        }
        if(showMessage != null)
            showMessage.SetShotBeforeTime();

        return false;
    }
    private void LeftShoots(){
        players[1].animator.SetTrigger("Shoot");
        players[0].animator.SetTrigger("Death");
    }
    private void RightShoots(){
        players[0].animator.SetTrigger("Shoot");
        players[1].animator.SetTrigger("Death");
    }
}
