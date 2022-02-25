using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonGeneric<InputManager>
{
    public static Action<int> OnInputSelection;
    public static Action RoundLost;
    public static Action RoundWon;
    public static Action RoundDrawn;

    public void GetPlayerInput(int playerType)
    {
        OnInputSelection?.Invoke(playerType);
    }

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
