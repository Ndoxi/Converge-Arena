using UnityEngine;

namespace TowerDefence.Systems
{
    public class PlayerEntityDecorator : EntityDecorator
    {
        public override void Decorate()
        {
            base.Decorate();
            var highlight = Object.Instantiate(_config.playerHighlightPrefab, _entity.transform);
            highlight.transform.localPosition = Vector3.zero;
        }
    }
}

