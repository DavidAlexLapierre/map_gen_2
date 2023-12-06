using System;
using System.Collections.Generic;
using Engine.Components;
using Engine.Events;
using Engine.UI;
using Engine.Utils;
using GameContent.Models;
using GameContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameContent.UI {

    class DeleteArgs : EventArgs {
        public string Name { get; private set; }
        public UIManager Manager { get; private set; }
        public DeleteArgs(string name, UIManager manager) {
            Name = name;
            Manager = manager;
        }
    }

    class MapSelectionMenu : UIEntity {
        SelectionMenu Buttons;
        MapSelectionContainer Container;

        public MapSelectionMenu(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            InitUI(game, events, manager);
        }

        void InitUI(Game game, SceneEvents events, UIManager manager) {
            Buttons = new SelectionMenu(game, events, manager);
            AddChild(Buttons);
            InitContainer(game, events, manager);
            LoadSavedGames(game, events, manager);
            InitOtherButtons(game, events, manager);
            Buttons.SetFocus();
        }

        void InitContainer(Game game, SceneEvents events, UIManager manager) {
            Container = new MapSelectionContainer(game, events, manager);
            Container.GetComponent<PositionComponent>().Center(Manager.UI_W, Manager.UI_H);
            Container.SetSpriteDepth(UIDepth.background);
            AddChild(Container);
            SetDim(Container.GetDim().X, Container.GetDim().Y);
        }

        void LoadSavedGames(Game game, SceneEvents events, UIManager manager) {
            // TODO: Retrieve maps from save
            var worlds = WorldDataController.LoadWorlds();
            if (worlds.Count > 0) {
                int counter = 0;
                var pos = Container.GetComponent<PositionComponent>().Coords;
                foreach (var world in worlds) {
                    List<UIEntity> buttons = new List<UIEntity>();

                    // World container
                    var worldContainer = new WorldContainer(game, events, manager);
                    var xx = pos.X + Container.InitialWorldListPos.X;
                    var yy = pos.Y + Container.InitialWorldListPos.Y + counter * worldContainer.GetDim().Y;
                    worldContainer.SetPos(xx, yy);
                    worldContainer.SetData(world);
                    ++counter;

                    // Delete icon
                    var xOffset = worldContainer.GetDim().X - 3;
                    var yOffset = 8;
                    var deleteIcon = new DeleteIcon(_game, _events, manager);
                    deleteIcon.SetPos(xx + xOffset, yy + yOffset);
                    deleteIcon.SetAction((sender, e) => DeleteWorldHandler(sender, new DeleteArgs(world.Name, manager)));

                    buttons.Add(worldContainer);
                    buttons.Add(deleteIcon);
                    AddChild(worldContainer);
                    AddChild(deleteIcon);

                    Buttons.AddItemList(buttons);
                }
            } else {
                var initPos = Container.GetComponent<PositionComponent>().Coords;
                var text = new Text(game, events, manager);
                text.Value.SetColor(ColorPalette.GetColor("light_gray"));
                text.Value.SetText("No map has been created");
                text.SetPos(initPos.X + Container.NoMapMessagePos.X, initPos.Y + Container.NoMapMessagePos.Y);
                AddChild(text);
            }
        }

        void InitOtherButtons(Game game, SceneEvents events, UIManager manager) {
            var initPos = Container.GetComponent<PositionComponent>().Coords;
            var buttons = new List<UIEntity>();

            // NEW GAME
            var newMapBtn = new Button(game, events, manager);
            newMapBtn.SetText("Create Map");
            newMapBtn.SetAction(NewMapBtnHandler);
            newMapBtn.SetPos(initPos.X + Container.NewMapBtnPos.X, initPos.Y + Container.NewMapBtnPos.Y);
            newMapBtn.SetSpriteDepth(UIDepth.Foreground1);
            newMapBtn.SetTextDepth(UIDepth.Foreground2);
            buttons.Add(newMapBtn);
            AddChild(newMapBtn);

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

        void NewMapBtnHandler(object sender, EventArgs args) {
            (Manager as MainMenuUIManager).ChangeState(MainMenuState.MAP_CREATION);
        }

        void BackBtnHandler(object sender, EventArgs args) {
            (Manager as MainMenuUIManager).ChangeState(MainMenuState.DEFAULT);
        }

        void DeleteWorldHandler(object sender, DeleteArgs args) {
            WorldDataController.DeleteWorld(args.Name);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (InputHelper.KeyPressed(Keys.Up)) Buttons.GoUp();
            if (InputHelper.KeyPressed(Keys.Down)) Buttons.GoDown();
            if (InputHelper.KeyPressed(Keys.Right)) Buttons.GoRight();
            if (InputHelper.KeyPressed(Keys.Left)) Buttons.GoLeft();

            if (InputHelper.KeyPressed(Keys.Enter)) { Buttons.Execute(); }
            if (InputHelper.KeyPressed(Keys.Escape)) { BackBtnHandler(this, EventArgs.Empty); }
        }
    }
}