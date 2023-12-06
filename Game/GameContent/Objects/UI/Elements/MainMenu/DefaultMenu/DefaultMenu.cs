using System;
using Engine.Components;
using Engine.Events;
using Engine.UI;
using Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameContent.UI {
    class DefaultMenu : UIEntity {
        const int OFFSET = 8;
        SelectionMenu Items;
        public DefaultMenu(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            Items = new SelectionMenu(game, events, manager);
            InitSingleplayerButton(game, events, manager);
            //InitMultiplayerButton(game, events, manager);
            InitSettingsButton(game, events, manager);
            InitExitButton(game, events, manager);
            GetDimAndSetPos(manager);
            Items.SetFocus();
        }

        void GetDimAndSetPos(UIManager manager) {
            int w = 0;
            int h = 0;
            foreach (var itemList in Items.Items) {
                var wCounter = 0;
                var itemH = OFFSET;
                foreach (var item in itemList) {
                    var dim = item.GetComponent<DimensionComponent>();
                    wCounter += dim.Width;
                    itemH += dim.Height;
                }
                if (wCounter > w) w = wCounter;
                h += itemH;
            }
            SetDim(w, h);
            var pos = GetComponent<PositionComponent>();
            pos.Center(manager.UI_W, manager.UI_H);
            Items.AdjustPosition(pos.Coords);
        }

        void InitSingleplayerButton(Game game, SceneEvents events, UIManager manager) {
            var btn = new Button(game, events, manager);
            btn.SetText("Singleplayer");
            btn.SetAction(SingleplayerAction);
            btn.SetSpriteDepth(UIDepth.Foreground1);
            btn.SetTextDepth(UIDepth.Foreground2);
            Items.AddItem(btn);
            AddChild(btn);
        }

        void InitMultiplayerButton(Game game, SceneEvents events, UIManager manager) {
            var btn = new Button(game, events, manager);
            btn.SetText("Multiplayer");
            btn.SetAction(MultiplayerAction);
            btn.SetSpriteDepth(UIDepth.Foreground1);
            btn.SetTextDepth(UIDepth.Foreground2);
            Items.AddItem(btn);
            AddChild(btn);
        }

        void InitSettingsButton(Game game, SceneEvents events, UIManager manager) {
            var btn = new Button(game, events, manager);
            btn.SetText("Settings");
            btn.SetAction(SettingsAction);
            btn.SetSpriteDepth(UIDepth.Foreground1);
            btn.SetTextDepth(UIDepth.Foreground2);
            Items.AddItem(btn);
            AddChild(btn);
        }

        void InitExitButton(Game game, SceneEvents events, UIManager manager) {
            var btn = new Button(game, events, manager);
            btn.SetText("Exit");
            btn.SetAction(ExitAction);
            btn.SetSpriteDepth(UIDepth.Foreground1);
            btn.SetTextDepth(UIDepth.Foreground2);
            Items.AddItem(btn);
            AddChild(btn);
        }

        void TestCallback(object sender, EventArgs args) {
            Console.WriteLine("Test callback called");
        }

        public void SingleplayerAction(object sender, EventArgs args) {
            (Manager as MainMenuUIManager).ChangeState(MainMenuState.MAP_SELECTION);
        }

        public void MultiplayerAction(object sender, EventArgs args) {
            (Manager as MainMenuUIManager).ChangeState(MainMenuState.MULTIPLAYER);
        }

        public void SettingsAction(object sender, EventArgs args) {
            (Manager as MainMenuUIManager).ChangeState(MainMenuState.SETTINGS);
        }

        public void ExitAction(object sender, EventArgs args) {
            _game.Exit();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (InputHelper.KeyPressed(Keys.Up)) Items.GoUp();
            if (InputHelper.KeyPressed(Keys.Down)) Items.GoDown();
            if (InputHelper.KeyPressed(Keys.Right)) Items.GoRight();
            if (InputHelper.KeyPressed(Keys.Left)) Items.GoLeft();
            if (InputHelper.KeyPressed(Keys.Enter)) Items.Execute();
            if (InputHelper.KeyPressed(Keys.Escape)) ExitAction(this, EventArgs.Empty);
        }
    }
}