using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Hand playerHand;
    
    // Riferimenti alle carte (dovrai creare questi ScriptableObjects)
    public CardData[] testCards;
    
    void Start()
    {
        // Test: aggiungi alcune carte alla mano
        DrawStartingHand();
    }
    
    void DrawStartingHand()
    {
        // Aggiungi 5 carte casuali dalla collection di test
        for (int i = 0; i < 5; i++)
        {
            if (testCards.Length > 0)
            {
                int randomIndex = Random.Range(0, testCards.Length);
                playerHand.AddCard(testCards[randomIndex]);
            }
        }
    }
    
    // Metodo da richiamare da un bottone UI per testare l'aggiunta di carte
    public void TestDrawCard()
    {
        if (testCards.Length > 0)
        {
            int randomIndex = Random.Range(0, testCards.Length);
            playerHand.AddCard(testCards[randomIndex]);
        }
    }
}
