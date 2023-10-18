using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class TestPlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField]
    GameObject playerPrefab;
    
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("Call Back");
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
        }
    }
}
