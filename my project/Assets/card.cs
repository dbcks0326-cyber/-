using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 10f; // 회전 속도 (인스펙터에서 조절)
    public GameObject frontImage;
    public GameObject backImage;

    public Cardgame cardGame; // 매니저 참조
    private TextMeshProUGUI text;

    public bool isFront = false;
    public bool ismatched = false;
    [HideInInspector] public int number;

    private void Update()
    {
        // 1. 회전 로직: isFront 값에 따라 목표 각도로 Slerp
        // 0도(앞면), 180도(뒷면)
        Quaternion targetRot = isFront ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotSpeed);

        // 2. 각도에 따른 오브젝트 활성화/비활성화 (90도 기준)
        float yRotation = transform.eulerAngles.y;
        if (yRotation > 90f && yRotation < 270f)
        {
            frontImage.SetActive(false);
            backImage.SetActive(true);
        }
        else
        {
            frontImage.SetActive(true);
            backImage.SetActive(false);
        }
    }

    public void ClickCard()
    {
        // 이미 맞췄거나 앞면이면 클릭 방지
        if (ismatched || isFront) return;

        // 매니저에게 알림 (매니저가 Flip(true)을 호출해줄 것임)
        cardGame.OnClickCard(this);
    }

    public void Flip(bool state)
    {
        isFront = state;
    }

    public void SetCardNumber(int num)
    {
        if (text == null) text = GetComponentInChildren<TextMeshProUGUI>();
        number = num;
        if (text != null) text.text = num.ToString();
    }

    public void Setimage(Sprite sprite)
    {
        if (frontImage != null)
        {
            Image img = frontImage.GetComponent<Image>();
            if (img != null) img.sprite = sprite;
        }
    }

    public void ChangeColor(Color newColor)
    {
        if (frontImage != null)
        {
            Image img = frontImage.GetComponent<Image>();
            if (img != null) img.color = newColor;
        }
    }
}