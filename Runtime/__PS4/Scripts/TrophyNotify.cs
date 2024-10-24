using System.Collections;
using UnityEngine;

public class TrophyNotify: MonoBehaviour
{
    public static TrophyNotify Instance = null;
#if UNITY_PS4
    private AchievementSystem AchievementSys => AchievementSystem.Instance;
#endif

    private bool[] _trophyGetList = new bool[40];

    /// <summary>
    /// ////////////////////////////////////////////////////////////////
    /// </summary>
    /// 

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Initialize()
    {
        if (Instance == null)
        {
            GameObject trophyNotify = new GameObject(nameof(TrophyNotify));
            Instance = trophyNotify.AddComponent<TrophyNotify>();
            DontDestroyOnLoad(trophyNotify);
        }
    }


    public void UnlockTrophy(ETrophey eTrophey)
    {
        UnlockTrophy((int)eTrophey);
    }

    public void UnlockTrophy(int indexETrophey)
    {
        //aici pui if sau switch la trofee daca trebuie sa verifici ceva

        StartCoroutine(UnlockTrophy_C( indexETrophey));
    }

    private IEnumerator UnlockTrophy_C(int indexETrophey)
    {
        //Debug.Log("Index Trophy -> " + indexETrophey);
        //Debug.LogWarning("Index Trophy -> " + indexETrophey);
        //Debug.LogError("Index Trophy -> " + indexETrophey);
#if !UNITY_PS4
        Debug.LogError("--Unlock " + ((ETrophey)indexETrophey).ToString() + ", Index = " + indexETrophey);
#endif
        yield return new WaitForSecondsRealtime(0.01f);
#if UNITY_PS4
        if (!_trophyGetList[indexETrophey]) AchievementSys.UnlockTrophy((int)indexETrophey);
#endif
        _trophyGetList[indexETrophey] = true;
        yield break;
    }
}
