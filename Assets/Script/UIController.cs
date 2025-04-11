using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject JoinPanel;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject LobbyPanel;
    [SerializeField] private GameObject ExitPanel;

    [SerializeField] private TMP_Text StartText;

    private string startText = "Starting in {0} seconds...";
    [SerializeField] int countdownTime = 10;

    void OnEnable()
    {
        EventManager.OnPlayerJoined += OnPlayerJoined;
        EventManager.OnPlayerLeft += OnPlayerLeft;
        EventManager.OnGameReadyToStart += OnGameReadyToStart;
        EventManager.OnGameStarted += OnGameStarted;
        EventManager.OnGameEnded += OnGameEnded;

        //Start Panel
        JoinPanel.SetActive(true);
        StartPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        ExitPanel.SetActive(false);

    }
    void OnDisable()
    {
         EventManager.OnPlayerJoined -= OnPlayerJoined;
        EventManager.OnPlayerLeft -= OnPlayerLeft;
        EventManager.OnGameReadyToStart -= OnGameReadyToStart;
        EventManager.OnGameStarted -= OnGameStarted;
        EventManager.OnGameEnded -= OnGameEnded;
    }

    // Update is called once per frame
    void OnPlayerJoined()
    {
        JoinPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        StartPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }
    void OnPlayerLeft()
    {
        // JoinPanel.SetActive(false);
        // LobbyPanel.SetActive(true);
        // StartPanel.SetActive(false);
        // ExitPanel.SetActive(false);
    }

    void OnGameReadyToStart()
    {
        JoinPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        startText = string.Format(startText, countdownTime);
        StartText.text = startText;
        StartPanel.SetActive(true);
        ExitPanel.SetActive(false);
    }
    void OnGameStarted()
    {
        JoinPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        StartPanel.SetActive(false);
        ExitPanel.SetActive(true);
    }
    IEnumerator CountdownToStart()
    {
        for (int i = countdownTime; i > 0; i--)
        {
            StartText.text = string.Format(startText, i);
            yield return new WaitForSeconds(1f);
        }
        EventManager.InvokeOnGameStarted();
    }

    void OnGameEnded()
    {

    }
}
