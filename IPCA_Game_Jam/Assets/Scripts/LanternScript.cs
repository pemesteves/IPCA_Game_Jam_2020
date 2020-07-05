using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{

    public Light lightToFade;
    public float fadeTime = 15f;
    private float counter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut(lightToFade, fadeTime));   
    }

    IEnumerator FadeOut(Light lightToFade, float duration)
    {
        float minLuminosity = 0;
        float maxLuminosity = 5.0f; 

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(maxLuminosity, minLuminosity, counter / duration);

            yield return null;
        }
    }

    public void ResetCounter(){
        counter = 0f;
        StopCoroutine("FadeOut");
        StartCoroutine(FadeOut(lightToFade, fadeTime));
    }

}
