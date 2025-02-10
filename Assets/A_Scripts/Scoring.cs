using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Scoring : MonoBehaviour
{
    /// <summary>
    /// Level design
    /// 
    /// Thinking of the future development, I have these ideas pop up in my mind
    /// 
    /// 1. Endings
    /// 
    ///     Not collecting enough healthy food / collecting too much unhealthy food = bad ending(s)
    ///     Collecting enough healthy food every level -> somehow the player find a seemingly possible way to escape? -> normal ending (We don't know their fate)
    ///     Collect all mysterious parts -> build a spaceship by that? -> good ending (Actually escaped)
    ///     
    /// 2. Awards
    /// 
    ///     Whether the player can or can not proceed to the next level depends on grades, grades depends on score?
    ///     
    ///     New mode unlock after beating the game once (playability)
    ///         Zombie fighting mode(endless)
    ///         (maybe) map editor mode?
    ///         (maybe) selectable levels?
    ///         
    ///     Some other awards? (game art style changing?)
    ///     
    /// 3. Timer(Not a must?)
    /// 
    ///     When a timer runs out of time, it doesn't end the level immediately
    ///     But spawning countless hoomans
    ///     Makes it impossible to escape
    ///     
    /// 4. Hooman(after level 1)
    /// 
    ///     Defeat them rewards bonus score
    ///     More realistic moving speed based on distance to player?
    ///     Navigation mesh?
    ///     
    /// 5. Level design
    /// 
    ///     Randomly generate items on the shelf?
    ///     Using cutscene to tell story?
    ///     Using only words to tell story?
    ///     
    /// 6. Instructions
    /// 
    /// 7. Ranking?
    /// 
    /// </summary>

    public TextMeshProUGUI Value1, Value2, Value3, Value4, Value5, Sum, CumulativeScore, Remaining;
    public static float TotalScore;

    public float VeggieScore = 0, CarbonScore = 0, ProteinScore = 0, FatScore = 0, WaterScore = 0;

    public static int RemainingWeek;

    public Button NextLevel;

    void Awake()
    {
        NextLevel.onClick.AddListener(NextLevel_onClick); //subscribe to the onClick event

    }

    public void Calculate() {

        // get data from outside
        VeggieScore = ExitChecking.VeggieScore;
        CarbonScore = ExitChecking.CarbonScore;
        ProteinScore = ExitChecking.ProteinScore;
        FatScore = ExitChecking.FatScore;
        WaterScore = ExitChecking.WaterScore;
        
        // restore the parameters
        ExitChecking.VeggieScore = 0;
        ExitChecking.CarbonScore = 0;
        ExitChecking.ProteinScore = 0;
        ExitChecking.FatScore = 0;
        ExitChecking.WaterScore = 0;

        Value1.text = "" + VeggieScore;
        Value2.text = "" + CarbonScore;
        Value3.text = "" + ProteinScore;
        Value4.text = "" + FatScore;
        Value5.text = "" + WaterScore;

        // defeating hooman bonus score
        // ...

        // collecting mystery parts bonus score
        // ...

        // fat is the part I want player to avoid
        // I reward those scores on veggie, carbon, and protein
        // simple logic
        float sum = (VeggieScore + CarbonScore + ProteinScore + WaterScore) - FatScore;

        // for multiple level
        TotalScore += sum;

        /// advancement
        /// every 100 score of particular part(exclude fat, player died from obese) = 1 more week of food storage
        /// hand menu shall show the remaining week the food can last
        /// whether proceeding to next level or not should depends on remaining week s of food left(e.g. >=3 weeks -> proceed to next level)
        ///
        /// also not getting enough water = died instantly
        /// more than or equal to 100 score of water = 1 more week of food storage
        /// because human can still remains alive for some time with enough water and not enough food
        /// 
        /// somewhere in the script
        /// put a mystery part with a hilarious high score
        /// e.g. 100000000 for every piece, total 5 piece
        /// total score >= 500000000 -> Good ending(refer to points above)
        /// 
        /// (just a rough idea)

        if (VeggieScore / 100 >= 1)
            RemainingWeek += (int)Mathf.Round(VeggieScore / 100);

        if (ProteinScore / 100 >= 1)
            RemainingWeek += (int)Mathf.Round(ProteinScore / 100);

        if (CarbonScore / 100 >= 1)
            RemainingWeek += (int)Mathf.Round(CarbonScore / 100);

        if (WaterScore / 100 >= 1)
            RemainingWeek += 1;

        /// gamification part
        // reconsider the number, think of one bigger than 100
        // if (FatScore / ? >= 1)
        //     die from obese -> trigger an ending

        // if (FatScore / 200 >= 1)
        // die from obese -> trigger an ending

        Sum.text = "" + sum;
        CumulativeScore.text = "" + TotalScore;
        Remaining.text = "" + RemainingWeek;

        if (RemainingWeek >= 3)
        {
            NextLevel.interactable = true;
            NextLevel.GetComponentInChildren<TextMeshProUGUI>().text = "Next Level";
        }
    }

    private void NextLevel_onClick()
    {
        if(NextLevel.GetComponentInChildren<TextMeshProUGUI>().text == "Next Level")
        {
            // go to next level...
            // scene...
        }
        else
            // stay in the same level...
            // scene...

            throw new NotImplementedException();
    }

}
