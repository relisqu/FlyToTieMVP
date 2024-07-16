using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "UnitListData", menuName = "ScriptableObjects/UnitMeta", order = 0)]
    public class UnitDataList : ScriptableObject
    {
        public List<UnitData> UnitList;
    }
}