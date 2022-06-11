using UnityEngine;

namespace VGP141
{
    public class UnitFactory : MonoBehaviour
    {
        private static int s_Count;

        public UnitData[] _unitData;

        public System.EventHandler<UnitView> UnitCreated;

        private DataStructures.HashTable<UnitData> _unitDataTable;

        public UnitView CreateUnit(string unitId)
        {
            UnitData data = _unitDataTable.Find(unitId);
            UnitView unit = Instantiate(data.Prefab,
                new Vector3(Random.Range(BuildMenu.X_RANGE.x, BuildMenu.X_RANGE.y), 0,
                            Random.Range(BuildMenu.Z_RANGE.x, BuildMenu.Z_RANGE.y)),
                Quaternion.identity);
            unit.name = $"Unit{s_Count++}";
            unit.Initialize(data);

            UnitCreated?.Invoke(this, unit);
            UnitCreated += unit.OnUnitCreated;

            return unit;
        }

        public UnitData GetUnitData(string unitId)
        {
            return _unitDataTable.Find(unitId);
        }

        private void Awake()
        {
            _unitDataTable = new DataStructures.HashTable<UnitData>((uint)_unitData.Length);
        }

        private void Start()
        {
            for (int i = 0; i < _unitData.Length; i++)
            {
                _unitDataTable.Insert(((UnitType)i).ToString(), _unitData[i]);
            }
        }
    }
}