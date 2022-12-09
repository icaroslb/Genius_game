public interface IObserverPublisher
{
    public void Subscribe(IObserverSubscriber subscriber);
    public void Unscribe(IObserverSubscriber subscriber);
    public void Notify();
}
