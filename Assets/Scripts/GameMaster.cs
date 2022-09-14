using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameObject submitButton;

    void Start()
    {
        Setup();
    }

    // カードを生成して配る
    void Setup()
    {
        player.OnSubmitAction = SubmittedAction;
        SendCardsTo(player);
        SendCardsTo(enemy);
    }

    void SubmittedAction()
    {
        Debug.Log("BattlerからMasterに通知を受け取った");

        if (player.IsSubmitted)
        {
            submitButton.SetActive(false);
        }
    }

    void SendCardsTo(Battler battler)
    {
        for (int i = 0; i <= 7; i++)
        {
            Card card = cardGenerator.Spawn(i);
            battler.SetCardToHand(card);
        }
        battler.Hand.ResetPositions();
    }
}
