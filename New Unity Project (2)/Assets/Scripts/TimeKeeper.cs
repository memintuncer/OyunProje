using UnityEngine;
using System.Collections;

//=========================================================================
// Class: TimeKeeper
// Mirrors Unity's Time.time, allowing the game to be logically paused
// while the animations, UI, and other Time.time dependant tasks keep running at the usual pace.
// TimeKeeper can be paused by the game when timestamp is set to 0,
// or by the user pressing the Pause/Break key.
//=========================================================================
public class TimeKeeper : MonoBehaviour
{

    // Variable: time
    // Access TimeKeeper.time to keep track of time with a different timescale then Time.time (read-only)
    public static float time
    {
        get { return instance.mTime; }
    }

    // Variable: timescale
    // Current timescale of the TimeKeeper.
    public static float timescale
    {
        get { return instance.mPaused ? 0 : instance.mTimescale; }
        set { instance.mTimescale = value; }
    }

    //=========================================================================
    private static TimeKeeper instance;

    private float mTime = 0.0f;
    private float mTimescale = 1.0f;
    private float mLastTimestamp = 0.0f;
    private bool mPaused = false;

    //=========================================================================
    void Start()
    {
        if (instance)
            Debug.LogError("Singleton violated (ooh!)");
        instance = this;
        instance.mLastTimestamp = Time.realtimeSinceStartup;
    }

    //=========================================================================
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Pause))
            this.mPaused = !this.mPaused;

        float realDelta = Time.realtimeSinceStartup - this.mLastTimestamp;
        this.mLastTimestamp = Time.realtimeSinceStartup;
        this.mTime += realDelta * timescale;
    }
}