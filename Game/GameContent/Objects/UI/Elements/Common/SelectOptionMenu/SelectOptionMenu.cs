using System;
using System.Collections.Generic;
using Engine.Components;
using Engine.Events;
using Engine.UI;
using Engine.Utils;
using GameContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameContent.UI {
    class SelectOptionMenu : UIEntity {
        int CurrentItemIndex;
        List<SelectOptionMenuItemData> Items;
        Point LeftArrowPos;
        Point RightArrowPos;
        Button CurrentItem;
        SelectOptionArrow LeftArrow;
        SelectOptionArrow RightArrow;

        public SelectOptionMenu(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            LeftArrowPos = new Point(-16, 0);
            RightArrowPos = new Point(48, 0);
            Items = new List<SelectOptionMenuItemData>();
            InitChildren(game, events, manager);
            AddComponent(new FocusComponent(FocusCallback));
            GetComponent<FocusComponent>().OverrideCallback(FocusCallback);
            var wDim =  CurrentItem.GetDim().X;
            var hDim = CurrentItem.GetDim().Y;
            SetDim(wDim, hDim);
        }

        void FocusCallback(object sender, EventArgs args) {
            if (GetComponent<FocusComponent>().IsFocused) {
                CurrentItem.GetComponent<FocusComponent>().ToggleFocus();
            } else {
                CurrentItem.GetComponent<FocusComponent>().ToggleFocus();
                LeftArrow.RemoveFocus();
                RightArrow.RemoveFocus();
            }
        }

        void InitChildren(Game game, SceneEvents events, UIManager manager) {
            var pos = GetComponent<PositionComponent>().Coords;

            LeftArrow = new SelectOptionArrow(game, events, manager);
            LeftArrow.GetComponent<ActionComponent>().SetAction(HandleLeftArrowPress);
            LeftArrow.Flip();
            AddChild(LeftArrow);

            CurrentItem = new Button(_game, _events, Manager);
            AddChild(CurrentItem);

            RightArrow = new SelectOptionArrow(game, events, manager);
            RightArrow.GetComponent<ActionComponent>().SetAction(HandleRightArrowPress);
            AddChild(RightArrow);
            AdjustPosition();
        }

        void SetNewCurrentItem() {
            CurrentItem.SetAction(Items[CurrentItemIndex].Action);
            CurrentItem.SetText(Items[CurrentItemIndex].Text);
        }

        public void AddItem(SelectOptionMenuItemData item) {
            Items.Add(item);
            SetNewCurrentItem();
        }

        public void HandleLeftArrowPress(object sender, EventArgs args) {
            --CurrentItemIndex;
            if (CurrentItemIndex < 0) CurrentItemIndex = Items.Count - 1;
            LeftArrow.Focus();
            RightArrow.RemoveFocus();
            SetNewCurrentItem();
        }

        public void HandleRightArrowPress(object sender, EventArgs args) {
            ++CurrentItemIndex;
            if (CurrentItemIndex >= Items.Count) CurrentItemIndex = 0;
            LeftArrow.RemoveFocus();
            RightArrow.Focus();
            SetNewCurrentItem();
        }

        public override void SetPos(float x, float y) {
            base.SetPos(x, y);
            AdjustPosition();
        }

        public void AdjustPosition() {
            var pos = GetComponent<PositionComponent>().Coords;
            LeftArrow.SetPos(pos.X + LeftArrowPos.X, pos.Y + LeftArrowPos.Y);
            CurrentItem.SetPos(pos.X, pos.Y);
            RightArrow.SetPos(pos.X + RightArrowPos.X, pos.Y + RightArrowPos.Y);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (GetComponent<FocusComponent>().IsFocused) {

                if (InputHelper.KeyPressed(Keys.Left)) { 
                    LeftArrow.GetComponent<ActionComponent>().Execute();
                    CurrentItem.GetComponent<ActionComponent>().Execute();
                }

                if (InputHelper.KeyPressed(Keys.Right)) {
                    RightArrow.GetComponent<ActionComponent>().Execute();
                    CurrentItem.GetComponent<ActionComponent>().Execute();
                }
            }
        }
    }
}