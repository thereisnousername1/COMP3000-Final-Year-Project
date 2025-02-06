using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    /// 3. Timer
    /// 
    ///     When a timer runs out of time, it doesn't end the level immediately
    ///     But spawning countless hoomans
    ///     Makes it impossible to escape
    ///     
    /// 4. Hooman
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

    public TextMeshProUGUI Value1, Value2, Value3, Value4, Value5, Sum;
    public static float TotalScore;

    public float VeggieScore = 0, CarbonScore = 0, ProteinScore = 0, FatScore = 0, WaterScore = 0;

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


        // simple logic
        float sum = (VeggieScore + CarbonScore + ProteinScore) - FatScore;
        TotalScore += sum;
    }

}
