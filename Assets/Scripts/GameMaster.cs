using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameObject submitButton;
    RuleBook ruleBook;

    void Awake()
    {
        ruleBook = GetComponent<RuleBook>();
    }

    void Start()
    {
        Setup();
    }

    // カードを生成して配る
    void Setup()
    {
        player.OnSubmitAction = SubmittedAction;
        enemy.OnSubmitAction = SubmittedAction;
        SendCardsTo(player);
        SendCardsTo(enemy);
    }

    void SubmittedAction()
    {
        if (player.IsSubmitted && enemy.IsSubmitted)
        {
            // Cardの勝利判定
            StartCoroutine(CardsBattle());

            submitButton.SetActive(false);
        }
        else if (player.IsSubmitted)
        {
            // playerだけがカードを出している

            submitButton.SetActive(false);
            // enemyからカードを出す
            enemy.RandomSubmit();
        }
        else if (enemy.IsSubmitted)
        {
            // playerの提出を待つ
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

    IEnumerator CardsBattle()
    {
        yield return new WaitForSeconds(1f);
        Result result = ruleBook.GetResult(player, enemy);
        Debug.Log((result));

        yield return new WaitForSeconds(1f);
        SetupNextTurn();
    }

    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        submitButton.SetActive(true);
    }
}
