//B00160560

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{

    // simple boolean that sets audio to 1 or 0 depending on condition
    public void MuteToggle(bool muted)
    {
        if(muted == true)
        {
            // audio is muted if true
            AudioListener.volume = 0f;
        }
        else
        {
            //if not, audio is not muted
            AudioListener.volume = 1;
        }
    }
}
