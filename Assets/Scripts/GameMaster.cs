using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameUI gameUI;
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
        gameUI.Init();
        player.Life = 4;
        enemy.Life = 4;
        gameUI.ShowLives(player.Life, enemy.Life);
        gameUI.UpdateAddNumber(false, false);
        player.OnSubmitAction = SubmittedAction;
        enemy.OnSubmitAction = SubmittedAction;
        SendCardsTo(player, isEnemy: false);
        SendCardsTo(enemy, isEnemy: true);
    }

    void SubmittedAction()
    {
        if (player.IsSubmitted && enemy.IsSubmitted)
        {
            // Cardの勝利判定
            StartCoroutine(CardsBattle());
        }
        else if (player.IsSubmitted)
        {
            // playerだけがカードを出している
            // enemyからカードを出す
            enemy.RandomSubmit();
        }
        else if (enemy.IsSubmitted)
        {
            // playerの提出を待つ
        }
    }

    void SendCardsTo(Battler battler, bool isEnemy)
    {
        for (int i = 0; i <= 7; i++)
        {
            Card card = cardGenerator.Spawn(i, isEnemy);
            battler.SetCardToHand(card);
        }
        battler.Hand.ResetPositions();
    }

    IEnumerator CardsBattle()
    {
        yield return new WaitForSeconds(1f);
        enemy.SubmitCard.Open();
        yield return new WaitForSeconds(0.7f);
        Result result = ruleBook.GetResult(player, enemy);
        switch (result)
        {
            case Result.TurnWin:
                gameUI.ShowTurnResult("WIN");
                enemy.Life--;
                break;
            case Result.TurnWin2:
                gameUI.ShowTurnResult("WIN");
                enemy.Life -= 2;
                break;
            case Result.TurnLose:
                gameUI.ShowTurnResult("LOSE");
                player.Life--;
                break;
            case Result.TurnLose2:
                gameUI.ShowTurnResult("LOSE");
                player.Life -= 2;
                break;
            case Result.TurnDraw:
                gameUI.ShowTurnResult("DRAW");
                break;
        }

        gameUI.ShowLives(player.Life, enemy.Life);
        yield return new WaitForSeconds(1f);

        if (player.Hand.IsEmpty && enemy.Hand.IsEmpty)
        {
            ShowResult(Result.GameDraw);
        }
        if (player.Life <= 0 || enemy.Life <= 0
            || result == Result.GameWin || result == Result.GameLose)
        {
            ShowResult(result);
        }
        else
        {
            SetupNextTurn();
        }
    }

    void ShowResult(Result result)
    {
        if (result == Result.GameWin)
        {
            gameUI.ShowGameResult("WIN");
        }
        else if (result == Result.GameLose)
        {
            gameUI.ShowGameResult("LOSE");
        }
        else if (player.Life <= 0 && enemy.Life <= 0)
        {
            // cant reach
            gameUI.ShowGameResult("DRAW");
        }
        else if (player.Life <= 0)
        {
            gameUI.ShowGameResult("LOSE");
        }
        else if (enemy.Life <= 0)
        {
            gameUI.ShowGameResult("WIN");
        }
        else
        {
            gameUI.ShowGameResult("DRAW");
        }
    }

    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        gameUI.SetupNextTurn();
        gameUI.UpdateAddNumber(player.IsAddNumber, enemy.IsAddNumber);

        if (enemy.IsFirstSubmit)
        {
            enemy.RandomSubmit();
            enemy.IsFirstSubmit = false;
            enemy.SubmitCard.Open();
        }
        if (player.IsFirstSubmit)
        {
            // TODO:
            // playerが先に表で出す（パネル表示）
        }
    }

    public void OnRetryButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void OnTitleButton()
    {
        SceneManager.LoadScene("Title");
    }
}
