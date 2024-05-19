using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    public GameLogic GameLogic;


    private void OnMouseDown()
    {
        if (GameLogic.CurrentPlayer == 1)
        {
            Destroy(gameObject);
        }
        else if (GameLogic.CurrentPlayer == 2)
        {

        }
        GameLogic.ChangePlayerStatus();
    }
}
