using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DictonarySerialize<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<KeyPairValue<TKey, TValue>> _dictonary;

    private List<TKey> _keys = new List<TKey>();
    private List<TValue> _values = new List<TValue>();

    public TValue this[TKey key] 
    { 
        get 
        {
            TryGetValue(key, out TValue value); 
            return value;
        } 
        set
        {
            int index = _keys.IndexOf(key);
            if (index != -1)
                _values[index] = value;
        }
    }
    public ICollection<TKey> Keys => _keys;
    public ICollection<TValue> Values => _values;
    public int Count => _keys.Count;
    public bool IsReadOnly => throw new NotImplementedException();
    public void Add(TKey key, TValue value)
    {
        _keys.Add(key);
        _values.Add(value);
    }
    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }
    public void Clear()
    {
        _keys.Clear();
        _values.Clear();
    }
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return ContainsKey(item.Key) && ContainsValue(item.Value);
    }
    public bool ContainsKey(TKey key)
    {
        return _keys.Contains(key);
    }
    public bool ContainsValue(TValue value)
    {
        return _values.Contains(value);
    }
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>();

        for(int i = 0; i < Count; i++)
            list.Add(new KeyValuePair<TKey, TValue>(_keys[i], _values[i]));

        return list.GetEnumerator();
    }
    public bool Remove(TKey key)
    {
        int index = _keys.IndexOf(key);
        return index != -1 && _keys.Remove(key) && _values.Remove(_values[index]);
    }
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return _keys.Remove(item.Key) && _values.Remove(item.Value);
    }
    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = _keys.IndexOf(key);
        value = _values[index];
        return index != -1;
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
    public void OnBeforeSerialize()
    {
        _dictonary.Clear();
        for (int i = 0; i < Count; i++)
        {
            KeyPairValue<TKey, TValue> keyPairValue = new KeyPairValue<TKey, TValue>(_keys[i], _values[i]);
            _dictonary.Add(keyPairValue);
        }
    }
    public void OnAfterDeserialize()
    {
        Clear();
        foreach (var keyPairValue in _dictonary)
        {
            Add(keyPairValue.Key, keyPairValue.Value);
        }
    }
}