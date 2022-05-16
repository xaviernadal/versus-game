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
    private FinalPanelHandler finalPanel;
    // Start is called before the first frame update
    private void Awake() {
        finalPanel = FindObjectOfType<FinalPanelHandler>();
        showMessage = FindObjectOfType<ShowEarlyMessage>();
        animator = this.GetComponent<Animator>();
        players = FindObjectsOfType<Shooter>();
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
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
                SetFinalPanel(1);
            } else {
                players[0].animator.SetTrigger("Death");
                SetFinalPanel(0);
            }
        } else {
            if (device == keyboardLeft) {
                players[1].animator.SetTrigger("Death");
                SetFinalPanel(1);
            } else {
                players[0].animator.SetTrigger("Death");
                SetFinalPanel(0);
            }
        }
        if(showMessage != null)
            showMessage.SetShotBeforeTime();
            
        return false;
    }
    private void LeftShoots(){
        players[1].animator.SetTrigger("Shoot");
        players[0].animator.SetTrigger("Death");
        SetFinalPanel(0);
    }
    private void RightShoots(){
        players[0].animator.SetTrigger("Shoot");
        players[1].animator.SetTrigger("Death");
        SetFinalPanel(1);
    }

    private void SetFinalPanel(int index){
        StartCoroutine(WaitForPanel(index));
       
    }
    IEnumerator WaitForPanel(int index){
        yield return new WaitForSeconds(2);
         if(finalPanel != null)
            finalPanel.SetCompletePanel(index);
    }
}
