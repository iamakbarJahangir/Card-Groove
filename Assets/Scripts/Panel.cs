using UnityEngine;
using UnityEngine.UI;

public class SimplePanel : MonoBehaviour
{
    [Header("Panel Colors")]
    public Color emptyColor = Color.gray;
    public Color filledColor = Color.green;

    private Image panelImage;

    void Start()
    {
        panelImage = GetComponent<Image>();
        if (panelImage != null)
        {
            panelImage.color = emptyColor;
        }
    }

    void Update()
    {
        // Simple check - if has child, show filled color
        bool hasCard = transform.childCount > 0;

        if (panelImage != null)
        {
            panelImage.color = hasCard ? filledColor : emptyColor;
        }
    }
}