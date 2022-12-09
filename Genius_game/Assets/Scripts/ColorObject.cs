using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    [SerializeField] public int color;

    [SerializeField] private SpriteRenderer spRenderer;
    [SerializeField] private Sprite texOn;
    [SerializeField] private Sprite texOff;

    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        spRenderer.sprite = texOff;
    }

    private void OnMouseDown()
    {
        int response = GameManager.Instance.IsNextColor(color);
        if (response == 1)
        {
            Play(GameManager.Instance.timeCorrect, 1);
        }
        else if (response == -1)
        {
            Play(GameManager.Instance.timeWrong, 3);
        }
    }

    public void Play(float waitTime, int times)
    {
        StartCoroutine(PlayColor(waitTime, times));
    }

    private IEnumerator PlayColor(float waitTime, int times)
    {
        for (int i = 0; i < times; i++)
        {
            spRenderer.sprite = texOn;
            yield return new WaitForSeconds(waitTime);
            spRenderer.sprite = texOff;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
