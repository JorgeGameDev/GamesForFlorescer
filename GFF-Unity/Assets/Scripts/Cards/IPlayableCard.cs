using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface holding the functions for cards being played.
/// </summary>
public interface ICard
{
    // Gets the card information.
    string GetCardName();
    string GetCardTooltip();
    Sprite GetCardSprite();

    // Gets the card damage.
    int ReturnAttack();
    int ReturnHealth();
    void PlayCard();
    void PlaceCard(CardButton card);
}
