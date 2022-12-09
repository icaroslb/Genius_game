using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pontuation : MonoBehaviour, IObserverSubscriber
{
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.Subscribe(this);
    }

    public void Notify(int value)
    {
        text.text = $"Pontuação\n{value}";
    }
}
