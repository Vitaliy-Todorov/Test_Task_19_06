using UnityEngine;

namespace Entities
{
    public abstract class AEntity : MonoBehaviour
    {
        public string ID { get; private set; }
        public abstract void DestroyEntity();
    }
}