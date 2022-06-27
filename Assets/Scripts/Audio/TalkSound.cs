using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkSound : MonoBehaviour
{
    public GameObject mouthOpen, mouthShut;
    bool isMouthOpen;
    int mouthDelay = 0;

    public bool radio;


    private void Update()
    {
        if(mouthDelay >= 3 )
        {
            isMouthOpen = !isMouthOpen;
            mouthDelay = 0;
        }

        if(isMouthOpen)
        {
            mouthOpen.SetActive(true);
            mouthShut.SetActive(false);
        }
        else
        {
            mouthOpen.SetActive(false);
            mouthShut.SetActive(true);
        }

        if(Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(1);
        }
        
    }
    public void PlayTalkSound()
    {
        FindObjectOfType<AudioManager>().Play("TalkSound", Random.Range(0.8f, 0.9f));
        mouthDelay++;
    }

}
