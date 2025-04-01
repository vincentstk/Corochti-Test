using System;
using Hiraishin.ObserverPattern;
using Platformer.Gameplay;
using UnityEngine;
using Zenject;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [Inject] private ConfigSystem _configSystem;
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;
        
        private PlayerHudSystem _hudSystem;

        public void Init(PlayerHudSystem hudSystem)
        {
            maxHP = _configSystem.Config.maxHp;
            currentHP = maxHP;
            _hudSystem = hudSystem;
        }
        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            this.PostEvent(EventID.GetHit);
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            _hudSystem.DecreaseHeart(currentHP);
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        public void Respawn()
        {
            currentHP = maxHP;
            _hudSystem.ResetHearts();
        }
    }
}
