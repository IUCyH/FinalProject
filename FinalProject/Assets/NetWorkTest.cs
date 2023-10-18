using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

struct NetworkInputData : INetworkInput
{
    public float horizontal;
    public float vertical;
}

public class NetWorkTest : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField]
    GameObject playerObj;
    NetworkObject networkObject;
    NetworkRunner networkRunner;
    NetworkInputData inputData;

    void Awake()
    {
        StartGame();
    }

    async void StartGame()
    {
        networkRunner = gameObject.AddComponent<NetworkRunner>();
        
        networkRunner.ProvideInput = true;

        await networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "TestSession",
            Scene = (int)KindOfScene.Title
        });
    }
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        networkObject = runner.Spawn(playerObj, Vector3.zero, Quaternion.identity, player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        runner.Despawn(networkObject);
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken){}

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        inputData.horizontal = Input.GetAxis("Horizontal");
        inputData.vertical = Input.GetAxis("Vertical");
        
        input.Set(inputData);
    }
}
