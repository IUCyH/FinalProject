using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerData enemyData;
    
    public void SetEnemyData(PlayerData data)
    {
        enemyData = data;
    }
}
