using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    public void NextLevelButton() 
    {
        Close(0);
        LevelManager.Instance.OnNextLevel();
    }
    public void RetryLevelButton()
    {
        Close(0);
        LevelManager.Instance.OnRetry();
    }
}
