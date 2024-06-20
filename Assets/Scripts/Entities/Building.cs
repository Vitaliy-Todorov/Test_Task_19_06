using UnityEngine;

namespace Entities
{
    public class Building : MonoBehaviour
    {
        [field: SerializeField] public int Level { get; private set; } = 1;
        [field: SerializeField] public MoveBuilding MoveBuilding { get; private set; }

        public void Stack(Building building)
        {
            Level += building.Level;
            Destroy(building.gameObject);
        }
    }
}