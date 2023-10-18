using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ServerRunner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField]
    GameObject playerPrefab;
    NetworkRunner runner;

    void Awake()
    {
        StartGame();
    }

    async void StartGame()
    {
        runner = gameObject.GetComponent<NetworkRunner>();
        
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "Test",
            Scene = (int)KindOfScene.Title
        });
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
        }
    }
}
