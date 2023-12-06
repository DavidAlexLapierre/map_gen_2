using Microsoft.Xna.Framework;
using Engine.Events;
using System;
using System.Collections.Generic;

namespace Engine.Core {
    abstract class Entity : IdElement {
        public Entity Parent { get; protected set; }
        protected  Dictionary<string, Component> Components { get; set; }
        public Dictionary<Guid, Entity> Children { get; protected set; }
        protected SceneEvents _events { get; set; }
        protected Game _game { get; set; }

        public Entity(Game game, SceneEvents events) : base() {
            _game = game;
            Children = new Dictionary<Guid, Entity>();
            Components = new Dictionary<String, Component>();
            _events = events;
        }

        public virtual void Update(GameTime gameTime) {
            foreach (var component in Components.Values) {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime) {
            foreach (var component in Components.Values) {
                component.Draw(gameTime);
            }
        }

        protected virtual void AddChild(Entity child) {
            child.SetParent(this);
            Children.Add(child.Id, child);
            AddEntity(child);
        }

        protected void RemoveChild(Guid id) {
            if (Children.ContainsKey(id)) {
                Children[id].Discard();
                Children.Remove(id);
            }
        }

        public void AddComponent<T>(T component) {
            var type = typeof(T).ToString();
            Components.Add(type, component as Component);
            (component as Component).Parent = this;
        }

        public virtual void Discard() {
            foreach (var child in Children.Values) {
                child.Discard();
            }
            _events.RemoveEntity?.Invoke(this, new SceneEventArgs(Id));
        }

        public virtual void AddEntity(Entity entity) {
            _events.AddEntity?.Invoke(this, new SceneEventArgs(entity));
        }

        public virtual Entity GetEntity(Guid id) {
            return _events.Scene.GetEntity(id);
        }

        public virtual void TriggerNewEntities() {
            _events.TriggerNewEntities?.Invoke(this, new SceneEventArgs());
        }

        public void SetParent(Entity parent) {
            Parent = parent;
        }
        /// Retrieves a specific component
        public T GetComponent<T>() where T : Component {
            var type = typeof(T).ToString();
            if (Components.ContainsKey(type)) {
                return (T)Components[type];
            }

            return null;
        }
    }
}
