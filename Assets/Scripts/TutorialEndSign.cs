using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;

public class TutorialEndSign : MonoBehaviour
{
    public GameObject popup;
    public GameObject clickPanel;
    string wheel;
    bool wheel1, wheel2, wheel3, wheel4;
    
    void Start()
    {
        wheel1 = false;
        wheel2 = false;
        wheel3 = false;
        wheel4 = false;
    }

    private void Update() {
        if(!SteamManager.Initialized){return;} 
    }

    void OnTriggerStay(Collider coll)
    {
        wheel = coll.gameObject.name;
        if (wheel == "BUS_wheelLB")
            wheel1 = true;
        else if (wheel == "BUS_wheelLF")
            wheel2 = true;
        else if (wheel == "BUS_wheelRB")
            wheel3 = true;
        else if (wheel == "BUS_wheelRF")
            wheel4 = true;

        
        if (wheel1 && wheel2 && wheel3 && wheel4 && bus.sss == 0)
        {
            popup.SetActive(true);
            clickPanel.SetActive(true);
            //SteamUserStats.ResetAllStats(true);
            SteamUserStats.SetAchievement("NEWBIE_DRIVER");
            SteamUserStats.StoreStats();
        }
    }

}
