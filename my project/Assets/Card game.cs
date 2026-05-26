using UnityEngine;
using System.Collections.Generic;

public class Cardgame : MonoBehaviour
{
    public List<card> cards = new List<card>();
    public List<Sprite> sprites = new List<Sprite>();

    public GameObject cardPrefab;
    public Transform cardParent;

    [Header("설정")]
    [Tooltip("원하는 페어(쌍)의 개수를 입력하세요. (예: 1 입력 시 카드 2장 생성)")]
    public int pairCount = 4; 

    public card firstCard = null;
    public card secondCard = null;
    private bool isChecking = false;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        Debug.Log($"StartGame 실행됨: {pairCount} 페어 생성 시작");

        Soundmanager.Instance.PlayBGM();
        
        foreach (card c in cards) { if (c != null) Destroy(c.gameObject); }
        cards.Clear();

        List<int> pairNumbers = GeneratePairNumbers(pairCount);

       
        int totalCardCount = pairCount * 2;

        for (int i = 0; i < totalCardCount; ++i)
        {
            GameObject obj = Instantiate(cardPrefab, cardParent);
            card newCard = obj.GetComponent<card>();

            newCard.cardGame = this;
            newCard.SetCardNumber(pairNumbers[i]);

            
            if (pairNumbers[i] < sprites.Count)
                newCard.Setimage(sprites[pairNumbers[i]]);

            newCard.Flip(true); 
            cards.Add(newCard);
        }

        Invoke("HideAllCards", 2.0f);
    }

    public void OnClickCard(card clickedCard)
    {
        if (isChecking || clickedCard == firstCard || clickedCard.ismatched) return;

        clickedCard.Flip(true);

        if (firstCard == null)
        {
            firstCard = clickedCard;
            Soundmanager.Instance.PlaySoundFx();

        }
        else
        {
            Soundmanager.Instance.PlaySoundFx();

            secondCard = clickedCard;
            isChecking = true;
            Invoke("CheckCard", 0.4f);
        }
    }

    void CheckCard()
    {
        if (firstCard.number == secondCard.number)
        {
            firstCard.ChangeColor(Color.red);
            secondCard.ChangeColor(Color.red);
            firstCard.ismatched = true;
            secondCard.ismatched = true;

            
            firstCard = null;
            secondCard = null;
            isChecking = false;
        }
        else
        {
            Invoke("HideCard", 0.4f);
            Soundmanager.Instance.PlaySoundFx();
        }
    }

    void HideCard()
    {
        if (firstCard != null) firstCard.Flip(false);
        if (secondCard != null) secondCard.Flip(false);

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    void HideAllCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null) cards[i].Flip(false);
        }
    }


    List<int> GeneratePairNumbers(int pairs)
    {
        List<int> newCardNumbers = new List<int>();

        
        for (int i = 0; i < pairs; ++i)
        {
            newCardNumbers.Add(i);
            newCardNumbers.Add(i);
        }

        
        for (int i = newCardNumbers.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            int temp = newCardNumbers[i];
            newCardNumbers[i] = newCardNumbers[rnd];
            newCardNumbers[rnd] = temp;
        }
        return newCardNumbers;
    }
}