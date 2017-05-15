using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used for everything related to the canvas and spawning cards and things like that.
/// </summary>
public class CanvasManager : MonoBehaviour {

    [Header("External References")]
    public GameCursor cursor;

    [Header("UI elements")]
    public GameObject gameOver;
    public Text gameOverText;

    [Header("Card Frames")]
    public Sprite goldFrame;
    public Sprite baseFrame;

    [Header("Prefabs")]
    public GameObject cardPrefab;
    public RuntimeAnimatorController tableAnimator;

    /// <summary>
    /// Creates the card on the actual scene.
    /// </summary>
    public CardButton CreateCardButton(ICard card, Player player)
    {
        // Creates a new card asset from the prefab.
        GameObject newCard = Instantiate(cardPrefab, player.handTransform);

        // Sets the card info.
        newCard.transform.GetChild(0).GetComponent<Text>().text = card.GetCardName();
        newCard.transform.GetChild(1).GetComponent<Text>().text = card.GetCardTooltip();
        newCard.transform.GetChild(2).GetComponent<Image>().sprite = card.GetCardSprite();
        newCard.transform.GetChild(3).GetComponentInChildren<Text>().text = card.ReturnHealth().ToString();
        newCard.transform.GetChild(4).GetComponentInChildren<Text>().text = card.ReturnAttack().ToString();

        // Associates the button with the card.
        CardButton newCardButton = newCard.GetComponent<CardButton>();
        newCardButton.SetCard(card, player);
        return newCardButton;
    }

    /// <summary>
    /// Used for updating the health of a specific button.
    /// </summary>
    public void UpdateHealth(CardButton button)
    {
        button.transform.GetChild(3).GetComponentInChildren<Text>().text = button.health.ToString();
    }

    /// <summary>
    /// Enables/Disables the decks of a specific player.
    /// </summary>
    public void EnableDecks(Player player, bool enable)
    {
        // Changes frames depending on the player.
        foreach (CardButton button in player.hand)
        {
            if(enable)
            {
                button.GetComponent<Image>().sprite = goldFrame;
            }
            else
            {
                button.GetComponent<Image>().sprite = baseFrame;
            }
        }

        // Changes frames depending on the player.
        foreach(CardButton button in player.attack)
        {
            if (enable)
            {
                button.GetComponent<Image>().sprite = goldFrame;
            }
            else
            {
                button.GetComponent<Image>().sprite = baseFrame;
            }
        }
    }

    /// <summary>
    /// Shows the end message with the winner.
    /// </summary>
    public void ShowEndMessage(string winner)
    {
        gameOver.SetActive(true);
        gameOverText.text = winner.ToString() + " venceu!";
    }

}
