using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEarlyMessage : MonoBehaviour
{
    [SerializeField]
    private GameObject earlyMessage;

    private bool shotBeforeTime = false;
    void Start()
    {
        earlyMessage.SetActive(false);
    }
    public void SetShotBeforeTime(){
        shotBeforeTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(shotBeforeTime){
            earlyMessage.SetActive(true);
        }
    }
}
