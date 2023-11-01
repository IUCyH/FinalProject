using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingManager : Singleton<MatchingManager>
{
    readonly List<PlayerData> playersCanMatch = new List<PlayerData>();
    
    public void Match()
    {
        playersCanMatch.Clear();
        var players = DataManager.Instance.Players;
        var user = DataManager.Instance.PlayerData;
        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            bool isCanMatch = player.level - user.level < 3 && !player.isInPlay;

            if (isCanMatch)
            {
                playersCanMatch.Add(player);
            }
        }

        var randomIndex = Random.Range(0, playersCanMatch.Count);
        PGC_BattleManager.Instance.SetEnemyData(playersCanMatch[randomIndex]);
    }
}
