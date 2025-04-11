using UnityEngine;
using System.Collections.Generic;
using Fusion;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;


public class StartGameMessage
{
    public string Command;

    public StartGameMessage(string Message)
    {
        Command = Message;
    }
}

public class NetworkManager : MonoBehaviour
{
    public NetworkRunner runner;
    public NetworkPrefabRef playerPrefab;
    [SerializeField] private TMP_InputField RoomNameInputField;
    [SerializeField] private GameObject RPMUICanvas;
    public int minPlayers = 4;

    public List<NetworkObject> players = new List<NetworkObject>();
    public bool gameStarted = false;
    public bool isAvatarloaded = false;

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_StartGame()
    {
        Debug.Log("Game is starting on all clients!");
        gameStarted = true;
    }

    void OnEnable()
    {
        RPMUICanvas.SetActive(false);  
    }

    public void OnJoinRoomButtonClick()
    {
        if(string.IsNullOrEmpty(RoomNameInputField.text))
        {
            Debug.Log("Please enter a room name.");
            return;
        }
        if (runner.IsRunning)
        {
            Debug.Log("Already in a room.");
            return;
        }
        CreateorJoinRoom(RoomNameInputField.text);
        Debug.Log("Joining room: " + RoomNameInputField.text);
    }

    public async void CreateorJoinRoom(string roomName)
    {
        var sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();
        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = roomName,
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
            SceneManager = sceneManager
        });

        if (!result.Ok)
        {
            Debug.LogError("Failed to start: " + result.ShutdownReason);
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player Joined: " + player.PlayerId);
        if (runner.IsServer)
        {
            runner.Spawn(playerPrefab, new Vector3(player.PlayerId * 2, 1, 0), Quaternion.identity, player);
            EventManager.InvokeOnPlayerJoined();
        }

        if (runner.ActivePlayers.ToList().Count() == minPlayers)
        {
            Debug.Log("All players joined! Starting game...");
            EventManager.InvokeOnGameReadyToStart();
            StartCoroutine(StartGameCountdown());
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { 
        var playerObject = runner.GetPlayerObject(player);
        if (playerObject != null)
        {
            runner.Despawn(playerObject);
            Debug.Log("Player object despawned for Player ID: " + player.PlayerId);
        }
    }

    // public void OnAvatarLoaded(GameObject Avatar)
    // {
    //     Debug.Log("Avatar loaded: " + Avatar.name);
    //     var Go = runner.GetPlayerObject(runner.LocalPlayer);
    //     Avatar.transform.SetParent(Go.transform);
    //     Avatar.transform.localPosition = Vector3.zero;
    //     isAvatarloaded = true;
    //     OnPlayerJoined(runner, runner.LocalPlayer);
    // }

    IEnumerator StartGameCountdown()
    {
        yield return new WaitForSeconds(10f);
        gameStarted = true;
        RPMUICanvas.SetActive(true);
        // var serverPlayer = runner.SessionInfo.GetPlayerByIndex(0);
        // runner.SendUserMessage(runner.serverPlayer, new StartGameMessage("Start Game"));
    }
}
