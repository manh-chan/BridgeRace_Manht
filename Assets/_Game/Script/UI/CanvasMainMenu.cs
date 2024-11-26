using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void StartButton(){
        Close(0);
        GameManager.Instance.UpdateGameState(GameManager.GameState.PlayGame);
    }
}
