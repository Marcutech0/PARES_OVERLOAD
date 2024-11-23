using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText; 
    public TextMeshProUGUI coinsText; 

    public TextMeshProUGUI FinalcoinsText;
    public TextMeshProUGUI FinalpointsText;

    private int totalPoints = 0; 
    private int coinsCollected = 0; 

    void Start()
    {
        UpdateUI();
    }

    public void AddPoints(int points)
    {
        totalPoints += points;
        UpdateUI(); 
    }

    public void CollectCoin()
    {
        coinsCollected++;
        AddPoints(1000); 
        UpdateUI(); 
    }

    private void UpdateUI()
    {
        pointsText.text = $"POINTS: {totalPoints}";
        FinalpointsText.text = $"YOUR POINTS: {totalPoints}";
        coinsText.text = $": {coinsCollected}";
        FinalcoinsText.text = $"COLLECTED: {coinsCollected}";
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }

    public int GetCoinsCollected()
    {
        return coinsCollected;
    }
}
