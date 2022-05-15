using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI countdown;
    [SerializeField]
    private GameObject shootMessage;
    // Start is called before the first frame update
    void Start()
    {
        shootMessage.SetActive(false);
        StartCoroutine(CountdownTimer());
    }


    IEnumerator CountdownTimer(){
        yield return new WaitForSeconds(5);
        countdown.SetText("3");
        yield return new WaitForSeconds(1);
        countdown.SetText("2");
        yield return new WaitForSeconds(1);
        countdown.SetText("1");
        yield return new WaitForSeconds(1);
        countdown.SetText("");
        shootMessage.SetActive(true);
    }
}
