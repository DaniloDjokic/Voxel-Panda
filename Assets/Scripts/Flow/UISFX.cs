using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Flow
{
    public class UISFX : MonoBehaviour
    {
        public string UIClickEvent = "Play_UI_Click";
        public string UIClickPitchedEvent = "Play_UI_Click_Pitched";
        public string UICountdownEvent = "Play_Countdown";

        public void PlayUIClick(bool pitchedVersion = false)
        {
            if (pitchedVersion)
            {
                AkSoundEngine.PostEvent(UIClickPitchedEvent, gameObject);

            }
            else
            {
                AkSoundEngine.PostEvent(UIClickEvent, gameObject);

            }
        }
        public void PlayCountdown()
        {
            AkSoundEngine.PostEvent(UICountdownEvent, gameObject);
        }
    }
}