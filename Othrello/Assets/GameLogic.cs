using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int[] GameTable = new int[64];
    public GameObject SlotPrefab;
    public int CurrentPlayer;

    void Start()
    {
        bool _white = true;
        for (int i = 0; i < 8; i++)
        {
            if (_white)
                _white = false;
            else
                _white = true;
            for (int j = 0; j < 8; j++)
            {
                GameObject slotToSpawn = Instantiate(SlotPrefab);
                slotToSpawn.transform.position = new Vector3(i, j, 0);
                slotToSpawn.GetComponent<SlotView>().GameLogic = this;
                if(_white)
                slotToSpawn.gameObject.GetComponent<SpriteRenderer>().color= Color.black;
                if (_white)
                    _white = false;
                else
                    _white = true;
            }
        }
    }


    void Update()
    {

    }



    public void CheckMove()
    {

    }

    public void ChangePlayerStatus()
    {
        if (CurrentPlayer == 1)
            CurrentPlayer = 2;
        else
            CurrentPlayer = 1;
    }
}
