using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowEarlyMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI earlyMessage;

    void Start(){
        earlyMessage.SetText("");
    }

    public void SetShotBeforeTime(){
        earlyMessage.SetText("Too soon! You lose!");
        StartCoroutine(Delay());
    }
    IEnumerator Delay(){
        yield return new WaitForSeconds(3);
        earlyMessage.SetText("");
    }
}
