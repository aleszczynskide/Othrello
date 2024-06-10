using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject[,] GameTable = new GameObject[8, 8];
    public GameObject SlotPrefab, PiecePrefab;
    public bool WhiteColor;
    public GameObject Pointer;
    public GameObject PointerPrefab;
    List<GameObject> PointerList = new List<GameObject>();
    bool Valid;
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
                GameTable[i, j] = slotToSpawn;
                slotToSpawn.transform.position = new Vector3(i, j, 0);
                slotToSpawn.GetComponent<SlotView>().GameLogic = this;
                slotToSpawn.GetComponent<SlotView>().PositionX = i;
                slotToSpawn.GetComponent<SlotView>().PositionY = j;
                if (_black)
                    slotToSpawn.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
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
        GameTable[3, 3].GetComponent<SlotView>().Piece = pieceToSpawn1;
        GameTable[3, 4].GetComponent<SlotView>().Piece = pieceToSpawn2;
        GameTable[4, 3].GetComponent<SlotView>().Piece = pieceToSpawn3;
        GameTable[4, 4].GetComponent<SlotView>().Piece = pieceToSpawn4;
        CheckForValidMove();
    }


    void Update()
    {

    }
    public void CheckForValidMove()
    {
        Valid = false;
        if (PointerList.Count != 0)
        {
            for (int i = 0; i < PointerList.Count; i++)
            {
                Destroy(PointerList[i]);
            }
        }
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameTable[i, j].GetComponent<SlotView>().ValidMove = false;

            }
        }
        if (WhiteColor)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameTable[i, j].GetComponent<SlotView>().Piece == null)
                        CheckForWhiteValidMove(i, j);
                }
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameTable[i, j].GetComponent<SlotView>().Piece == null)
                        CheckForBlackValidMove(i, j);
                }
            }
        }

    }
    public void CheckForWhiteValidMove(int x, int y)
    {
        List<GameObject> goList = new List<GameObject>();
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8)
            {
                if (GameTable[x + i, y].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x + i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x + i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1)
            {
                if (GameTable[x - i, y].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x - i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x - i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + y < 8)
            {
                if (GameTable[x, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (y - i > -1)
            {
                if (GameTable[x, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8 && i + y < 8)
            {
                if (GameTable[x + i, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x + i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x + i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8 && y - i > -1)
            {
                if (GameTable[x + i, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x + i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x + i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1 && y - i > -1)
            {
                if (GameTable[x - i, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x - i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x - i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1 && i + y < 8)
            {
                if (GameTable[x - i, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x - i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x - i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        if (!Valid)
        {
            GameOver();
        }
    }

    public void CheckForBlackValidMove(int x, int y)
    {
        List<GameObject> goList = new List<GameObject>();
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8)
            {
                if (GameTable[x + i, y].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x + i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x + i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1)
            {
                if (GameTable[x - i, y].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x - i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x - i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + y < 8)
            {
                if (GameTable[x, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (y - i > -1)
            {
                if (GameTable[x, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8 && i + y < 8)
            {
                if (GameTable[x + i, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x + i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x + i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8 && y - i > -1)
            {
                if (GameTable[x + i, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x + i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x + i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1 && y - i > -1)
            {
                if (GameTable[x - i, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x - i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x - i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1 && i + y < 8)
            {
                if (GameTable[x - i, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x - i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x - i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            GameTable[x, y].GetComponent<SlotView>().ValidMove = true;
                            GameObject go = Instantiate(PointerPrefab, new Vector3(x, y, -1), Quaternion.identity);
                            PointerList.Add(go);
                            goList.Clear();
                            Valid = true;
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        if (!Valid)
        {
            GameOver();
        }
    }
    public void CheckWhiteMove(int x, int y)
    {
        List<GameObject> goList = new List<GameObject>();
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8)
            {
                if (GameTable[x + i, y].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x + i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x + i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", true);
                                goList[j].GetComponent<PieceView>().WhiteColor = true;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1)
            {
                if (GameTable[x - i, y].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x - i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x - i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", true);
                                goList[j].GetComponent<PieceView>().WhiteColor = true;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + y < 8)
            {
                if (GameTable[x, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", true);
                                goList[j].GetComponent<PieceView>().WhiteColor = true;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (y - i > -1)
            {
                if (GameTable[x, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", true);
                                goList[j].GetComponent<PieceView>().WhiteColor = true;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8 && i + y < 8)
            {
                if (GameTable[x + i, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x + i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x + i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", true);
                                goList[j].GetComponent<PieceView>().WhiteColor = true;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8 && y - i > -1)
            {
                if (GameTable[x + i, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x + i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x + i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", true);
                                goList[j].GetComponent<PieceView>().WhiteColor = true;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1 && y - i > -1)
            {
                if (GameTable[x - i, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x - i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x - i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", true);
                                goList[j].GetComponent<PieceView>().WhiteColor = true;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1 && i + y < 8)
            {
                if (GameTable[x - i, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (!GameTable[x - i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (GameTable[x - i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", true);
                                goList[j].GetComponent<PieceView>().WhiteColor = true;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }

    }

    public void CheckBlackMove(int x, int y)
    {
        List<GameObject> goList = new List<GameObject>();
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8)
            {
                if (GameTable[x + i, y].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x + i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x + i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", false);
                                goList[j].GetComponent<PieceView>().WhiteColor = false;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1)
            {
                if (GameTable[x - i, y].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x - i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x - i, y].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", false);
                                goList[j].GetComponent<PieceView>().WhiteColor = false;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + y < 8)
            {
                if (GameTable[x, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", false);
                                goList[j].GetComponent<PieceView>().WhiteColor = false;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (y - i > -1)
            {
                if (GameTable[x, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", false);
                                goList[j].GetComponent<PieceView>().WhiteColor = false;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8 && i + y < 8)
            {
                if (GameTable[x + i, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x + i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x + i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", false);
                                goList[j].GetComponent<PieceView>().WhiteColor = false;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (i + x < 8 && y - i > -1)
            {
                if (GameTable[x + i, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x + i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x + i, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x + i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", false);
                                goList[j].GetComponent<PieceView>().WhiteColor = false;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1 && y - i > -1)
            {
                if (GameTable[x - i, y - i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x - i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y - i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x - i, y - i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", false);
                                goList[j].GetComponent<PieceView>().WhiteColor = false;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
        for (int i = 1; i < 8; i++)
        {
            if (x - i > -1 && i + y < 8)
            {
                if (GameTable[x - i, y + i].GetComponent<SlotView>().Piece != null)
                {
                    if (GameTable[x - i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        goList.Add(GameTable[x - i, y + i].GetComponent<SlotView>().Piece);
                    }
                    else if (!GameTable[x - i, y + i].GetComponent<SlotView>().Piece.GetComponent<PieceView>().WhiteColor)
                    {
                        if (goList.Count > 0)
                        {
                            for (int j = 0; j < goList.Count; j++)
                            {
                                goList[j].GetComponent<Animator>().SetBool("White", false);
                                goList[j].GetComponent<PieceView>().WhiteColor = false;
                            }
                            goList.Clear();
                            break;
                        }
                        else
                        {
                            goList.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    goList.Clear();
                    break;
                }
            }
            else
            {
                goList.Clear();
                break;
            }
        }
    }

    void GameOver()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameTable[i, j].GetComponent<SlotView>().ValidMove = false;
            }
        }
    }
    public void ChangePlayerStatus()
    {
        if (WhiteColor)
        {
            Pointer.GetComponent<Animator>().SetBool("White", false);
            WhiteColor = false;
        }
        else
        {
            Pointer.GetComponent<Animator>().SetBool("White", true);
            WhiteColor = true;
        }
    }
}
