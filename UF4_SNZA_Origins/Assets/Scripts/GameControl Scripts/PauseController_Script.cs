using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController_Script : MonoBehaviour
{
    public static bool isGamePaused { get; private set; } = false;
    public static void SetPause (bool pause)
    {
        isGamePaused = pause;
        SetTimescale0();
    }
    public static void SetTimescale0()
    {
        if (isGamePaused) { Time.timeScale = 0f;}
        else if (!isGamePaused) { Time.timeScale = 1f; }
    }
}
