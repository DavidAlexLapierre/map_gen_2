using System;
using Engine.Components;
using Engine.Events;
using Engine.Managers;
using Engine.UI;
using Engine.Utils;
using GameContent.Models;
using GameContent.Scenes;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class WorldContainer : UIEntity{
        const int FOCUS_INDEX = 1;
        const int REST_INDEX = 0;
        WorldData Data;
        Text NameText;
        Text VersionText;
        Text CreationDateText;

        public WorldContainer(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            AddComponent(new SpriteComponent(game, Sprites.GetSprite("s_world_container")));
            AddComponent(new FocusComponent(FocusCallback));
            AddComponent(new ActionComponent());
            GetComponent<ActionComponent>().SetAction(StartMap);
            var sprite = GetComponent<SpriteComponent>();
            SetDim(sprite.Width, sprite.Height);
        }

        public void SetData(WorldData data) {
            Data = data;
            var pos = GetComponent<PositionComponent>().Coords;
            NameText = new Text(_game, _events, Manager);
            NameText.SetText(data.Name);
            NameText.SetPos(pos.X + 8, pos.Y + 8);
            NameText.GetComponent<TextComponent>().SetSize(8);

            CreationDateText = new Text(_game, _events, Manager);
            CreationDateText.SetText(data.CreationDate);
            CreationDateText.SetPos(pos.X + 8, pos.Y + 16);
            CreationDateText.GetComponent<TextComponent>().SetSize(4);
            CreationDateText.GetComponent<TextComponent>().SetColor(ColorPalette.GetColor("light_gray"));

            VersionText = new Text(_game, _events, Manager);
            VersionText.SetText("Version: " + data.Version);
            VersionText.SetPos(pos.X + 8, pos.Y + 24);
            VersionText.GetComponent<TextComponent>().SetSize(4);
            VersionText.GetComponent<TextComponent>().SetColor(ColorPalette.GetColor("light_gray"));

            AddChild(NameText);
            AddChild(CreationDateText);
            AddChild(VersionText);
        }

        void StartMap(object sender, EventArgs args) {
            _game.Services.GetService<SceneManager>().ChangeScene<WorldMapScene>(Data.Name);
        }

        public void SetAction(EventHandler handler) { GetComponent<ActionComponent>().SetAction(handler); }

        void FocusCallback(object sender, EventArgs args) {
            if (GetComponent<FocusComponent>().IsFocused) {
                GetComponent<SpriteComponent>().SetIndex(FOCUS_INDEX);
            } else {
                GetComponent<SpriteComponent>().SetIndex(REST_INDEX);
            }
        }
    }
}