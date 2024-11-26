using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFail : UICanvas
{
    public void RetryLevelButton()
    {
        Close(0);
        LevelManager.Instance.OnRetry();
    }
}
