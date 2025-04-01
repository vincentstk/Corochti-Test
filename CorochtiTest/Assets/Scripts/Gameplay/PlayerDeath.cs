using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            if (!player.HealthSystem.IsAlive)
            {
                player.HealthSystem.Die();
                model.virtualCamera.Follow = null;
                model.virtualCamera.LookAt = null;
                // player.collider.enabled = false;
                player.InputSystem.State = SystemState.Disable;

                if (player.AudioSystem != null)
                    player.AudioSystem.PlayAudio(ConstantDefinitions.HURT_SFX);
                player.AnimationSystem.Dead(true);
                Simulation.Schedule<PlayerSpawn>(2);
            }
        }
    }
}