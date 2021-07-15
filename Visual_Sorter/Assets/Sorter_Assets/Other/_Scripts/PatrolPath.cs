using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            return (i + 1) % this.transform.childCount;
        }

        public Vector3 GetWaypoint(int i)
        {
            return this.transform.GetChild(i).position;
        }
    }
}
