using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryManager {
    [Inject] private GameSettingsInstaller.GameSetting setting;
    [Inject] private GameSettingsInstaller.PrefabSettings prefabs;
    [Inject] private TubeManager _tubeManager;
    [Inject]
    private GameSettingsInstaller.GameSetting _setting;

    private Dictionary<TubeType, int> inventory = new Dictionary<TubeType, int>();

    public void Unlock(List<InventoryDto> inventoryDtos) {
        if (_setting.isDebug) {
            return;            
        }

        foreach (InventoryDto inventoryDto in inventoryDtos) {
            int count = 0;
            if (inventory.ContainsKey(inventoryDto.tubeType)) {
                count = inventory[inventoryDto.tubeType];
            }

            count = count + 1;
            inventory[inventoryDto.tubeType] = count;
        }
    }

    public int InventoryCount(TubeType tubeType) {
        if (setting.isDebug) {
            return 100;
        }

        if (!inventory.ContainsKey(tubeType)) {
            return 0;
        }

        return inventory[tubeType];
    }

    public Dictionary<TubeType, int> Inventory => inventory;

    public void GetOutOfInventory(TubeType tubeType) {
        if (!setting.isDebug) {
            if (!inventory.ContainsKey(tubeType)) {
                Debug.LogError("No in inventory " + tubeType);
            }

            int count = inventory[tubeType];
            if (count == 0) {
                Debug.LogError("No in inventory " + tubeType);
            }
            inventory[tubeType] = count - 1;
        }
        _tubeManager.Create(new InventoryDto(tubeType, Constants.CREATE_POSITION, 0));
    }
}