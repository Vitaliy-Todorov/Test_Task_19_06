using UnityEngine;

namespace Entities.HP
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private float _value;
        [SerializeField] private float _maxValue;

        public void DoDamage(float damage)
        {
            _value -= damage;
            if(_value <= 0)
                Destroy(gameObject);
        }

        public void Regeneration() => 
            _value = _maxValue;
    }
}
