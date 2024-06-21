using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class MoveAlongTrajectory : MonoBehaviour
    {
        [field: SerializeField] private float _speed;
        [field: SerializeField] private List<Transform> _path;

        private int _currenPoint;

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _path[_currenPoint].position - transform.position);
            transform.position = Vector2.MoveTowards(transform.position, _path[_currenPoint].position, _speed * Time.fixedDeltaTime);

            if (_path[_currenPoint].position == transform.position && _currenPoint < _path.Count)
                _currenPoint++;
        }
    }
}
