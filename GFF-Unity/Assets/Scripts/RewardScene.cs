using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RewardScene : MonoBehaviour {

    public List<GameObject> cards;

    [Header("Prefabs")]
    public Transform canvasTransform;
    public GameObject cardPrefab;

    // Use this for initialization
    void Start () {
        
	}


    public void startReward()
    {
        int cardNum = Random.Range(0, cards.Count);
        CreateCardButton(cards[cardNum].GetComponent<ICard>());
    }
    /// <summary>
    /// Creates the card on the actual scene.
    /// </summary>
    public void CreateCardButton(ICard card)
    {
        // Creates a new card asset from the prefab.
        GameObject newCard = Instantiate(cardPrefab, canvasTransform);

        // Sets the card info.
        newCard.transform.GetChild(0).GetComponent<Text>().text = card.GetCardName();
        newCard.transform.GetChild(1).GetComponent<Text>().text = card.GetCardTooltip();
        newCard.transform.GetChild(2).GetComponent<Image>().sprite = card.GetCardSprite();
        newCard.transform.GetChild(3).GetComponentInChildren<Text>().text = card.ReturnHealth().ToString();
        newCard.transform.GetChild(4).GetComponentInChildren<Text>().text = card.ReturnAttack().ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
    