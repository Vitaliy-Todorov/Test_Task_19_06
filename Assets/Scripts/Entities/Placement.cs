using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Entities
{
    public class Placement
    {
        private Building _building;
        private int _layerMask;

        public Placement()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Building");
            Update().Forget();
        }

        private async UniTask Update()
        {
            while (true)
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
                
                if (_building && _building.Placement())
                {
                    _building = null;
                    continue;
                }
                
                RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, _layerMask);
                if (hit)
                {
                    _building = hit.transform.GetComponent<Building>();
                    _building.IsMoving();
                }
            }
        }
    }
}
