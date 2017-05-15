using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for selecting the card itself on the button that is placed on the table.
/// </summary>
public class CardButton : MonoBehaviour {

    // Card that's associated to this object.
    [Header("Card Information")]
    public int health;
    [ReadOnly]
    public bool cardOnTable;
    [ReadOnly]
    public bool hasAttacked;

    // Internal
    [HideInInspector]
    public ICard associatedCard;
    [HideInInspector]
    public Player associatedPlayer;

    /// <summary>
    /// Sets this card as this button's card.
    /// </summary>
    public void SetCard(ICard card, Player player)
    {
        // Sets up the references.
        associatedCard = card;
        associatedPlayer = player;

        // Sets up the player health.
        health = card.ReturnHealth();
    }

    /// <summary>
    /// Checks that the card has been played and calls that card on the Turn Manager.
    /// </summary>
	public void PickedCard()
    {
        // Checks if the card in on the table or not in the table.
        if(!cardOnTable)
        {
            associatedCard.PlaceCard(this);
            cardOnTable = true;
        }
        else
        {
            // Checks if there's still not a selected card before selecting another one.
            if(RoundManager.roundManager.selectedCard == null && !hasAttacked)
            {
                RoundManager.roundManager.MarkSelected(this);
            }
            else if(RoundManager.roundManager.selectedCard != null) // Attacks another card.
            {
                RoundManager.roundManager.AttackedAnotherCard(this);
            }
        }
    }

    /// <summary>
    /// Called when a card gets damaged.
    /// </summary>
    public void GetDamage(int damage)
    {
        // Reduces health to the damage.
        health -= damage;

        // Updates the selected card's health.
        RoundManager.roundManager.canvasManager.UpdateHealth(this);

        // Destroys the card if the player has gone too far.
        if (health <= 0)
        {
            associatedPlayer.attack.Remove(this);
            Destroy(gameObject);
        }
    }
}
