using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinalPanelHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerModelsLeft;
    [SerializeField]
    private GameObject[] playerModelsRight;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject[] winnerMessages;
    [SerializeField]
    private GameObject playAgain;
    [SerializeField]
    private TextMeshProUGUI[] counters;

    private PlayerConfiguration[] playerConfigs;
    // Start is called before the first frame update
    void Start()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
    }

    public void SetCompletePanel(int indexMessage){
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play("EndingScreen");
        SetModels();
        SetPanel();
        SetWinnerMessage(indexMessage);
        SetPlayAgainMessage();
        SetPointsCount();
    }

    private void SetModels(){
        playerModelsLeft[playerConfigs[1].playerMaterial].SetActive(true);
        playerModelsRight[playerConfigs[0].playerMaterial].SetActive(true);
    }
    private void SetPanel(){
        panel.SetActive(true);
    }
    private void SetWinnerMessage(int indexMessage){
        winnerMessages[indexMessage].SetActive(true);
        playerConfigs[indexMessage].pointsCount += 1;
    }
    private void SetPlayAgainMessage(){
        playAgain.SetActive(true);
    }
    private void SetPointsCount() {
        counters[0].SetText(playerConfigs[0].pointsCount.ToString());
        counters[1].SetText(playerConfigs[1].pointsCount.ToString());
    }
}
