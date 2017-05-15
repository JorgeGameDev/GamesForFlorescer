using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the state of a round, such as the current turn and references to the player.
/// </summary>
public class RoundManager : MonoBehaviour {

    // Static reference to the round.
    public static RoundManager roundManager;
    public CanvasManager canvasManager;

    [Header("Turn Status")]
    [ReadOnly]
    public Turn currentTurn;

    [Header("Players")]
    public int startingCards;
    public Player currentPlayer;
    public Player defender;
    public Player attacker;

    [Header("Selection")]
    public ParticleSystem particles;
    [ReadOnly]
    public CardButton selectedCard;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Sets this as the singleton instance.
        if(roundManager == null)
        {
            roundManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // Rolls new card in the first round.
        RollFirstHand(ref defender);
        RollFirstHand(ref attacker);

        // Decides who wants the first turn.
        currentTurn = (Turn)UnityEngine.Random.Range(0, 2);
        EndTurn();
    }

    /// <summary>
    /// Rolls a first hand for each of the players.
    /// </summary>
    private void RollFirstHand(ref Player player)
    {
        // Gets the player object to fetch the cards from.
        List<ICard> startingHand = new List<ICard>();

        // Does a loop for the first card hand.
        for(int i = 0; i < startingCards; i++)
        {
            startingHand.Add(player.GetCard());
        }
        
        // Does a loop to create cards for each hand.
        foreach(ICard card in startingHand)
        {
            CardButton newHandCard = canvasManager.CreateCardButton(card, player);
            player.hand.Add(newHandCard);
        }
    }


    /// <summary>
    /// Used for attacking the player.
    /// </summary>
    public void AttackPlayer(LifeButton lifeButton)
    {
        if(selectedCard != null)
        {
            if(lifeButton.isAttacker && currentTurn == Turn.Defender)
            {
                selectedCard.hasAttacked = true;
                attacker.GetDamaged(selectedCard.associatedCard.ReturnAttack());
                selectedCard = null;
                canvasManager.cursor.ChangeCursor(0);

                // Plays Particles
                particles.transform.position = lifeButton.transform.position;
                particles.Play();
            }
            else if(!lifeButton.isAttacker && currentTurn == Turn.Attacker)
            {
                selectedCard.hasAttacked = true;
                defender.GetDamaged(selectedCard.associatedCard.ReturnAttack());
                selectedCard = null;
                canvasManager.cursor.ChangeCursor(0);

                // Plays Particles
                particles.transform.position = lifeButton.transform.position;
                particles.Play();
            }
        }
    }

    /// <summary>
    /// Rolls a new card for the player.
    /// </summary>
    public void RollNewCard(Turn turn)
    {
        Player player = currentTurn == Turn.Defender ? defender : attacker;
        // Rolls new cards if the player still has new cards.
        if(player.deck.Count > 0)
        {
            ICard card = player.GetCard();
            CardButton newHandCard = canvasManager.CreateCardButton(card, player);
            player.hand.Add(newHandCard);
        }
    }

    /// <summary>
    /// Play card and damage the opposing player
    /// </summary>
    public void PlaceCardOffense(CardButton button, ICard card)
    {
        if (currentTurn == Turn.Defender && button.associatedPlayer == defender)
        {
            if(defender.attack.Count < 5)
            {
                button.GetComponent<Animator>().runtimeAnimatorController = canvasManager.tableAnimator;
                defender.hand.Remove(button);
                defender.attack.Add(button);
                button.GetComponent<RectTransform>().SetParent(defender.attackTransform);
            }
        }
        else if (currentTurn == Turn.Attacker && button.associatedPlayer == attacker)
        {
            if (attacker.attack.Count < 5)
            {
                button.GetComponent<Animator>().runtimeAnimatorController = canvasManager.tableAnimator;
                attacker.hand.Remove(button);
                attacker.attack.Add(button);
                button.GetComponent<RectTransform>().SetParent(attacker.attackTransform);
            }
        }
    }

    /// <summary>
    /// Used for selecting a specific for attacking.
    /// </summary>
    public void MarkSelected(CardButton button)
    {
        // Checks if the player which selected the card can select that card.
        if (button.associatedPlayer == currentPlayer)
        {
            canvasManager.cursor.ChangeCursor(1);
            selectedCard = button;
        }
    }

    /// <summary>
    /// Used for cards to attack other cards.
    /// </summary>
    public void AttackedAnotherCard(CardButton button)
    {
        // Checks if the player is allowed to attack.
        if(button.associatedPlayer != currentPlayer)
        {
            // Makes the second card get attacked the amount of the first one.
            selectedCard.hasAttacked = true;
            selectedCard.associatedCard.PlayCard();
            canvasManager.cursor.ChangeCursor(0);
            particles.transform.position = button.transform.position;
            particles.Play();

            // Does damage to the first card.
            int attackDealt = selectedCard.associatedCard.ReturnAttack();
            int damageDealt = button.associatedCard.ReturnAttack();
            selectedCard.GetDamage(damageDealt);
            button.GetDamage(attackDealt);

            // Updates the selected card's health.
            selectedCard = null;
        }
    }

    /// <summary>
    /// Ends the current turn, if called by the player.
    /// </summary>
    public void EndTurn()
    {
        // Clears the selected card.
        selectedCard = null;
        canvasManager.cursor.ChangeCursor(0);

        // Checks whose turn to switch too.
        switch (currentTurn)
        {
            case Turn.Defender:
                attacker.ChangeTurn(true);
                defender.ChangeTurn(false);
                RollNewCard(currentTurn);
                canvasManager.EnableDecks(defender, false);
                canvasManager.EnableDecks(attacker, true);
                currentTurn = Turn.Attacker;
                currentPlayer = attacker;

                // Clears the cards that attacker has attacked with.
                foreach(CardButton button in attacker.attack)
                {
                    button.hasAttacked = false;
                }
                break;

            case Turn.Attacker:
                defender.ChangeTurn(true);
                attacker.ChangeTurn(false);
                RollNewCard(currentTurn);
                canvasManager.EnableDecks(defender, true);
                canvasManager.EnableDecks(attacker, false);
                currentTurn = Turn.Defender;
                currentPlayer = defender;

                // Clears the cards that defender has attacked with.
                foreach (CardButton button in defender.attack)
                {
                    button.hasAttacked = false;
                }
                break;
        }
    }

    /// <summary>
    /// Shows the end-game showing who won the current torn.
    /// </summary>
    public void EndGame()
    {
        String winner = currentTurn == Turn.Defender ? "O Defensor" : "O Atacante";
        canvasManager.ShowEndMessage(winner);

    }
}

/// <summary>
/// Enumerator defining the current turn.
/// </summary>
public enum Turn
{
    Defender,
    Attacker,
    NumberOfTypes
}
