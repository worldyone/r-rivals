using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;

    public BattlerHand Hand { get => hand; set => hand = value; }
}
