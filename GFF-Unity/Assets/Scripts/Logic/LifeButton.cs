using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for removing health from the player.
/// </summary>
public class LifeButton : MonoBehaviour {

    [Header("Button Properties")]
    public bool isAttacker;

    /// <summary>
    /// Announces that the life button has been picked.
    /// </summary>
	public void PickedLifeButton()
    {
        RoundManager.roundManager.AttackPlayer(this);
    }
}
