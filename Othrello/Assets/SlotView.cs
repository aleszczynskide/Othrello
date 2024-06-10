using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    public GameLogic GameLogic;
    public GameObject Piece;
    public GameObject PiecePrefab;
    public int PositionX;
    public int PositionY;
    internal bool ValidMove;
    private void OnMouseDown()
    {
        if (ValidMove)
        {
            if (GameLogic.WhiteColor)
            {
                if (Piece == null)
                {
                    GameObject newPiece = Instantiate(PiecePrefab);
                    Piece = newPiece;
                    newPiece.GetComponent<Animator>().SetBool("White", true);
                    newPiece.GetComponent<PieceView>().WhiteColor = true;
                    newPiece.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                    GameLogic.CheckWhiteMove(PositionX, PositionY);
                    GameLogic.ChangePlayerStatus();
                    GameLogic.CheckForValidMove();
                }
            }
            else if (!GameLogic.WhiteColor)
            {
                if (Piece == null)
                {
                    GameObject newPiece = Instantiate(PiecePrefab);
                    Piece = newPiece;
                    newPiece.GetComponent<Animator>().SetBool("White", false);
                    newPiece.GetComponent<PieceView>().WhiteColor = false;
                    newPiece.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                    GameLogic.CheckBlackMove(PositionX, PositionY);
                    GameLogic.ChangePlayerStatus();
                    GameLogic.CheckForValidMove();
                }
            }
            GameLogic.Count();
        }
    }
}
