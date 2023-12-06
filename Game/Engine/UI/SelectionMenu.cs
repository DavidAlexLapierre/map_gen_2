using System;
using System.Collections.Generic;
using Engine.Components;
using Engine.Events;
using Microsoft.Xna.Framework;

namespace Engine.UI {
    class SelectionMenu : UIEntity {
        protected int CurrentListIndex;
        protected int CurrentLevelIndex;
        public List<List<UIEntity>> Items { get; protected set; }
        public SelectionMenu(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            Items = new List<List<UIEntity>>();
        }

        public void AddItem(UIEntity entity) {
            var itemList = new List<UIEntity>();
            itemList.Add(entity);
            Items.Add(itemList);
        }

        public void AddItemList(List<UIEntity> entities) {
            Items.Add(entities);
        }

        public void GoUp() {
            SetFocus();
            CurrentLevelIndex = 0;
            --CurrentListIndex;
            if (CurrentListIndex < 0) {
                CurrentListIndex = Items.Count - 1;
            }
            SetFocus();
        }

        public void GoDown() {
            SetFocus();
            CurrentLevelIndex = 0;
            ++CurrentListIndex;
            if (CurrentListIndex >= Items.Count) {
                CurrentListIndex = 0;
            }
            SetFocus();
        }

        public void GoRight() {
            if (Items[CurrentListIndex].Count > 1) {
                SetFocus();
                if (CurrentLevelIndex < Items[CurrentListIndex].Count - 1) ++CurrentLevelIndex;
                SetFocus();
            }
        }

        public void GoLeft() {
            if (Items[CurrentListIndex].Count > 1) {
                SetFocus();
                if (CurrentLevelIndex > 0) --CurrentLevelIndex;
                SetFocus();
            }
        }

        public int GetListCount() { return Items.Count; }
        public int GetLevelCount() { return Items[CurrentListIndex].Count; }
        
        public void Execute() {
            var action = GetCurrent().GetComponent<ActionComponent>();
            if (action != null) {
                action.Execute();
            }
        }

        public UIEntity GetCurrent() {
            return Items[CurrentListIndex][CurrentLevelIndex];
        }

        public void SetFocus() {
            if (Items.Count > 0) {
                var item = GetCurrent();
                var focus = item.GetComponent<FocusComponent>();
                if (focus != null) focus.ToggleFocus();
            }
        }

        public void AdjustPosition(Vector2 parentPos) {
            var hCounter = 0;
            foreach (var itemList in Items) {
                var wCounter = 0;
                foreach (var item in itemList) {
                    var dim = item.GetComponent<DimensionComponent>();
                    var pos = item.GetComponent<PositionComponent>().Coords;
                    var newPos = parentPos + pos;
                    item.SetPos(newPos.X + wCounter * dim.Width, newPos.Y + hCounter * dim.Height);
                    ++wCounter;
                }
                ++hCounter;
            }
        }
    }
}