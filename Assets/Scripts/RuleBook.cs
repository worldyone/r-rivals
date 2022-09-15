using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    public Result GetResult(Battler player, Battler enemy)
    {
        if (player.SubmitCard.Base.Number > enemy.SubmitCard.Base.Number)
        {
            return Result.TurnWin;
        }
        else if (player.SubmitCard.Base.Number < enemy.SubmitCard.Base.Number)
        {
            return Result.TurnLose;
        }

        return Result.TurnDraw;
    }
}

public enum Result
{
    TurnWin,
    TurnLose,
    TurnDraw,
    TurnWin2,
    TurnLose2,
    GameWin,
    GameLose,
    GameDraw,
}
