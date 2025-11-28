using TowerDefence.Core;

namespace TowerDefence.Systems
{
    public interface ILevelBuilder : IService
    {
        void Load();
        void Unload();
    }
}

