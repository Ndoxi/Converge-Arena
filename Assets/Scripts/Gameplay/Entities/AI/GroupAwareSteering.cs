using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.Gameplay.AI
{
    public class GroupAwareSteering
    {
        private readonly float _groupGravityFactor;
        private readonly float _minGroupDistance;
        private readonly float _separationForce;
        private readonly float _smoothSpeed;

        private Vector3 _currentDirection = Vector3.zero;

        public GroupAwareSteering(float groupGravityFactor = 0.3f,
                                  float minGroupDistance = 1.5f,
                                  float separationForce = 10f,
                                  float smoothSpeed = 5f)
        {
            _groupGravityFactor = groupGravityFactor;
            _minGroupDistance = minGroupDistance;
            _separationForce = separationForce;
            _smoothSpeed = smoothSpeed;
        }

        public Vector3 CalculateDirection(Entity entity, IEntityGroup group, Vector3 waypoint, float deltaTime)
        {
            if (entity == null || !entity.isAlive)
            {
                _currentDirection = Vector3.Lerp(_currentDirection, Vector3.zero, Mathf.Clamp01(deltaTime * _smoothSpeed));
                return _currentDirection;
            }

            Vector3 position = entity.transform.position;
            Vector3 toWaypoint = waypoint - position;

            Vector3 desired = Vector3.zero;
            if (toWaypoint.sqrMagnitude > 0.01f)
                desired = toWaypoint.normalized;

            Vector3 groupForce = Vector3.zero;
            if (group != null)
                groupForce = (group.center - position) * _groupGravityFactor;

            Vector3 separation = Vector3.zero;
            if (group != null && group.entities != null)
            {
                foreach (var other in group.entities)
                {
                    if (other == null || other == entity || !other.isAlive)
                        continue;

                    Vector3 diff = position - other.transform.position;
                    float dist = diff.magnitude;
                    if (dist <= 0f)
                        continue;

                    if (dist < _minGroupDistance)
                    {
                        float factor = (_minGroupDistance - dist) / _minGroupDistance;
                        separation += diff.normalized * (_separationForce * factor);
                    }
                }
            }

            Vector3 combined = desired + groupForce + separation;

            if (combined.sqrMagnitude < 0.0001f)
            {
                _currentDirection = Vector3.Lerp(_currentDirection, Vector3.zero, Mathf.Clamp01(deltaTime * _smoothSpeed));
                _currentDirection.y = 0f;
                return _currentDirection;
            }

            Vector3 steering = combined.normalized;
            _currentDirection = Vector3.Slerp(_currentDirection, steering, Mathf.Clamp01(deltaTime * _smoothSpeed));
            _currentDirection.y = 0f;
            return _currentDirection;
        }

        /// <summary>
        /// Reset internal smoothing state (call when changing targets/states).
        /// </summary>
        public void Reset()
        {
            _currentDirection = Vector3.zero;
        }
    }
}