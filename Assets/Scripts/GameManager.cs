using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Left Panel UI")]
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI spEffectText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI staminaText;

    [Header("Top Panel UI")]
    public TextMeshProUGUI multiplierText;
    public Button[] arrows; // 4 arrows

    [Header("Right Panel UI")]
    public Button rButton; // Play Cards Button
    public Button bButton; // End Turn Button
    public Transform[] panels; // 3 panels

    [Header("Center Text")]
    public TextMeshProUGUI centerText;

    [Header("Game State")]
    public int currentScore = 0;
    public int targetScore = 300;
    public int currentStamina = 10;
    public int maxStamina = 10;
    public float currentMultiplier = 1.0f;
    public string currentObjective = "Reach 300 Hype Points";
    public string currentSPEffect = "No Special Effects";

    private int cardsInPanels = 0;
    private bool gameWon = false;
    private bool rhythmGameActive = false;

    void Start()
    {
        InitializeGame();
        SetupButtons();
    }

    void InitializeGame()
    {
        UpdateUI();
        ShowTemporaryMessage("Game Started! Select cards and play!");
    }

    void SetupButtons()
    {
        rButton.onClick.AddListener(PlayCards);
        bButton.onClick.AddListener(EndTurn);

        // Arrow functions
        for (int i = 0; i < arrows.Length; i++)
        {
            int index = i;
            arrows[i].onClick.AddListener(() => OnArrowClicked(index));
        }
    }

    public void MoveCardToPanel(GameObject card, int cardValue)
    {
        if (panels == null || panels.Length == 0)
        {
            Debug.LogError("Panels array not assigned!");
            return;
        }

        if (cardsInPanels < 3 && cardsInPanels < panels.Length)
        {
            Transform targetPanel = panels[cardsInPanels];

            if (targetPanel != null)
            {
                card.transform.SetParent(targetPanel);
                card.transform.localPosition = Vector3.zero;

                cardsInPanels++;

                Debug.Log("Card moved to panel. Cards in panels: " + cardsInPanels);
                ShowTemporaryMessage($"Cards selected: {cardsInPanels}/3");
                UpdateUI();
            }
        }
        else
        {
            ShowTemporaryMessage("Panels full! Play cards or end turn.");
        }
    }

    void PlayCards()
    {
        if (rhythmGameActive) return;

        if (cardsInPanels > 0 && currentStamina >= cardsInPanels)
        {
            StartRhythmGame();
        }
        else if (cardsInPanels == 0)
        {
            ShowTemporaryMessage("No cards selected!");
        }
        else
        {
            ShowTemporaryMessage("Not enough stamina!");
        }
    }

    void StartRhythmGame()
    {
        rhythmGameActive = true;
        ShowTemporaryMessage("🎵 Rhythm Game Starting... 🎵");

        // Disable buttons during rhythm
        rButton.interactable = false;
        bButton.interactable = false;
        foreach (Button arrow in arrows) arrow.interactable = false;

        Invoke("CompleteRhythmGame", 3f);
    }

    void CompleteRhythmGame()
    {
        rhythmGameActive = false;

        // Re-enable buttons
        rButton.interactable = true;
        bButton.interactable = true;
        foreach (Button arrow in arrows) arrow.interactable = true;

        // Calculate performance with different card effects
        ApplyCardEffects();

        // Clear panels
        ClearAllPanels();
        cardsInPanels = 0;

        // Check win condition
        CheckWinCondition();

        UpdateUI();
    }

    void ApplyCardEffects()
    {
        float rhythmMultiplier = Random.Range(0.8f, 1.5f);
        float totalMultiplier = currentMultiplier * rhythmMultiplier;

        int totalScore = 0;
        int cardsPlayed = cardsInPanels;
        int totalStaminaCost = cardsPlayed; // Simple: 1 stamina per card

        // Different card effects based on number and type
        for (int i = 0; i < cardsPlayed; i++)
        {
            int cardType = Random.Range(0, 4); // Random card type
            int baseScore = 50;

            switch (cardType)
            {
                case 0: // Attack Card
                    int attackScore = Mathf.RoundToInt(baseScore * totalMultiplier);
                    totalScore += attackScore;
                    Debug.Log($"Attack Card: +{attackScore} damage");
                    break;

                case 1: // Defense Card
                    int defenseScore = Mathf.RoundToInt(baseScore * 0.7f * totalMultiplier);
                    totalScore += defenseScore;
                    currentStamina += 1; // Bonus stamina
                    Debug.Log($"Defense Card: +{defenseScore} score, +1 stamina");
                    break;

                case 2: // Buff Card
                    int buffScore = Mathf.RoundToInt(baseScore * 0.8f * totalMultiplier);
                    totalScore += buffScore;
                    currentMultiplier += 0.2f; // Permanent multiplier boost
                    currentSPEffect = "Buff Active: +0.2x multiplier!";
                    Debug.Log($"Buff Card: +{buffScore} score, multiplier boosted");
                    break;

                case 3: // Taunt Card
                    int tauntScore = Mathf.RoundToInt(baseScore * 1.3f * totalMultiplier);
                    totalScore += tauntScore;
                    // Taunt effect - extra damage to opponent (in single player, just bonus score)
                    int tauntBonus = Mathf.RoundToInt(20 * totalMultiplier);
                    totalScore += tauntBonus;
                    currentSPEffect = "Taunt Active: Dealing extra damage!";
                    Debug.Log($"Taunt Card: +{tauntScore} base + {tauntBonus} taunt damage");
                    break;
            }
        }

        // Apply effects
        currentScore += totalScore;
        currentStamina -= totalStaminaCost;
        currentStamina += 1; // Turn end bonus

        ShowTemporaryMessage($"Earned {totalScore} points! ({cardsPlayed} cards x {totalMultiplier:F1})");

        // Reset temporary effects after some time
        if (currentSPEffect.Contains("Taunt") || currentSPEffect.Contains("Buff"))
        {
            CancelInvoke("ResetSPEffect");
            Invoke("ResetSPEffect", 5f);
        }
    }

    void EndTurn()
    {
        if (rhythmGameActive) return;

        // Restore stamina
        currentStamina = Mathf.Min(currentStamina + 3, maxStamina);

        // Clear panels
        ClearAllPanels();
        cardsInPanels = 0;

        // Reactivate all cards
        ReactivateAllCards();

        ShowTemporaryMessage("Turn ended. Stamina restored!");
        UpdateUI();
    }

    // ARROW FUNCTIONS - Multiplier Control
    void OnArrowClicked(int arrowIndex)
    {
        if (rhythmGameActive) return;

        switch (arrowIndex)
        {
            case 0: // Left Arrow - Decrease Multiplier
                if (currentMultiplier > 0.5f)
                {
                    currentMultiplier -= 0.1f;
                    ShowTemporaryMessage("Multiplier decreased!");
                }
                else
                {
                    ShowTemporaryMessage("Minimum multiplier reached!");
                }
                break;

            case 1: // Right Arrow - Increase Multiplier (costs stamina)
                if (currentStamina >= 1)
                {
                    currentStamina -= 1;
                    currentMultiplier += 0.2f;
                    ShowTemporaryMessage("Multiplier increased! (-1 stamina)");
                }
                else
                {
                    ShowTemporaryMessage("Need 1 stamina to boost multiplier!");
                }
                break;

            case 2: // Up Arrow - Special Power (costs more stamina)
                if (currentStamina >= 2)
                {
                    currentStamina -= 2;
                    currentMultiplier += 0.5f;
                    currentSPEffect = "Power Boost: +0.5x multiplier!";
                    ShowTemporaryMessage("Power Boost Applied! (-2 stamina)");

                    CancelInvoke("ResetSPEffect");
                    Invoke("ResetSPEffect", 3f);
                }
                else
                {
                    ShowTemporaryMessage("Need 2 stamina for power boost!");
                }
                break;

            case 3: // Down Arrow - Reset & Heal
                currentMultiplier = 1.0f;
                currentStamina = Mathf.Min(currentStamina + 2, maxStamina);
                currentSPEffect = "Reset: Multiplier reset, +2 stamina";
                ShowTemporaryMessage("Reset applied! +2 stamina");

                CancelInvoke("ResetSPEffect");
                Invoke("ResetSPEffect", 2f);
                break;
        }

        UpdateUI();
    }

    void ClearAllPanels()
    {
        if (panels != null)
        {
            foreach (Transform panel in panels)
            {
                if (panel != null)
                {
                    foreach (Transform child in panel)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    void ReactivateAllCards()
    {
        SimpleCard[] allCards = FindObjectsOfType<SimpleCard>(true);
        foreach (SimpleCard card in allCards)
        {
            card.gameObject.SetActive(true);
            card.transform.SetParent(card.originalParent);
            card.transform.localPosition = card.originalPosition;
            card.ResetCard();
        }
    }

    void CheckWinCondition()
    {
        if (currentScore >= targetScore)
        {
            gameWon = true;
            ShowTemporaryMessage("🎉 YOU WON! Objective Complete! 🎉");
            currentObjective = "COMPLETED!";
            rButton.interactable = false;
        }
        else if (currentStamina <= 0 && cardsInPanels == 0)
        {
            ShowTemporaryMessage("Game Over! No stamina left.");
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore + "/" + targetScore;
        staminaText.text = "Stamina: " + currentStamina + "/" + maxStamina;
        multiplierText.text = "x" + currentMultiplier.ToString("F1");
        objectiveText.text = currentObjective;
        spEffectText.text = currentSPEffect;
    }

    void ResetSPEffect()
    {
        currentSPEffect = "No Special Effects";
        UpdateUI();
    }

    void ShowTemporaryMessage(string message)
    {
        centerText.gameObject.SetActive(true);
        centerText.text = message;
        StartCoroutine(HideCenterTextAfterDelay(2f));
    }

    IEnumerator HideCenterTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        centerText.gameObject.SetActive(false);
    }
}