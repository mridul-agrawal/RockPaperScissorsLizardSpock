using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class gets the Input from user during the game and according to the Round Result takes an action.
/// </summary>
public class InputManager : SingletonGeneric<InputManager>
{
    public static Action<int> OnInputSelection;
    public static Action RoundLost;
    public static Action RoundWon;
    public static Action RoundDrawn;

    // This method gets called when the player chooses an ObjectType to play. 
    public void GetPlayerInput(int playerType)
    {
        OnInputSelection?.Invoke(playerType);
    }

    // This method fetches the result of a round using EffectivenessMatrix class & Invokes the right delegate according to the result.
    public void CalculateResult(int playerType, ObjectType cpuObject)
    {
        ObjectType playerObject = (ObjectType)playerType;
        UIManager.Instance.ToggleInputButtons(false);

        int result = EffectivenessMatrix.GetResult(playerObject, cpuObject);

        switch (result)
        {
            case -1:
                RoundLost?.Invoke();
                break;
            case 1:
                RoundWon?.Invoke();
                break;
            case 0:
                RoundDrawn?.Invoke();
                break;
        }
    }
}
