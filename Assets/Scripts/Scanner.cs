using UnityEngine;

namespace Assets.Scripts
{
    public class Scanner : MonoBehaviour
    {
        public float scanRange;
        public LayerMask targetLayer;
        public RaycastHit2D[] targets;
        public Transform nearestTarget;

        private void FixedUpdate()
        {
            targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
            nearestTarget = GetNearest();
        }

        private Transform GetNearest()
        {
            Transform result = null;
            var diff = 100f;

            foreach (var target in targets)
            {
                var scannerPos = transform.position;
                var targetPos = target.transform.position;
                var currDiff = Vector3.Distance(scannerPos, targetPos);

                if (currDiff < diff)
                {
                    diff = currDiff;
                    result = target.transform;
                }
            }

            return result;
        }
    }
}