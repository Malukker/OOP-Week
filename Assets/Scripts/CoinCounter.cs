using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [ShowNonSerializedField] private int _totalAmount;
    private TextMeshProUGUI _text;

    void Start()
    {
        Coin.OnCoinPickup += UpdateCounter;
        _text = GetComponent<TextMeshProUGUI>();
        UpdateCounter(0);
    }

    void UpdateCounter(int value)
    {
        _totalAmount += value;
        _text.text = _totalAmount.ToString() + " coins";
    }

    private void OnDestroy()
    {
        Coin.OnCoinPickup -= UpdateCounter;
    }
}
