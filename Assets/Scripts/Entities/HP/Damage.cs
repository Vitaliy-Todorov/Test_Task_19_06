using UnityEngine;

namespace Entities.HP
{
    public class Damage : MonoBehaviour
    {
        [SerializeField] private float _value;

        private void OnTriggerEnter2D(Collider2D col)
        {
            Health health = col.gameObject.GetComponent<Health>();
            
            if (health)
                health.DoDamage(_value);
        }
    }
}
