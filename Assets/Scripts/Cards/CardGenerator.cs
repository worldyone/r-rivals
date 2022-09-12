using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] Card cardPrefab;

    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            Spawn();
        }
    }

    // Cardの生成
    public void Spawn()
    {
        Instantiate(cardPrefab);
    }
}
