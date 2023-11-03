using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMESTATE
{
    Battle,
    Win,
    Lose,
    Pause,
}
enum TRUNSTATE
{
    Start,
    Processing,
    End
}

public class PGC_BattleJudgement : Singleton<PGC_BattleJudgement>
{
    PlayerData enemyData;
    
    public void SetEnemyData(PlayerData data)
    {
        enemyData = data;
    }

    

}
