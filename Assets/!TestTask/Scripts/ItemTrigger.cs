using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] private ItemTriggerNeed[] itemNeeds;
    private Dictionary<string, ItemTriggerNeed> _dicItemNeeds = new Dictionary<string, ItemTriggerNeed>();

    private GameManager _gameManager;

    [Inject]
    public void Init(GameManager gameManager)
    {
        foreach (var item in itemNeeds)
        {
            if (!_dicItemNeeds.ContainsKey(item.dataRef.Id))
                _dicItemNeeds.Add(item.dataRef.Id, item);
        }
        _gameManager = gameManager;        
    }

    private void AddItem(ItemData item)
    {
        if (_dicItemNeeds.ContainsKey(item.Id))
        {
            _dicItemNeeds[item.Id].Count--;
        }
        UpdateNeeds();
    }

    private void RemoveItem(ItemData item)
    {
        if (_dicItemNeeds.ContainsKey(item.Id))
        {
            _dicItemNeeds[item.Id].Count++;
        }
    }

    private void UpdateNeeds ()
    {
        if (_dicItemNeeds.All((itemNeeds) => itemNeeds.Value.IsComplite))
        {
            _gameManager.Win();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Grasped grapable))
        {
            AddItem(grapable.DataRef);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Grasped grapable))
        {
            RemoveItem(grapable.DataRef);
        }
    }
}

[System.Serializable]
public class ItemTriggerNeed
{
    public ItemData dataRef;
    public int Count;

    public bool IsComplite => Count <= 0;
}
