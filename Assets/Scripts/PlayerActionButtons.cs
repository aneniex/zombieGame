using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionButtons : MonoBehaviour
{
    public void OnAttackButtonPressed()
    {
        CustomCharacterController.Instance.Attack();
    }

    public void OnDodgeButtonPressed()
    {
        CustomCharacterController.Instance.Dodge();
    }
}
