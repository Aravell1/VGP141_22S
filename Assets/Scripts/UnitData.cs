using UnityEngine;

namespace VGP141
{
    [System.Serializable]
    public class UnitData
    {
        public float BuildTime;
        public UnitView Prefab;
        [Tooltip("The speed of the unit as it moves across the map.")]
        public float MoveSpeed;
        [Tooltip("The rate that the unit fires at in seconds.")]
        public float AttackRate;
        [Tooltip("The range that the unit can start to fire at it's target.")]
        public float AttackRange;
    }
}