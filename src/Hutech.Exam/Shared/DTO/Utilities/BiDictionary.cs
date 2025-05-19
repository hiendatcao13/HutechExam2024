using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Utilities
{
    public class BiDictionary<K, V> where K : notnull where V : notnull
    {
        private readonly Dictionary<K, V> _forward = new();
        private readonly Dictionary<V, K> _reverse = new();

        public void Add(K key, V value)
        {
            _forward[key] = value;
            _reverse[value] = key;
        }

        public bool TryGetByKey(K key, [NotNullWhen(true)] out V? value)
    => _forward.TryGetValue(key, out value);

        public bool TryGetByValue(V value, [NotNullWhen(true)] out K? key)
            => _reverse.TryGetValue(value, out key);

        public void RemoveByKey(K key)
        {
            if (_forward.TryGetValue(key, out var value))
            {
                _forward.Remove(key);
                _reverse.Remove(value);
            }
        }

        public void RemoveByValue(V value)
        {
            if (_reverse.TryGetValue(value, out var key))
            {
                _reverse.Remove(value);
                _forward.Remove(key);
            }
        }
        public void Clear()
        {
            {
                _forward.Clear();
                _reverse.Clear();
            }
        }
    }
}
