using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void ExitGame()//hace que salga del juego
    {
        GameManager.instance.ExitGame();
    }


    public void CharacterSelection(int selection)
    {
        
        GameManager.instance.SelectCharacter(selection);

    }
}

