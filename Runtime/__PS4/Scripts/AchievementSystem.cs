using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_PS4
using UnityEngine.PS4;
#endif

public enum ETrophey
{
    Platinum = 0,

    Equip_stone_axe_tool1,
    Equip_one_leather_armor2,
    Equip_aluminium_sword_weapon3,
    Cut_down_25_tree4,
    Discover_iron5,
    Discover_dark_forest6,
    Kill_25_monsters7,
    Enter_the_portal8,
    Equip_iron_axe_tool9,
    Equip_one_obsidian_armor10,
    Equip_obsidian_sword_weapon11,
    Cut_down_150_tree12,
    Discover_obsidian13,
    Discover_blood_forest14,
    Kill_150_monsters15,
    Equip_gold_axe_tool16,
    Equip_one_gold_armor17,
    Equip_gold_sword_weapon18,
    Cut_down_300_tree19,
    Discover_gold20,
    Discover_dead_forest21,
    Kill_300_monsters22

}
public class AchievementSystem : Singleton<AchievementSystem>
{
#if UNITY_PS4
    public PS4Input.LoggedInUser loggedInUser;
#endif

    private void Start()
    {
#if UNITY_PS4

        //DontDestroyOnLoad(this);
        Sony.NP.Main.OnAsyncEvent += Main_OnAsyncEvent;
        Sony.NP.InitToolkit init = new Sony.NP.InitToolkit();
        init.contentRestrictions.DefaultAgeRestriction = 10;

        init.threadSettings.affinity = Sony.NP.Affinity.AllCores;
        init.SetPushNotificationsFlags(Sony.NP.PushNotificationsFlags.None);
        Sony.NP.AgeRestriction[] ageRestrictions = new Sony.NP.AgeRestriction[2];
        ageRestrictions[0] = new Sony.NP.AgeRestriction(10, new Sony.NP.Core.CountryCode("us"));
        ageRestrictions[1] = new Sony.NP.AgeRestriction(10, new Sony.NP.Core.CountryCode("au"));
        init.contentRestrictions.AgeRestrictions = ageRestrictions;


        loggedInUser = PS4Input.RefreshUsersDetails(0);
        //loggedInUser = User.GetActiveUserId;
        try
        {
            //Initialize the NPToolkit2
            var initiliazi = Sony.NP.Main.Initialize(init);
            Debug.LogError("Initialize " + initiliazi);
            //Register the Trophy Pack
            RegisterTrophyPack();

        }
        catch (Sony.NP.NpToolkitException e)
        {
            Debug.LogError("Error initializing the NPToolkit2 : " + e.ExtendedMessage);
        }
#endif
    }

    public void RegisterTrophyPack()
    {
#if UNITY_PS4
        try
        {
            Sony.NP.Trophies.RegisterTrophyPackRequest request = new Sony.NP.Trophies.RegisterTrophyPackRequest();

            //request.UserId = User.GetActiveUserId;
            request.UserId = loggedInUser.userId;
            
            Sony.NP.Core.EmptyResponse response = new Sony.NP.Core.EmptyResponse();

            // Make the async call which returns the Request Id 
            int requestId = Sony.NP.Trophies.RegisterTrophyPack(request, response);
            Debug.LogError("RegisterTrophyPack Async : Request Id = " + requestId);
        }
        catch (Sony.NP.NpToolkitException e)
        {
            Debug.LogError("Exception : " + e.ExtendedMessage);
        }
#endif
    }

    public void CheckName(int nextTrophyId) {

        Debug.Log((ETrophey)nextTrophyId);
    }

    public void UnlockTrophy(int nextTrophyId)
    {
        Debug.LogError("--Unlock " + ((ETrophey)nextTrophyId).ToString() + ", Index = " + nextTrophyId);
        //Debug.LogError(((ETrophey)nextTrophyId).ToString());
#if UNITY_PLAYSTATION && !UNITY_EDITOR
        try
        {
            Sony.NP.Trophies.UnlockTrophyRequest request = new Sony.NP.Trophies.UnlockTrophyRequest();

            request.TrophyId = nextTrophyId;

            request.UserId = loggedInUser.userId;

            Sony.NP.Core.EmptyResponse response = new Sony.NP.Core.EmptyResponse();

            // Make the async call which returns the Request Id 
            int requestId = Sony.NP.Trophies.UnlockTrophy(request, response);
            Debug.LogError("GetUnlockedTrophies Async : Request Id = " + requestId);
        }
        catch (Sony.NP.NpToolkitException e)
        {
            Debug.LogError("Exception : " + e.ExtendedMessage);
        }
#endif
    }
    public void DisplayTrophyPackDialog()
    {
#if UNITY_PS4
        try
        {
            Sony.NP.Trophies.DisplayTrophyListDialogRequest request = new Sony.NP.Trophies.DisplayTrophyListDialogRequest();
            request.UserId =  loggedInUser.userId;

            Sony.NP.Core.EmptyResponse response = new Sony.NP.Core.EmptyResponse();

            // Make the async call which will return the Request Id 
            int requestId = Sony.NP.Trophies.DisplayTrophyListDialog(request, response);
            Debug.Log("DisplayTrophyPackDialog Async : Request Id = " + requestId);
        }
        catch (Sony.NP.NpToolkitException e)
        {
            Debug.Log("Exception : " + e.ExtendedMessage);
        }
#endif
    }

    private void Main_OnAsyncEvent(
#if UNITY_PS4
        Sony.NP.NpCallbackEvent callbackEvent
#endif
        )
    {
#if UNITY_PS4
        Debug.LogError("Event: Service = (" + callbackEvent.Service + ") : API Called = (" + callbackEvent.ApiCalled + ") : Request Id = (" + callbackEvent.NpRequestId + ") : Calling User Id = (" + callbackEvent.UserId + ")");
        HandleAsyncEvent(callbackEvent);
#endif
    }

    private void HandleAsyncEvent(
#if UNITY_PS4
        Sony.NP.NpCallbackEvent callbackEvent
#endif
        )
    {
#if UNITY_PS4
        /* try
         {
             if (callbackEvent.Response != null)
             {
                 //We got an error response 
                 if (callbackEvent.Response.ReturnCodeValue < 0)
                 {
                     Debug.LogError("Response : " + callbackEvent.Response.ConvertReturnCodeToString(callbackEvent.ApiCalled));
                 }
                 else
                 {
                     //The callback of the event is a trophyUnlock event
                     if (callbackEvent.ApiCalled == Sony.NP.FunctionTypes.TrophyUnlock)
                     {
                         Debug.Log("Trophy Unlock : " + callbackEvent.Response.ConvertReturnCodeToString(callbackEvent.ApiCalled));
                     }
                 }
             }
         }
         catch (Sony.NP.NpToolkitException e)
         {
             Debug.Log("Main_OnAsyncEvent Exception = " + e.ExtendedMessage);
         }*/
        if (callbackEvent.Service == Sony.NP.ServiceTypes.Trophy)
        {
            switch (callbackEvent.ApiCalled)
            {
                case Sony.NP.FunctionTypes.TrophyRegisterTrophyPack:
                    {
                        User user = User.FindUser(callbackEvent.UserId);

                        if (user != null)
                        {
                            user.trophyPackRegistered = true;
                        }

                        OutputRegisterTrophyPack(callbackEvent.Response as Sony.NP.Core.EmptyResponse);
                    }
                    break;
                case Sony.NP.FunctionTypes.TrophySetScreenshot:
                    OutputSetScreenshot(callbackEvent.Response as Sony.NP.Core.EmptyResponse);
                    break;
                case Sony.NP.FunctionTypes.TrophyUnlock:
                    OutputTrophyUnlock(callbackEvent.Response as Sony.NP.Core.EmptyResponse);
                    break;
                case Sony.NP.FunctionTypes.TrophyGetUnlockedTrophies:
                    OutputUnlockedTrophies(callbackEvent.Response as Sony.NP.Trophies.UnlockedTrophiesResponse);
                    break;
                case Sony.NP.FunctionTypes.TrophyDisplayTrophyListDialog:
                    OutputDisplayTrophyListDialog(callbackEvent.Response as Sony.NP.Core.EmptyResponse);
                    break;
                default:
                    break;
            }
        }
#endif
    }

    private void OutputRegisterTrophyPack(
#if UNITY_PS4
        Sony.NP.Core.EmptyResponse response
#endif
        )
    {
#if UNITY_PS4
        if (response == null) return;

        Debug.LogError("CallBack: RegisterTrophyPack Empty Response");

        if (response.Locked == false)
        {
        }
#endif
    }
    private void OutputSetScreenshot(
#if UNITY_PS4
        Sony.NP.Core.EmptyResponse response
#endif
        )
    {
#if UNITY_PS4
        if (response == null) return;

        Debug.LogError("SetScreenshot Empty Response");

        if (response.Locked == false)
        {

        }
#endif
    }

    private void OutputTrophyUnlock(
#if UNITY_PS4
        Sony.NP.Core.EmptyResponse response
#endif
        )
    {
#if UNITY_PS4
        if (response == null) return;

        Debug.LogError("TrophyUnlock Empty Response");

        if (response.Locked == false)
        {

        }
#endif
    }
    private void OutputUnlockedTrophies(
#if UNITY_PS4
        Sony.NP.Trophies.UnlockedTrophiesResponse response
#endif
        )
    {
#if UNITY_PS4
        if (response == null) return;

        OnScreenLog.Add("GetUnlockedTrophies Response");

        if (response.Locked == false)
        {
            if (response.TrophyIds != null)
            {
                OnScreenLog.Add("Number Unlocked Trophys = " + response.TrophyIds.Length);
                for (int i = 0; i < response.TrophyIds.Length; i++)
                {
                    OnScreenLog.Add("   : " + response.TrophyIds[i]);
                }
            }
        }
#endif
    }
    private void OutputDisplayTrophyListDialog(
#if UNITY_PS4
        Sony.NP.Core.EmptyResponse response
# endif
        )
    {
#if UNITY_PS4
        if (response == null) return;

        OnScreenLog.Add("DisplayTrophyListDialog Empty Response");

        if (response.Locked == false)
        {

        }
#endif
    }
}
