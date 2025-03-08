using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab della carta UI
    public Transform handTransform; // Dove posizionare le carte
    
    public List<CardData> cardsInHand = new List<CardData>();
    public int maxHandSize = 10;
    
    // Aggiunge una carta alla mano
    public void AddCard(CardData cardData)
    {
        if (cardsInHand.Count >= maxHandSize)
        {
            Debug.Log("Mano piena!");
            return;
        }
        
        cardsInHand.Add(cardData);
        
        // Crea l'oggetto carta visuale
        GameObject cardObject = Instantiate(cardPrefab, handTransform);
        Card cardComponent = cardObject.GetComponent<Card>();
        
        if (cardComponent != null)
        {
            cardComponent.Initialize(cardData);
        }
        
        // Riorganizza le carte nella mano
        ArrangeCards();
    }
    
    // Rimuove una carta dalla mano
    public void RemoveCard(Card card)
    {
        if (card.cardData != null && cardsInHand.Contains(card.cardData))
        {
            cardsInHand.Remove(card.cardData);
            Destroy(card.gameObject);
            
            // Riorganizza le carte nella mano
            ArrangeCards();
        }
    }
    
    // Dispone le carte in un arco
    void ArrangeCards()
    {
        int cardCount = handTransform.childCount;
        
        if (cardCount == 0) return;
        
        float cardWidth = 150f; // Larghezza stimata di una carta
        float totalWidth = cardWidth * cardCount;
        float radius = totalWidth * 0.8f;
        
        float angleStep = 10f; // Angolo tra le carte
        float startAngle = -angleStep * (cardCount - 1) / 2;
        
        for (int i = 0; i < cardCount; i++)
        {
            Transform card = handTransform.GetChild(i);
            
            float angle = startAngle + angleStep * i;
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius * 0.3f - radius * 0.3f;
            
            card.localPosition = new Vector3(x, y, 0);
            
            // Ruota leggermente le carte
            card.localRotation = Quaternion.Euler(0, 0, -angle);
        }
    }
    
    // Scarta tutta la mano
    public void DiscardHand()
    {
        for (int i = handTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(handTransform.GetChild(i).gameObject);
        }
        
        cardsInHand.Clear();
    }
}
