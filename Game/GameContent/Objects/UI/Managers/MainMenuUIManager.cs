using System;
using Engine.Components;
using Engine.Core;
using Engine.Events;
using Engine.UI;
using Engine.Utils;
using GameContent.Models;
using GameContent.Scenes;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class MainMenuUIManager : UIManager {
        UIEntity CurrentUI;
        MainMenuState State;
        public MainMenuUIManager(Game game, SceneEvents events) : base(game, events) {
            State = MainMenuState.DEFAULT;
            var scale = new ScaleComponent();
            scale.SetScale(Scale);
            AddComponent(scale);
            InitMenu(new DefaultMenu(game, events, this));
            InitVersionInfo(game, events);
        }

        void InitMenu(UIEntity menu) {
            menu.SetScale(Scale);
            CurrentUI = menu;
            AddChild(menu);
        }

        void InitVersionInfo(Game game, SceneEvents events) {
            var versionInfo = new VersionInfo(game, events, this);
            versionInfo.GetComponent<PositionComponent>().Bottom(UI_H);
            versionInfo.GetComponent<PositionComponent>().Left();
            versionInfo.GetComponent<PositionComponent>().Move(new Vector2(4,0));
            versionInfo.SetScale(Scale);
            AddChild(versionInfo);
        }

        public void ChangeState(MainMenuState state) {
            State = state;
            RemoveChild(CurrentUI.Id);
            switch (State) {
                case MainMenuState.DEFAULT:
                    InitMenu(new DefaultMenu(_game, _events, this));
                    break;
                case MainMenuState.MAP_SELECTION:
                    InitMenu(new MapSelectionMenu(_game, _events, this));
                    break;
                case MainMenuState.MAP_CREATION:
                    InitMenu(new MapCreationMenu(_game, _events, this));
                    break;
                case MainMenuState.CHARACTER_CREATION:
                    break;
                case MainMenuState.MULTIPLAYER:
                    Logger.Log("Multiplayer menu not yet implemented");
                    break;
                case MainMenuState.SETTINGS:
                    Logger.Log("Settings menu not yet implemented");
                    break;
                default:
                    break;
            }
        }
    }
}