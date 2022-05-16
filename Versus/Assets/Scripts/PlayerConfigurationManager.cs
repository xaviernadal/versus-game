using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField]
    private int MaxPlayers = 2;
    public static PlayerConfigurationManager Instance { get; private set; }
    [SerializeField]
    private GameObject player1Panel;
    [SerializeField]
    private GameObject player2Panel;

    [SerializeField]
    private GameObject info1;
    [SerializeField]
    private GameObject info2;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
        
    }
    private void Start(){
        info1.SetActive(false);
        info2.SetActive(false);
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("player joined " + pi.playerIndex);
        pi.transform.SetParent(transform);
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
            HidePlayerPanel(pi);
        }
        Debug.Log(pi.devices[0]);
        ShowInformationMessage(pi);
        
    }
    public void HidePlayerPanel(PlayerInput pi){
        if(pi.devices[0].ToString() == "Keyboard:/Left"){
            FindObjectOfType<AudioManager>().Play("Join");
            player1Panel.SetActive(false);
        }
        if(pi.devices[0].ToString() == "Keyboard:/Right"){
        FindObjectOfType<AudioManager>().Play("Join");
            player2Panel.SetActive(false);
        }
    }
    public void ShowInformationMessage(PlayerInput pi){
        if(pi.playerIndex == 0){
            if(pi.devices[0].ToString() == "Keyboard:/Left"){
                info1.SetActive(true);
            }
            if(pi.devices[0].ToString() == "Keyboard:/Right"){
                info2.SetActive(true);
            }
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerColor(int index, int character)
    {
        playerConfigs[index].playerMaterial = character;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if (playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("ShootingGame");
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
        pointsCount = 0;
    }

    public PlayerInput Input { get; private set; }
    public int PlayerIndex { get; private set; }
    public bool isReady { get; set; }
    public int playerMaterial {get; set;}
    public int pointsCount {get; set;}
}
