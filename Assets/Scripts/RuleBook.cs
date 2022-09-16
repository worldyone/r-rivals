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
        int playerNumber = player.SubmitCard.Base.Number;
        int enemyNumber = enemy.SubmitCard.Base.Number;

        /// 戦闘前処理
        // 将軍効果の発動
        if (player.IsAddNumber)
        {
            playerNumber += 2;
            player.IsAddNumber = false;
        }
        if (enemy.IsAddNumber)
        {
            enemyNumber += 2;
            enemy.IsAddNumber = false;
        }

        /// タイプ別戦闘処理
        // ・マジシャンがいれば数字対決
        if (ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Magician))
        {
            return NumberBattle(playerNumber, enemyNumber);
        }

        // ・密偵がいるなら追加効果(次のターン、相手は先に出す)
        if (player.SubmitCard.Base.Type == CardType.Spy
            && enemy.SubmitCard.Base.Type != CardType.Spy)
        {
            enemy.IsFirstSubmit = true;
        }
        if (enemy.SubmitCard.Base.Type == CardType.Spy
            && player.SubmitCard.Base.Type != CardType.Spy)
        {
            player.IsFirstSubmit = true;
        }

        // ・将軍がいるなら追加効果(次のターン、数字+2)
        if (player.SubmitCard.Base.Type == CardType.Shogun)
        {
            player.IsAddNumber = true;
        }
        if (enemy.SubmitCard.Base.Type == CardType.Shogun)
        {
            enemy.IsAddNumber = true;
        }

        // ・道化がいるなら引き分け
        if (ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Clown))
        {
            return Result.TurnDraw;
        }

        // ・暗殺者がいて、王子がいないなら数字の強弱を反転
        if (ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Assassin)
            && !ExistTypeInBattle(player.SubmitCard, enemy.SubmitCard, CardType.Prince))
        {
            return NumberBattle(enemyNumber, playerNumber);
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
            Result ministerBattleResult;
            ministerBattleResult = NumberBattle(playerNumber, enemyNumber);

            // 自分が大臣で自分が勝ったなら2倍の勝利
            if (player.SubmitCard.Base.Type == CardType.Minister
                && ministerBattleResult == Result.TurnWin)
            {
                ministerBattleResult = Result.TurnWin2;
            }

            // 相手が大臣で自分が負けたなら2倍の敗北
            if (enemy.SubmitCard.Base.Type == CardType.Minister
                && ministerBattleResult == Result.TurnLose)
            {
                ministerBattleResult = Result.TurnLose2;
            }

            return ministerBattleResult;
        }

        // 通常の数字対決
        return NumberBattle(playerNumber, enemyNumber);
    }

    private Result PrinceVsPrincessBattle(CardBase playerCard, CardBase enemyCard)
    {
        if (playerCard.Type == CardType.Prince)
            return Result.GameLose;
        else
            return Result.GameWin;
    }

    Result NumberBattle(int playerNumber, int enemyNumber)
    {
        if (playerNumber > enemyNumber)
        {
            return Result.TurnWin;
        }
        else if (playerNumber < enemyNumber)
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
