using System.Collections;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2f;
    public Color fadeColor;
    private Renderer rend;

    Color newColor;
    Color newColor2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
            FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }
    
    public void FadeOut()
    {
        Fade(0, 1);
    }

    // transit form alphaIn to alphaOut value
    public void Fade(float alphaIn, float alphaOut) 
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    // function that play through time
    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer/fadeDuration);
            
            rend.material.SetColor("_BaseColor", newColor);
            timer += Time.deltaTime;

            // wait for 1 frame
            yield return null;
        }

        newColor2 = fadeColor;
        newColor2.a = alphaOut;

        rend.material.SetColor("_BaseColor", newColor2);
    }
}

