public interface IHealthSystem
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    void TakeDamage(float amount);
    void Die();
}
