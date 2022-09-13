using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;

    void Start()
    {
        Setup();
    }

    // カードを生成して配る
    void Setup()
    {
        for (int i = 0; i <= 7; i++)
        {
            Card card = cardGenerator.Spawn(i);
            player.Hand.Add(card);
        }

        player.Hand.ResetPositions();
    }
}
