using UnityEngine;
using UnityEngine.UI;

public class SimpleCard : MonoBehaviour
{
    [Header("Card Stats")]
    public int cardValue = 50;
    public int staminaCost = 1;

    private Button cardButton;
    private GameManager gameManager;
    public Transform originalParent;
    public Vector3 originalPosition;
    private bool isUsed = false;

    void Start()
    {
        cardButton = GetComponent<Button>();
        gameManager = FindObjectOfType<GameManager>();

        // Store original position
        originalParent = transform.parent;
        originalPosition = transform.localPosition;

        if (cardButton != null)
        {
            cardButton.onClick.AddListener(OnCardClicked);
        }

        // Set random card values
        cardValue = Random.Range(30, 100);
        staminaCost = Random.Range(1, 3);
    }

    void OnCardClicked()
    {
        if (!isUsed && gameManager.currentStamina >= staminaCost)
        {
            Debug.Log($"{gameObject.name} played for {cardValue} points! Cost: {staminaCost} stamina");
            gameManager.MoveCardToPanel(gameObject, cardValue);
            isUsed = true;
        }
        else if (gameManager.currentStamina < staminaCost)
        {
            Debug.Log("Not enough stamina!");
        }
    }

    public void ResetCard()
    {
        isUsed = false;
    }
}