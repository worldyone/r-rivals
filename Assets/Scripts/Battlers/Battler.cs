using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;
    public bool IsSubmitted { get; private set; }
    public bool IsFirstSubmit { get; set; }
    public bool IsAddNumber { get; set; }
    public int AddNumber { get; private set; }
    public UnityAction OnSubmitAction;
    public int Life { get; set; }

    public BattlerHand Hand { get => hand; set => hand = value; }
    public Card SubmitCard { get => submitPosition.SubmitCard; }

    public void SetCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    void SelectedCard(Card card)
    {
        if (IsSubmitted) return;

        // すでにセットしていれば、手札にもどす
        if (submitPosition.SubmitCard)
        {
            hand.Add(submitPosition.SubmitCard);
        }
        hand.Remove(card);
        submitPosition.Set(card);
        hand.ResetPositions();
    }

    public void OnSubmitButton()
    {
        if (submitPosition.SubmitCard)
        {
            // カードの決定
            IsSubmitted = true;

            // GameMasterに通知
            OnSubmitAction?.Invoke();
        }
    }

    public void RandomSubmit()
    {
        // 手札からランダムにカードを出す
        Card randomCard = hand.RandomRemove();
        submitPosition.Set(randomCard);
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
        hand.ResetPositions();
    }

    public void SetupNextTurn()
    {
        submitPosition.DeleteCard();
        IsSubmitted = false;
    }
}
