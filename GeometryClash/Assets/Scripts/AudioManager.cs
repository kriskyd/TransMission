using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource koloSource, kwadratSource, doubleSource;
    // standard
    public AudioClip [] startGry, wstep,
        startRundy1,
        startRundy2Kolo, startRundy2Kwadrat,
        startRundy3Kolo, startRundy3Kwadrat,
        koniecWalkiKolo, koniecWalkiKwadrat;

    public AudioClip [] walkaLosowaKolo, walkaLosowaKwadrat;
    public float walkaKoloCD = 5f, walkaKwadratCD = 2f, walkaCD = 5f;
    public int soundCounter = 0;

    void Start ()
    {

    }

    public void DoUpdate ()
    {
        //walkaKoloCD -= Time.deltaTime;
        //walkaKwadratCD -= Time.deltaTime;

        //if (walkaKoloCD <= 0f)
        //{
        //    walkaKoloCD = Random.Range (5f, 10f);
        //    koloSource.PlayOneShot (walkaLosowaKolo [Random.Range (0, walkaLosowaKolo.Length)]);
        //}
        //if (walkaKwadratCD <= 0f)
        //{
        //    walkaKwadratCD = Random.Range (5f, 10f);
        //    kwadratSource.PlayOneShot (walkaLosowaKwadrat [Random.Range (0, walkaLosowaKwadrat.Length)]);
        //}
        walkaCD -= Time.deltaTime;
        if (walkaCD <= 0f && !koloSource.isPlaying && !kwadratSource.isPlaying)
        {
            soundCounter += 1;
            walkaCD = Random.Range (2.5f, 4.5f);
            if (soundCounter % 2 == 0)
                koloSource.PlayOneShot (walkaLosowaKolo [Random.Range (0, walkaLosowaKolo.Length)]);
            else
                kwadratSource.PlayOneShot (walkaLosowaKwadrat [Random.Range (0, walkaLosowaKwadrat.Length)]);

        }


    }
}
