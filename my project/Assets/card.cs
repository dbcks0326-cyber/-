using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 10f; 
    public GameObject frontImage;
    public GameObject backImage;

    public Cardgame cardGame; 
    private TextMeshProUGUI text;

    public bool isFront = false;
    public bool ismatched = false;
    [HideInInspector] public int number;

    private void Update()
    {
        
        Quaternion targetRot = isFront ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotSpeed);

     
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
       
        if (ismatched || isFront) return;

    
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