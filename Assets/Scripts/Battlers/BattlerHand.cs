using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerHand : MonoBehaviour
{
    List<Card> list = new List<Card>();

    // listに追加して自分の子要素にする
    public void Add(Card card)
    {
        list.Add(card);
        card.transform.SetParent(this.transform);
    }

    public void Remove(Card card)
    {
        list.Remove(card);
    }

    public void ResetPositions()
    {
        for (int i = 0; i < list.Count; i++)
        {
            float posX = (i - list.Count / 2f) * 1.4f;
            list[i].transform.localPosition = new Vector3(posX, 0);
        }
    }
}
