using System.Collections;
using System.Windows.Forms;

namespace Salem.Controls {
    public class GenericControlCollection<T> : CollectionBase where T : Control {
        private readonly Control _owner;

        public GenericControlCollection(Control owner) => _owner = owner;
        
        public T this[int index] { get => List[index] as T; }

        protected override void OnClear() {
            base.OnClear();
            _owner.Controls.Clear();
        }

        protected override void OnRemove(int index, object value) {
            base.OnRemove(index, value);
            _owner.Controls.RemoveAt(index);
        }

        public void Add(T value) {
            List.Add(value);
            _owner.Controls.Add(value);
        }

        public void Remove(T value) {
            List.Remove(value);
            _owner.Controls.Remove(value);
        }

        public void AddRange(T[] values) {
            foreach (var value in values)
                List.Add(value);

            _owner.Controls.AddRange(values);
        }

        public bool Contains(T value) => List.Contains(value);
    }
}
