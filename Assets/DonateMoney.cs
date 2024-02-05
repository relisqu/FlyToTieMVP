using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class DonateMoney : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DonatePlayerMoney()
    {
        if(PlayerData.MoneyCount<=0) return;
        AudioManager.instance.Play("coin");
        PlayerData.SaveMoney(PlayerData.MoneyCount-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
