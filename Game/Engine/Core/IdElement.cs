using System;
namespace Engine.Core {
    abstract class IdElement {
        public Guid Id { get; private set; }
        public IdElement() {
            Id = Guid.NewGuid();
        }
    }
}
