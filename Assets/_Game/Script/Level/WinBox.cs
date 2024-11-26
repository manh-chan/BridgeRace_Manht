using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null)
        {
            LevelManager.Instance.OnFinishGame();
            if (character is Player)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.Victory);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.fail);
            }

            //character.ChangeAnim(Constants.ANIM_DANCE);

            character.TF.eulerAngles = Vector3.up * 180;
            character.OnInit();
        }
    }
}
