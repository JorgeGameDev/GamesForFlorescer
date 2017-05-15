using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Class for player
/// </summary>
public class Player : MonoBehaviour {

    [Header("Player Properties")]
    public int health = 25;

    [Header("Deck and Hand Cards")]
    public List<GameObject> deck;
    public List<CardButton> hand;
    public List<CardButton> attack;
    public RectTransform handTransform;
    public RectTransform attackTransform;

    [Header("Canvas Objects")]
    public Text healthText;
    public Text cardsLeft;

    [SerializeField]
    private bool myTurn;

	// Use this for initialization
	void Awake ()
    {
        healthText.text = "" + health;
        myTurn = false;
	}

    /// <summary>
    /// Gets a card from the deck to add to the player.
    /// </summary>
    /// <returns></returns>
    public ICard GetCard()
    {
        // Gets a card from the deck and returns it.
        int cardTake = Random.Range(0, deck.Count);
        ICard cardTaken = deck[cardTake].GetComponent<ICard>();
        deck.RemoveAt(cardTake);

        // Updates the card remaining count:
        cardsLeft.text = deck.Count.ToString();
        return cardTaken;
    }

    /// <summary>
    /// Called when a turn is changed.
    /// </summary>
    public void ChangeTurn(bool turn)
    {
        myTurn = turn;
    }

    /// <summary>
    /// Applies the damage to the player.
    /// </summary>
    public void GetDamaged(int damage)
    {
        // Used for directly damaging the player.
        health -= damage;
        healthText.text = "" + health;
        if(health <= 0)
        {
            healthText.text = "0";
            RoundManager.roundManager.EndGame();
        }
    }

    /// <summary>
    /// Self-Explanatory. Forces an for this End Game.
    /// </summary>
    public void Die()
    {
        RoundManager.roundManager.EndGame();
    }
}
