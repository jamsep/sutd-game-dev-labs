using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    public void ButtonClick()
    {
        Debug.Log("Onclick pause button");
        GameManager.instance.GamePauseToggle();
    }
}
