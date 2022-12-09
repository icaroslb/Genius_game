using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IObserverPublisher
{
    [SerializeField] public static GameManager Instance;
    [SerializeField] public int points;
    [SerializeField] public float timeCorrect;
    [SerializeField] public float timeWrong;

    [SerializeField] public List<IObserverSubscriber> subscribers;

    [SerializeField] private float velocity;
    [SerializeField] private int nextColor;
    [SerializeField] private bool isPlaying;

    [SerializeField] private ColorObject[] colors;
    [SerializeField] private List<int> colorOrder;
    [SerializeField] private PlayPauseButton playPause;

    public void Subscribe(IObserverSubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void Unscribe(IObserverSubscriber subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void Notify()
    {
        foreach (var s in subscribers)
        {
            s.Notify(points);
        }
    }

    private void Awake()
    {
        Instance = this;
        colorOrder = new List<int>();
        subscribers = new List<IObserverSubscriber>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        colors = FindObjectsOfType<ColorObject>();

        velocity = 0.5f;
        timeCorrect = 1.0f;
        timeWrong = 0.2f;

        points = 0;

        for (int i = 0; i < colors.Length; i++)
            colors[i].color = i;

        isPlaying = false;
    }

    public void Initiate()
    {
        if (colorOrder.Count == 0)
        {
            for (int i = 0; i < 3; i++)
                AddNewColor();
        }

        StartCoroutine(PlayColors());
        isPlaying = true;
    }

    public void Pause(float timeWait)
    {
        isPlaying = false;
        StartCoroutine(ReloadTime(timeWait + 0.1f));
    }

    public void End(float timeWait)
    {
        Pause(timeWait);
        colorOrder.Clear();
    }

    private IEnumerator ReloadTime(float timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        playPause.GetOn();
    }

    public void Wait(float timeWait)
    {
        StartCoroutine(WaitTime(timeWait));
    }

    private IEnumerator WaitTime(float timeWait)
    {
        isPlaying = false;
        yield return new WaitForSeconds(timeWait);
        isPlaying = true;
    }

    public int IsNextColor (int color)
    {
        if (isPlaying)
        {
            if (colorOrder[nextColor] == color)
            {
                nextColor += 1;
                
                if (nextColor >= colorOrder.Count)
                {
                    nextColor = 0;
                    AddNewColor();
                    Pause(timeCorrect);

                    AddPoints();
                }
                return 1;
            }
            else
            {
                End(timeWrong * 3);

                ClearPoints();
                return -1;
            }
        }
        else
        { return 0; }
    }

    private void AddNewColor ()
    {
        colorOrder.Add(Random.Range(0, colors.Length));
    }

    private IEnumerator PlayColors ()
    {
        foreach(var c in colorOrder)
        {
            colors[c].Play(velocity, 1);
            yield return new WaitForSeconds(velocity + 0.1f);
        }
    }

    private void ClearPoints ()
    {
        points = 0;
        Notify();
    }

    private void AddPoints ()
    {
        points = points + 1;
        Notify();
    }
}
