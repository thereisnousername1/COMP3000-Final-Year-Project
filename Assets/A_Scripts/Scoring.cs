using UnityEngine;

public class Scoring : MonoBehaviour
{

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

        // simple logic
        float sum = (VeggieScore + CarbonScore + ProteinScore) - FatScore;
        TotalScore += sum;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
