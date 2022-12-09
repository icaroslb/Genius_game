using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPauseButton : MonoBehaviour
{
    [SerializeField] private bool isOn;
    [SerializeField] private SpriteRenderer spRenderer;
    [SerializeField] private Sprite texOn;
    [SerializeField] private Sprite texOff;

    private void Start()
    {
        isOn = true;
        spRenderer = GetComponent<SpriteRenderer>();
    }

    public void GetOn ()
    {
        isOn = true;
        spRenderer.sprite = texOn;
    }

    public void GetOff ()
    {
        isOn = false;
        spRenderer.sprite = texOff;
    }

    private void OnMouseDown()
    {
        if (isOn)
        {
            GetOff();
            GameManager.Instance.Initiate();
        }
    }
}
