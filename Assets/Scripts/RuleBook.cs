using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    //* カード効果
    // ・マジシャンがいれば数字対決
    // ・密偵がいるなら追加効果
    // ・将軍がいるなら追加効果
    // ・道化がいるなら引き分け
    // ・暗殺者がいて、王子がいないなら数字の強弱を反転
    // ・王子と姫がいるならGameの勝利判定
    // ・大臣がいるなら2倍の勝利判定
    // ・上記以外であれば数字対決
    //*/
    public Result GetResult(Battler player, Battler enemy)
    {
        // ・マジシャンがいれば数字対決
        if (ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Magician))
        {
            return NumberBattle(player.SubmitCard.Base, enemy.SubmitCard.Base);
        }

        // TODO:
        // ・密偵がいるなら追加効果

        // TODO:
        // ・将軍がいるなら追加効果

        // ・道化がいるなら引き分け
        if (ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Clown))
        {
            return Result.TurnDraw;
        }

        // ・暗殺者がいて、王子がいないなら数字の強弱を反転
        if (ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Assassin)
            && !ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Prince))
        {
            return NumberBattle(enemy.SubmitCard.Base, player.SubmitCard.Base);
        }

        // ・王子と姫がいるならGameの勝利判定
        if (ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Prince)
            && ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Princess))
        {
            return PrinceVsPrincessBattle(player.SubmitCard.Base, enemy.SubmitCard.Base);
        }

        // ・大臣がいるなら2倍の勝利判定
        if (ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Minister))
        {
            return NumberMinisterBattle(player.SubmitCard.Base, enemy.SubmitCard.Base);
        }

        // 通常の数字対決
        return NumberBattle(player.SubmitCard.Base, enemy.SubmitCard.Base);
    }

    private Result PrinceVsPrincessBattle(CardBase playerCard, CardBase enemyCard)
    {
        if (playerCard.Type == CardType.Prince)
            return Result.GameLose;
        else
            return Result.GameWin;
    }

    private Result NumberMinisterBattle(CardBase playerCard, CardBase enemyCard)
    {
        if (playerCard.Type == CardType.Minister)
        {
            if (playerCard.Number > enemyCard.Number)
            {
                return Result.TurnWin2;
            }
            else if (playerCard.Number < enemyCard.Number)
            {
                return Result.TurnLose;
            }
            else
            {
                return Result.TurnDraw;
            }
        }
        else
        {
            if (playerCard.Number > enemyCard.Number)
            {
                return Result.TurnWin;
            }
            else if (playerCard.Number < enemyCard.Number)
            {
                return Result.TurnLose2;
            }
            else
            {
                return Result.TurnDraw;
            }
        }
    }

    Result NumberBattle(CardBase playerCard, CardBase enemyCard)
    {
        if (playerCard.Number > enemyCard.Number)
        {
            return Result.TurnWin;
        }
        else if (playerCard.Number < enemyCard.Number)
        {
            return Result.TurnLose;
        }
        return Result.TurnDraw;
    }

    bool ExistTypeInBattle(Card playerCard, Card enemyCard, CardType type)
    {
        return playerCard.Base.Type == type || enemyCard.Base.Type == type;
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
