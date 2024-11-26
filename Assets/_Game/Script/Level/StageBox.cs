using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBox : MonoBehaviour
{
    public Stage stage;
    private List<ColorType> colorTypes = new List<ColorType>();

    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);

        if (character != null && !colorTypes.Contains(character.colorType))
        {
            colorTypes.Add(character.colorType);
            character.Stage = stage;
            stage.InitColor(character.colorType);
        }
    }
}
