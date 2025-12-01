using UnityEngine;

namespace TowerDefence.Data
{
    [CreateAssetMenu(menuName = "TowerDefence/Data/DecoratorConfig")]
    public class DecoratorConfig : ScriptableObject
    {
        public GameObject playerHighlightPrefab => _playerHighlightPrefab;
        public TeamDecorationData[] teamDecorationDatas => _teamDecorationDatas;

        [SerializeField] private GameObject _playerHighlightPrefab;
        [SerializeField] private TeamDecorationData[] _teamDecorationDatas;
    }
}