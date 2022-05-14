using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DuelUI : MonoBehaviour
{
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private TextMeshProUGUI countdown;
    [SerializeField]
    private TextMeshProUGUI[] playerControls;
    // Start is called before the first frame update
    private PlayerConfiguration[] playerConfigs;
    private float showTitleTime = 5f;
    void Start()
    {
        showTitleTime = Time.time + showTitleTime;
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        showControlTexts(playerConfigs);
    }

    private void showControlTexts(PlayerConfiguration[] playerConfigs){
        if (playerConfigs[0].Input.devices[0].ToString() == "Keyboard:/Left"){
            playerControls[0].SetText("Spacebar");
            playerControls[1].SetText("Enter");
        } else {
            playerControls[1].SetText("Spacebar");
            playerControls[0].SetText("Enter");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > showTitleTime) {
            title.SetActive(false);
        }
    }
}
