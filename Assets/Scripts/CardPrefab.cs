using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/Card", order = 1)]
public class CardPrefab : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite artwork;
    public UnityEvent cardEffect;
    public int numberOfCards;
}