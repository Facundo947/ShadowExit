public interface IPlayerHealthObserver
{
    void OnNotify(PlayerHealthNotification notification);
}

public enum PlayerHealthNotificationType
{
    //hasta que no agreguemos curaciones no hace falta tener los 3, con dos alcanza
    HealthChanged,
    Damaged,
    Died
}

public readonly struct PlayerHealthNotification
{
    //hacer un Onotify con todos datos para agilizar el testeo
    public PlayerHealthNotification(PlayerHealthNotificationType type, int currentHealth, int maxHealth)
    {
        Type = type;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
    }

    public PlayerHealthNotificationType Type { get; }
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
}
