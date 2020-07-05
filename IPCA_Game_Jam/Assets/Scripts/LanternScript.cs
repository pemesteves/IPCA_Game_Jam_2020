using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{

    public Light lightToFade;
    public float fadeTime = 15f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeOut(lightToFade, fadeTime));   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator fadeOut(Light lightToFade, float duration)
    {
        float minLuminosity = 0;
        float maxLuminosity = 5.0f; 

        float counter = 0f;

        float currentIntensity = lightToFade.intensity;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(maxLuminosity, minLuminosity, counter / duration);

            yield return null;
        }
    }


}
