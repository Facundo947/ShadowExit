public interface IPlayerHealthSubject
{
    void RegisterObserver(IPlayerHealthObserver observer);
    void UnregisterObserver(IPlayerHealthObserver observer);
    void NotifyObservers(PlayerHealthNotification notification);
}
