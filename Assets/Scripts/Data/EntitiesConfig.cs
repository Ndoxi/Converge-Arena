using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.Data
{
    [CreateAssetMenu(menuName = "TowerDefence/Data/EntitiesConfig")]
    public class EntitiesConfig : ScriptableObject
    {
        public Entity entityPrefab => _entityPrefab;
        public EntityData entityData => _entityData;

        [SerializeField] private Entity _entityPrefab;
        [SerializeField] private EntityData _entityData;
    }
}