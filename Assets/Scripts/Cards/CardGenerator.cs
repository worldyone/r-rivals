using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] CardBase[] cardBases;
    [SerializeField] Card cardPrefab;

    void Start()
    {
    }

    // Cardの生成
    public Card Spawn(int number, bool isEnemy)
    {
        Card card = Instantiate(cardPrefab);
        card.Set(cardBases[number], isEnemy);
        return card;
    }
}
