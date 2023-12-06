using System;

namespace GameContent.UI {
    class SelectOptionMenuItemData {
        public EventHandler Action { get; private set; }
        public string Text { get; private set; }
        public SelectOptionMenuItemData(string text, EventHandler action) {
            Action = action;
            Text = text;
        }
    }
}