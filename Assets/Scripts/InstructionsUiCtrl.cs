using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsUiCtrl : MonoBehaviour
{
    public UICtrl mainUiCtrl;

    private void OnEnable()
    {
        mainUiCtrl.OnEnableInstructions();
    }
}
