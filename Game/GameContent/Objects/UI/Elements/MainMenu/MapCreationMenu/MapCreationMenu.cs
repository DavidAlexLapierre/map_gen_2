using System;
using System.Collections.Generic;
using Engine.Components;
using Engine.Events;
using Engine.Managers;
using Engine.UI;
using Engine.Utils;
using GameContent.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameContent.UI {
    class MapCreationMenu : UIEntity {
        SelectionMenu Buttons;
        MapCreationContainer Container;
        NameField NameField;
        public MapCreationMenu(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            Buttons = new SelectionMenu(game, events, manager);
            AddChild(Buttons);
            InitContainer(game, events, manager);
            InitNameField(game, events, manager);
            InitOtherButtons(game, events, manager);
            Buttons.SetFocus();
        }

        void InitContainer(Game game, SceneEvents events, UIManager manager) {
            Container = new MapCreationContainer(game, events, manager);
            Container.GetComponent<PositionComponent>().Center(Manager.UI_W, Manager.UI_H);
            Container.SetSpriteDepth(UIDepth.background);
            AddChild(Container);
            SetDim(Container.GetDim().X, Container.GetDim().Y);
        }

        void InitNameField(Game game, SceneEvents events, UIManager manager) {
            var initPos = Container.GetComponent<PositionComponent>().Coords;
            NameField = new NameField(game, events, manager);
            NameField.SetPos(initPos.X + Container.NameFieldPos.X, initPos.Y + Container.NameFieldPos.Y);
            Buttons.AddItem(NameField);
            AddChild(NameField);
        }

        void InitOtherButtons(Game game, SceneEvents events, UIManager manager) {
            var initPos = Container.GetComponent<PositionComponent>().Coords;
            var buttons = new List<UIEntity>();

            // NEW GAME
            var createMapBtn = new Button(game, events, manager);
            createMapBtn.SetText("Create Map");
            createMapBtn.SetAction(CreateMapBtnHandler);
            createMapBtn.SetPos(initPos.X + Container.CreateMapBtnPos.X, initPos.Y + Container.CreateMapBtnPos.Y);
            createMapBtn.SetSpriteDepth(UIDepth.Foreground1);
            createMapBtn.SetTextDepth(UIDepth.Foreground2);
            buttons.Add(createMapBtn);
            AddChild(createMapBtn);

            // BACK
            var backBtn = new Button(game, events, manager);
            backBtn.SetText("Back");
            backBtn.SetAction(BackBtnHandler);
            backBtn.SetPos(initPos.X + Container.BackBtnPos.X, initPos.Y + Container.BackBtnPos.Y);
            backBtn.SetSpriteDepth(UIDepth.Foreground1);
            backBtn.SetTextDepth(UIDepth.Foreground2);
            buttons.Add(backBtn);
            AddChild(backBtn);

            Buttons.AddItemList(buttons);
        }

        void CreateMapBtnHandler(object sender, EventArgs args) {
            if (VerifyMapName() && WorldDataController.CanCreateWorld()) {
                // TODO: Create the config file
                var canCreatWorld = WorldDataController.CreateWorld(NameField.GetValue());
                if (!canCreatWorld) Logger.LogError("A map with this name already exists");
                else {
                    // If the connection happens then change scene
                    _game.Services.GetService<SceneManager>().ChangeScene<WorldMapScene>(NameField.GetValue());
                }
            }
        }

        bool VerifyMapName() {
            return !string.IsNullOrEmpty(NameField.GetValue());
        }

        void BackBtnHandler(object sender, EventArgs args) {
            (Manager as MainMenuUIManager).ChangeState(MainMenuState.MAP_SELECTION);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (InputHelper.KeyPressed(Keys.Up)) Buttons.GoUp();
            if (InputHelper.KeyPressed(Keys.Down)) Buttons.GoDown();
            if (InputHelper.KeyPressed(Keys.Right)) Buttons.GoRight();
            if (InputHelper.KeyPressed(Keys.Left)) Buttons.GoLeft();
            if (InputHelper.KeyPressed(Keys.Enter)) Buttons.Execute();
            if (InputHelper.KeyPressed(Keys.Escape)) BackBtnHandler(this, EventArgs.Empty);
        }
    }
}