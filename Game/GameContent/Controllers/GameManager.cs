using Engine.Core;
using Engine.Events;
using Microsoft.Xna.Framework;

namespace GameContent.Controllers {
    class GameController : Entity {
        public GameController(Game game, SceneEvents events) : base(game, events) {
        }
    }
}