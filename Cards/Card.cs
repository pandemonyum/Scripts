using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public CardData cardData;
    
    [Header("Card UI Elements")]
    public Image artworkImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public Image cardFrame; // Background/frame dell'immagine
    
    [Header("Card Behavior")]
    private bool isDragging;
    private Vector3 startPosition;
    private Transform originalParent;
    
    // Colori per i diversi tipi di carte
    private Color attackColor = new Color(0.8f, 0.2f, 0.2f);
    private Color skillColor = new Color(0.2f, 0.5f, 0.8f);
    private Color powerColor = new Color(0.6f, 0.2f, 0.8f);
    
    void Start()
    {
        if (cardData != null)
        {
            UpdateCardVisuals();
        }
    }
    
    public void Initialize(CardData data)
    {
        cardData = data;
        UpdateCardVisuals();
    }
    
    void UpdateCardVisuals()
    {
        // Aggiorna tutti gli elementi visivi della carta
        nameText.text = cardData.cardName;
        descriptionText.text = cardData.description;
        costText.text = cardData.energyCost.ToString();
        
        if (cardData.artwork != null)
        {
            artworkImage.sprite = cardData.artwork;
        }
        
        // Imposta il colore in base al tipo di carta
        switch (cardData.cardType)
        {
            case CardData.CardType.Attack:
                cardFrame.color = attackColor;
                break;
            case CardData.CardType.Skill:
                cardFrame.color = skillColor;
                break;
            case CardData.CardType.Power:
                cardFrame.color = powerColor;
                break;
        }
    }
    
    // Gestione degli eventi di input per trascinare le carte
    public void OnBeginDrag()
    {
        isDragging = true;
        startPosition = transform.position;
        originalParent = transform.parent;
        
        // Sposta la carta in primo piano mentre viene trascinata
        transform.SetParent(transform.root);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    
    public void OnDrag(Vector2 position)
    {
        if (isDragging)
        {
            transform.position = position;
        }
    }
    
    public void OnEndDrag()
    {
        isDragging = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        // Implementare qui la logica per giocare la carta o riportarla alla mano
        // Per ora, torniamo semplicemente alla posizione iniziale
        transform.position = startPosition;
        transform.SetParent(originalParent);
    }
    
    // Metodo per giocare la carta
    public void PlayCard()
    {
        // Qui implementeremo l'effetto della carta quando viene giocata
        Debug.Log("Giocata carta: " + cardData.cardName);
        
        // Esempio semplificato dell'effetto della carta
        switch (cardData.cardType)
        {
            case CardData.CardType.Attack:
                // Infligge danno al bersaglio
                Debug.Log("Infligge " + cardData.damage + " danno");
                break;
                
            case CardData.CardType.Skill:
                // Aggiunge blocco o altri effetti
                if (cardData.block > 0)
                {
                    Debug.Log("Aggiunge " + cardData.block + " blocco");
                }
                break;
                
            case CardData.CardType.Power:
                // Aggiunge un effetto permanente
                Debug.Log("Attiva potere: " + cardData.description);
                break;
        }
        
        // Dopo aver giocato la carta, la rimuoviamo
        Destroy(gameObject);
    }
}