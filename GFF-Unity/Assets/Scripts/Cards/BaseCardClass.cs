using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

/// <summary>
/// Card Class that holds information regarding a specific card.
/// </summary>
public class BaseCardClass : MonoBehaviour, ICard {

    [Header("Card Information")]
    public int attack, health;

    [Header("Card Display")]
    public string cardName;
    [TextArea]
    public string toolTip;
    public Sprite portrait;

    /// <summary>
    /// Called when a card is played.
    /// </summary>
    public void PlayCard()
    {
       // Used for playing a card against another.
    }

    /// <summary>
    /// Used to place a card in the table, before playing it.
    /// </summary>
    public void PlaceCard(CardButton card)
    {
        RoundManager.roundManager.PlaceCardOffense(card, this);
    }

    /// <summary>
    /// Returns the card name.
    /// </summary>
    public string GetCardName()
    {
        return cardName;
    }

    /// <summary>
    /// Return the card tool-tip.
    /// </summary>
    public string GetCardTooltip()
    {
        return toolTip;
    }

    /// <summary>
    /// Returns the card portrait.
    /// </summary>
    /// <returns></returns>
    public Sprite GetCardSprite()
    {
        return portrait;
    }

    /// <summary>
    /// Returns the damage that this card deals.
    /// </summary>
    public int ReturnAttack()
    {
        return attack;
    }

    /// <summary>
    /// Returns the health that this card has.
    /// </summary>
    public int ReturnHealth()
    {
        return health;
    }
}
