using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int[] GameTable = new int[64];
    public GameObject SlotPrefab,PiecePrefab;
    public int CurrentPlayer;

    void Start()
    {
        bool _black = true;
        for (int i = 0; i < 8; i++)
        {
            if (_black)
                _black = false;
            else
                _black = true;
            for (int j = 0; j < 8; j++)
            {
                GameObject slotToSpawn = Instantiate(SlotPrefab);
                slotToSpawn.transform.position = new Vector3(i, j, 0);
                slotToSpawn.GetComponent<SlotView>().GameLogic = this;
                if(_black)
                slotToSpawn.gameObject.GetComponent<SpriteRenderer>().color= Color.black;
                if (_black)
                    _black = false;
                else
                    _black = true;
            }
        }
        GameObject pieceToSpawn1 = Instantiate(PiecePrefab);
        pieceToSpawn1.transform.position = new Vector3(3, 3, -1);
        GameObject pieceToSpawn2 = Instantiate(PiecePrefab);
        pieceToSpawn2.transform.position = new Vector3(3, 4, -1);
        GameObject pieceToSpawn3 = Instantiate(PiecePrefab);
        pieceToSpawn3.transform.position = new Vector3(4, 3, -1);
        GameObject pieceToSpawn4 = Instantiate(PiecePrefab);
        pieceToSpawn4.transform.position = new Vector3(4, 4, -1);
        pieceToSpawn2.GetComponent<Animator>().SetBool("White", true);
        pieceToSpawn3.GetComponent<Animator>().SetBool("White", true);
        pieceToSpawn2.GetComponent<PieceView>().WhiteColor = true;
        pieceToSpawn3.GetComponent<PieceView>().WhiteColor = true;
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
