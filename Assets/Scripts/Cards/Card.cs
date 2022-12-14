using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // カードUI
    // カード処理
    [SerializeField] Text nameText;
    [SerializeField] Text numberText;
    [SerializeField] Image icon;
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject hidePanel;
    public CardBase Base { get; private set; }

    // 関数を登録する
    public UnityAction<Card> OnClickCard;

    public void Set(CardBase cardBase, bool isEnemy)
    {
        Base = cardBase;
        nameText.text = cardBase.Name;
        numberText.text = cardBase.Number.ToString();
        icon.sprite = cardBase.Icon;
        descriptionText.text = cardBase.Description;
        hidePanel.SetActive(isEnemy);
    }

    public void OnClick()
    {
        OnClickCard?.Invoke(this);
    }

    public void Open()
    {
        hidePanel.SetActive(false);
    }

    public void OnPointerEnter()
    {
        transform.position += Vector3.up * 0.3f;
        transform.localScale = Vector3.one * 1.3f;
        GetComponentInChildren<Canvas>().sortingLayerName = "Overlay";
    }

    public void OnPointerExit()
    {
        transform.position -= Vector3.up * 0.3f;
        transform.localScale = Vector3.one;
        GetComponentInChildren<Canvas>().sortingLayerName = "Default";
    }
}
