using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

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
    /// 4. Hooman(appears after level 1)
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

    private float VeggieScore = 0, CarbonScore = 0, ProteinScore = 0, FatScore = 0, WaterScore = 0, tempScore = 0, sum = 0;

    public static int RemainingWeek;

    public static int FailCounter;

    public static bool End = false;

    public static int LevelToGo;
    public Button NextLevel;

    [SerializeField]
    private FadeScreen fadeScreen;

    void Awake()
    {
        NextLevel.onClick.AddListener(NextLevel_onClick); //subscribe to the onClick event
    }

    public void Calculate() {

        Debug.Log("Current scene number was " + ExitChecking.CurrentGameLevel);
        // get previous level index
        LevelToGo = ExitChecking.CurrentGameLevel;

        // restore the local variables
        VeggieScore = 0;
        CarbonScore = 0;
        ProteinScore = 0;
        FatScore = 0;
        WaterScore = 0;
        tempScore = 0;
        sum = 0;

        RemainingWeek = 0;

        // get data from outside
        VeggieScore = ExitChecking.VeggieScore;
        CarbonScore = ExitChecking.CarbonScore;
        ProteinScore = ExitChecking.ProteinScore;
        FatScore = ExitChecking.FatScore;
        WaterScore = ExitChecking.WaterScore;

        /// Thinking time:
        /// Remaining week begin with 0
        /// that's why player is getting food from supermarket
        /// 
        /// In the checkout scene, remaining week++ -> shown in hand menu(GameMenuManager.cs)
        /// Then the number in hand menu represents the remaining food from last level can lasts for ? week(s)
        /// If so, hand menu number = last checkout scene remaining week value
        /// or
        /// make it useful?
        /// implement the current nutrition value(in week? in score?) of the food player collected?
        /// or
        /// use it to display score?

        // restore the parameters
        ExitChecking.VeggieScore = 0;
        ExitChecking.CarbonScore = 0;
        ExitChecking.ProteinScore = 0;
        ExitChecking.FatScore = 0;
        ExitChecking.WaterScore = 0;

        ExitChecking.CurrentGameLevel = 0;

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
        sum = (VeggieScore + CarbonScore + ProteinScore + WaterScore) - FatScore;

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
        /// but human cannot live with just water for long
        /// 
        /// somewhere in the script
        /// put a mystery part with a hilarious high score
        /// e.g. 100000000 for every piece, total 5 piece
        /// total score >= 500000000 -> Good ending(refer to points above)
        /// 
        /// (just a rough idea)

        // regular food logic
        if (VeggieScore / 100 >= 1)
            //RemainingWeek += (int)Mathf.Round(VeggieScore / 100);
            RemainingWeek += Mathf.FloorToInt(VeggieScore / 100);
        else
            tempScore += VeggieScore;

        if (ProteinScore / 100 >= 1)
            //RemainingWeek += (int)Mathf.Round(ProteinScore / 100);
            RemainingWeek += Mathf.FloorToInt(ProteinScore / 100);
        else
            tempScore += ProteinScore;

        if (CarbonScore / 100 >= 1)
            //RemainingWeek += (int)Mathf.Round(CarbonScore / 100);
            RemainingWeek += Mathf.FloorToInt(CarbonScore / 100);
        else
            tempScore += CarbonScore;

        if (tempScore / 100 >= 1)
            //RemainingWeek += (int)Mathf.Round(tempScore / 100);
            RemainingWeek += Mathf.FloorToInt(tempScore / 100);

        // water logic
        if (WaterScore / 100 >= 1)
        {
            if ((VeggieScore + ProteinScore + CarbonScore) / 100 < 1)
                RemainingWeek += 1;
            else
                //RemainingWeek += (int)Mathf.Round(WaterScore / 100);
                RemainingWeek += Mathf.FloorToInt(WaterScore / 100);
        }

        /// gamification part
        // reconsider the number, think of one bigger than 100
        // if (FatScore / ? >= 1)
        //     die from obese -> trigger an ending

        // if (FatScore / 200 >= 1)
        // die from obese -> trigger an ending

        Sum.text = "" + sum;
        CumulativeScore.text = "" + TotalScore;
        Remaining.text = "" + RemainingWeek;

        if (FatScore < 500)
        {
            if (RemainingWeek >= 3)
            {
                LevelToGo++;
                FailCounter = 0;
                NextLevel.GetComponentInChildren<TextMeshProUGUI>().text = "Next Level";
            }

            if (FailCounter < 3)
            {
                if (RemainingWeek <= 3)
                {
                    // LevelToGo retrieve data at the beginning
                    FailCounter += 1;
                    NextLevel.GetComponentInChildren<TextMeshProUGUI>().text = "Go back and grab more";
                }
            }
            else
            {
                // normally SceneTransitionManager.TargetScene is not null at this point
                LevelToGo = 1; // go to transition scene to watch an ending
                Triggering.InputCutsceneIndex(1);
                End = true;
                NextLevel.GetComponentInChildren<TextMeshProUGUI>().text = "You failed your destiny";
            }
        }
        else
        {
            LevelToGo = 1; // go to transition scene to watch an ending
            Triggering.InputCutsceneIndex(2);   // refer to cutscene index 2: obesity
            End = true;
            NextLevel.GetComponentInChildren<TextMeshProUGUI>().text = "You are fat, meet your fate";
        }

        NextLevel.interactable = true;
    }

    private void NextLevel_onClick()
    {
        /*
        if (NextLevel.GetComponentInChildren<TextMeshProUGUI>().text == "Next Level")
        {
            // go to next level...
            // scene...
            SceneTransitionManager.FadeScreenAnimation();
            SceneManager.LoadScene(LevelToGo);
        }
        else
        {
            // stay in the same level...
            // scene...

            throw new NotImplementedException();
        }
        */

        //SceneTransitionManager.FadeScreenAnimation();
        StartCoroutine(FadeScreenAnimation());

        if (NextLevel.GetComponentInChildren<TextMeshProUGUI>().text == "Next Level")
            FailCounter = 0;    // restore

        SceneManager.LoadScene(LevelToGo);
        LevelToGo = 0;
    }

    IEnumerator FadeScreenAnimation()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // set FadeScreen to inactive, to prevent blocking ray interactors and UI canvas
        fadeScreen.gameObject.SetActive(false);
    }
}
