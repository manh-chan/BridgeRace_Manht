using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Stage stage;
    private List<ColorType> colorTypes = new List<ColorType>();

    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);

        if (character != null)
        {
            stage.ClearBrickColor(character.colorType);
        }
    }
}
