using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class EntityView : MonoBehaviour
    {
        public Material material 
        { 
            get => _meshRenderer.material; 
            set => _meshRenderer.material = value; 
        }

        [SerializeField] private MeshRenderer _meshRenderer;
    }
}
