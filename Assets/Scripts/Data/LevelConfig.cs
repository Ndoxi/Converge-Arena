using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.Data
{
    [CreateAssetMenu(menuName = "TowerDefence/Data/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public Team playersTeam;
        public TeamData[] teamDatas;
    }
}