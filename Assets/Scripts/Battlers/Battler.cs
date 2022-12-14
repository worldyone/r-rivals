using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;
    [SerializeField] GameObject submitButton;
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
        submitButton?.SetActive(true);
    }

    public void OnSubmitButton()
    {
        if (submitPosition.SubmitCard)
        {
            // カードの決定
            IsSubmitted = true;

            // GameMasterに通知
            OnSubmitAction?.Invoke();
            submitButton?.SetActive(false);
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

    public void ReflectEnemySubmitAction(int number)
    {
        // numberからカードを確定させる
        Card card = hand.CardOfNumber(number);

        // 提出行動を行う(playerAさん画面のenemyBさんの行動処理)
        hand.Remove(card);
        submitPosition.Set(card);
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
    }
}
