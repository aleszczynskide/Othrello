using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    public GameLogic GameLogic;
    public GameObject Piece;
    public GameObject PiecePrefab;
    public int PositionX;
    public int PositionY;
    private void OnMouseDown()
    {
        if (GameLogic.WhiteColor)
        {
            if (Piece == null)
            {
                GameObject newPiece = Instantiate(PiecePrefab);
                newPiece.GetComponent<Animator>().SetBool("White", true);
                newPiece.GetComponent<PieceView>().WhiteColor = true;
                newPiece.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                Piece = newPiece;
                GameLogic.CheckWhiteMove(PositionX,PositionY);
                GameLogic.ChangePlayerStatus();
            }
        }
        else if (!GameLogic.WhiteColor)
        {
            if (Piece == null)
            {
                GameObject newPiece = Instantiate(PiecePrefab);
                newPiece.GetComponent<Animator>().SetBool("White", false);
                newPiece.GetComponent<PieceView>().WhiteColor = false;
                newPiece.transform.position = new Vector3(transform.position.x,transform.position.y,-1);
                Piece = newPiece;
                GameLogic.CheckBlackMove(PositionX, PositionY);
                GameLogic.ChangePlayerStatus();
            }
        }
    }
}
