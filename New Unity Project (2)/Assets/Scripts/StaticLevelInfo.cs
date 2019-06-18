using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticLevelInfo
{
    public static int PreviousScene { get; set; }

    private static int _nextSceneToLoad = 0;
    public static int NextSceneToLoad
    {
        get { return _nextSceneToLoad; }
        set { PreviousScene = _nextSceneToLoad;
            _nextSceneToLoad = value;
        }
    }



}
