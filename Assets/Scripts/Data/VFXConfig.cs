using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.Data
{
    [CreateAssetMenu(menuName = "TowerDefence/Data/VFXConfig")]
    public class VFXConfig : ScriptableObject
    {
        public VFX attackPrefab => _attackPrefab;

        [SerializeField] private VFX _attackPrefab;
    }
}