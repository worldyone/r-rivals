using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBase : ScriptableObject
{
    // カードの基礎データ
    [SerializeField] new string name;
    [SerializeField] CardType type;
    [SerializeField] int number;
    [SerializeField] Sprite icon;
    [TextArea]
    [SerializeField] string description;

    public string Name { get => name; set => name = value; }
    public CardType Type { get => type; set => type = value; }
    public int Number { get => number; set => number = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public string Description { get => description; set => description = value; }
}

public enum CardType
{
    Clown,
    Princess,
    Spy,
    Assassin,
    Minister,
    Magician,
    Shogun,
    Prince,
}
