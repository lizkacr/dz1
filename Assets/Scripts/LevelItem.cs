using UnityEngine;

public enum ItemType
{
    Damage,
    Score,
    Speed,
    Health
}

public class LevelItem : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private int _value = 1;
    [SerializeField] private float _speedMultiplier = 1.6f;
    [SerializeField] private float _duration = 5f;
    [SerializeField] private bool _destroyOnPickup = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Apply(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_type == ItemType.Damage)
        {
            Apply(other);
        }
    }

    private void Apply(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerController player))
        {
            return;
        }

        PlayerHealth health = player.GetComponent<PlayerHealth>();

        switch (_type)
        {
            case ItemType.Damage:
                health.TakeDamage(_value);
                break;

            case ItemType.Score:
                GameManager.Instance.AddScore(_value);
                break;

            case ItemType.Speed:
                player.ApplySpeedBoost(_speedMultiplier, _duration);
                break;

            case ItemType.Health:
                health.Heal(_value);
                break;
        }

        if (_destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}