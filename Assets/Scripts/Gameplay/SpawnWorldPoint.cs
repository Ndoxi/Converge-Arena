using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class SpawnWorldPoint : WorldPoint
    {
        public Team team => _team;

        [SerializeField] private Team _team;
    }
}
