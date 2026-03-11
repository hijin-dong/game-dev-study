using UnityEngine;
using System.Diagnostics;
using debug = UnityEngine.Debug;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text TimerUI;
    public static string TimeElapsed;
    public static Stopwatch StopWatch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StopWatch = new Stopwatch();
        StopWatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        TimeElapsed = (StopWatch.ElapsedMilliseconds/1000.0).ToString("n2") + "s";
        TimerUI.text = TimeElapsed;
    }

    public static void StopTimer()
    {
        StopWatch.Stop();
    }
}
