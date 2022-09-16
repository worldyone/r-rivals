using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text turnResultText;
    [SerializeField] Text playerLifeText;
    [SerializeField] Text enemyLifeText;
    [SerializeField] GameObject resultPanel;
    [SerializeField] Text resultText;
    [SerializeField] GameObject playerAddNumber;
    [SerializeField] GameObject enemyAddNumber;

    public void Init()
    {
        turnResultText.gameObject.SetActive(false);
        resultPanel.SetActive(false);
    }

    public void ShowTurnResult(string result)
    {
        turnResultText.gameObject.SetActive(true);
        turnResultText.text = result;
    }

    public void ShowGameResult(string result)
    {
        resultPanel.SetActive(true);
        resultText.text = result;
    }

    public void SetupNextTurn()
    {
        turnResultText.gameObject.SetActive(false);
    }

    public void ShowLives(int playerLife, int enemyLife)
    {
        playerLifeText.text = $"x{playerLife}";
        enemyLifeText.text = $"x{enemyLife}";
    }

    public void UpdateAddNumber(bool isPlayerAddNumber, bool isEnemyAddNumber)
    {
        playerAddNumber.SetActive(isPlayerAddNumber);
        enemyAddNumber.SetActive(isEnemyAddNumber);
    }
}
