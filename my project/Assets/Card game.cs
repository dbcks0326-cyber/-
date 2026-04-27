using UnityEngine;
using System.Collections.Generic;

public class Cardgame : MonoBehaviour
{
    public List<card> cards = new List<card>();
    public List<Sprite> sprites = new List<Sprite>();

    public GameObject cardPrefab;
    public Transform cardParent;
    public int cardCount = 8;

    public card firstCard = null;
    public card secondCard = null;
    private bool isChecking = false;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        Debug.Log("StartGame 실행됨");
        cards.Clear();

        List<int> pairNumbers = GeneratePairNumbers(cardCount);

        for (int i = 0; i < cardCount; ++i)
        {
            GameObject obj = Instantiate(cardPrefab, cardParent);
            card newCard = obj.GetComponent<card>();

            newCard.cardGame = this;
            newCard.SetCardNumber(pairNumbers[i]);

            // 이미지 설정 (sprites 리스트 범위 체크)
            if (pairNumbers[i] < sprites.Count)
                newCard.Setimage(sprites[pairNumbers[i]]);

            // 처음엔 앞면을 보여줬다가 잠시 후 뒤집음
            newCard.Flip(true);
            cards.Add(newCard);
        }

        Invoke("HideAllCards", 2.0f); // 처음에 기억할 시간 2초로 늘림
    }

    public void OnClickCard(card clickedCard)
    {
        // 체크 중이거나 이미 선택한 카드를 또 누르면 무시
        if (isChecking || clickedCard == firstCard) return;

        clickedCard.Flip(true); // 카드 뒤집기

        if (firstCard == null)
        {
            firstCard = clickedCard;
        }
        else
        {
            secondCard = clickedCard;
            isChecking = true; // 두 장 다 뒤집었으니 체크 시작
            Invoke("CheckCard", 0.4f); // 뒤집히는 애니메이션 시간 확보
        }
    }

    void CheckCard()
    {
        if (firstCard.number == secondCard.number)
        {
            // 짝이 맞을 때
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
            // 짝이 틀릴 때 -> 1초 뒤에 다시 뒤집기
            Invoke("HideCard", 0.4f);
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
            cards[i].Flip(false);
        }
    }

    List<int> GeneratePairNumbers(int cardCount)
    {
        int pairCount = cardCount / 2;
        List<int> newCardNumbers = new List<int>();

        for (int i = 0; i < pairCount; ++i)
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