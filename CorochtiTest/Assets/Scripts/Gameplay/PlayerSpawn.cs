using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            player.MovementSystem.collider2d.enabled = true;
            player.InputSystem.State = SystemState.Disable;
            if (player.AudioSystem != null)
                player.AudioSystem.PlayAudio(ConstantDefinitions.RESPAWN_SFX);
            player.HealthSystem.Respawn();
            player.Teleport(model.spawnPoint.transform.position);
            player.MovementSystem.jumpState = PlayerController.JumpState.Grounded;
            player.AnimationSystem.Dead(false);
            player.GfxSystem.ChangeColor(false);
            player.MovementSystem.Landing();
            model.virtualCamera.Follow = player.transform;
            model.virtualCamera.LookAt = player.transform;
            Simulation.Schedule<EnablePlayerInput>(2f);
        }
    }
}