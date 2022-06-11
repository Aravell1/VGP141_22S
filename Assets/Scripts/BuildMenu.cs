using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VGP141
{
    public class BuildMenu : MonoBehaviour
    {
        public static readonly Vector2 X_RANGE = new Vector2(8, -8);
        public static readonly Vector2 Z_RANGE = new Vector2(11, -12);

        public Image[] ButtonFills;

        private DataStructures.Queue<UnitType> _buildQueue;
        private UnitFactory _unitFactory;

        /// <summary>
        /// This tracks how long has elapsed while we are building a unit.
        /// </summary>
        private float _elapsedBuildTime;

        public void SpawnUnit(UnitType unitType)
        {
            _unitFactory.CreateUnit(unitType.ToString());
        }

        private void Awake()
        {
            _buildQueue = new DataStructures.Queue<UnitType>();
            _unitFactory = GetComponent<UnitFactory>();
        }

        private void Update()
        {
            if (!_buildQueue.Empty())
            {
                _elapsedBuildTime += Time.deltaTime;

                UnitType currentUnit = _buildQueue.Peek();
                UnitData currentData = _unitFactory.GetUnitData(currentUnit.ToString());
                ButtonFills[(int)currentUnit].fillAmount = 1.0f - _elapsedBuildTime / currentData.BuildTime;
                if (_elapsedBuildTime >= currentData.BuildTime)
                {
                    SpawnUnit(currentUnit);
                    _buildQueue.Dequeue();
                    _elapsedBuildTime = 0;
                }
            }
        }

        public void OnBuildButtonPressed(int type)
        {
            if (ButtonFills[type].fillAmount == 0)
            {
                ButtonFills[type].fillAmount = 1;
            }
            _buildQueue.Enqueue((UnitType)type);
        }
    }
}