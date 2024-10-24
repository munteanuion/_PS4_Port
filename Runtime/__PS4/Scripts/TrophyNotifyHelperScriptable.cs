using UnityEngine;

[CreateAssetMenu(fileName = "TrophyNotifyHelperScriptable", menuName = "ScriptableObjects/TrophyNotifyHelperScriptable", order = 1)]
public class TrophyNotifyHelperScriptable : ScriptableObject
{
    /*----------------------------*/

    #region PUBLIC ATTRIBUTES

    private static TrophyNotifyHelperScriptable _instance;

    public static TrophyNotifyHelperScriptable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<TrophyNotifyHelperScriptable>("TrophyNotifyHelperScriptable");

                if (_instance == null)
                {
                    Debug.LogError("TrophyNotifyHelperScriptable not found. Please create one in the Resources folder.");
                }
            }
            return _instance;
        }
    }

    #endregion

    /*----------------------------*/

    #region PRIVATE ATTRIBUTES

    private const string SAVE_KEY_CUT_TREES = "SAVE_KEY_CUT_TREES_TROPHY";

    #endregion

    /*----------------------------*/

    #region PUBLIC METHODS

    public void UnlockTrophy(int index)
    {
        if (TrophyNotify.Instance) TrophyNotify.Instance.UnlockTrophy(index);
    }

    #endregion

    /*----------------------------*/

    #region PRIVATE METHODS


    #endregion

    /*----------------------------*/

    #region UNITY FUNCTIONS


    #endregion

    /*----------------------------*/
}
