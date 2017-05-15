using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour {

    public List<GameObject> cards;

    [Header("Prefabs")]
    public Transform galleryTransform;
    public GameObject cardPrefab;

    [Header("Layout")]
    public LayoutGroup layout;

    private bool swiping;
    private Vector2 lastPosition;


    /// <summary>
    /// Start is called just before any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        foreach(GameObject card in cards)
        {
            CreateCardButton(card.GetComponent<ICard>());
        }
    }

    /// <summary>
    /// Creates the card on the actual scene.
    /// </summary>
    public void CreateCardButton(ICard card)
    {
        // Creates a new card asset from the prefab.
        GameObject newCard = Instantiate(cardPrefab, galleryTransform);

        // Sets the card info.
        newCard.transform.GetChild(0).GetComponent<Text>().text = card.GetCardName();
        newCard.transform.GetChild(1).GetComponent<Text>().text = card.GetCardTooltip();
        newCard.transform.GetChild(2).GetComponent<Image>().sprite = card.GetCardSprite();
        newCard.transform.GetChild(3).GetComponentInChildren<Text>().text = card.ReturnHealth().ToString();
        newCard.transform.GetChild(4).GetComponentInChildren<Text>().text = card.ReturnAttack().ToString();
    }
}
