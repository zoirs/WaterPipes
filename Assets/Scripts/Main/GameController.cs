﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace Main {
    public enum GameStates {
        WaitingToStart,
        Playing,
        LevelComplete,
        GameOver
    }

    public class GameController : IInitializable, ITickable, IDisposable {
        // readonly PeopleManager _peopleManager;
        readonly HomeManager _homeManager;
        readonly StoneManager _stoneManager;
        readonly InventoryManager _inventory;
        readonly LevelManager _levelManager;

        private List<string> levels = new List<string>();
        private int currentLevel;

        GameStates _state = GameStates.WaitingToStart;

        public GameController(HomeManager homeManager,StoneManager stoneManager, InventoryManager inventory,LevelManager levelManager) {
            // _peopleManager = peopleManager;
            _homeManager = homeManager;
            _stoneManager = stoneManager;
            _inventory = inventory;
            _levelManager = levelManager;
        }

        public void Initialize() {
            DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
            Debug.Log(dir);
            FileInfo[] info = dir.GetFiles("*.txt");

            foreach (FileInfo f in info) {
                levels.Add(f.Name);
            }
        }

        public void Dispose() { }

        public void Tick() {
            switch (_state) {
                case GameStates.WaitingToStart: {
                    UpdateStarting();
                    break;
                }
                case GameStates.Playing: {
//                UpdatePlaying();
                    break;
                }
                case GameStates.GameOver: {
//                UpdateGameOver();
                    break;
                }
                default: {
//                Assert.That(false);
                    break;
                }
            }
        }

        private void UpdateStarting() {
            
//         Assert.That(_state == GameStates.WaitingToStart);

//            _peopleManager.Start();
        }

        public void CreateEmptyLevel() {
            TubeLevel tubeLevel = new TubeLevel();
            tubeLevel.height = 10;
            tubeLevel.wight = 10;

            // HomesDto homesDto = new HomesDto(HomeType.ONE_ONE);
            // homesDto.position = new Vector2Int(5, 5);
            // tubeLevel.homes.Add(homesDto);
            // HomesDto homesDto2 = new HomesDto(HomeType.ONE_ONE);
            // homesDto2.position = new Vector2Int(2, 9);
            // tubeLevel.homes.Add(homesDto2);

            // StonesDto stonesDto = new StonesDto();
            // stonesDto.position = new Vector2Int(2, 10);
            // tubeLevel.stones.Add(stonesDto);

            // WaterWellsDto waterWellsDto = new WaterWellsDto(WellType.ONE_ONE);
            // waterWellsDto.position = new Vector2Int(5, 1);
            // tubeLevel.waterWells.Add(waterWellsDto);

            // InventoryDto inventoryDto = new InventoryDto(TubeType.LINE1, 0);
            // inventoryDto.position = new Vector2Int(5, 1);
            // tubeLevel.inventory.Add(inventoryDto);
            currentLevel = levels.Count;
            _levelManager.LoadLevel(tubeLevel);

            _state = GameStates.Playing;
        }

        public void StartGame(int index) {
            currentLevel = index;
  
            // string json = JsonUtility.ToJson(tubeLevel);
            // Debug.Log(json);

            // TextAsset levelasset = Resources.Load<TextAsset>("Levels/Level");
            // TubeLevel level = JsonUtility.FromJson<TubeLevel>(levelasset.text);
            
            // TubeLevel level = JsonUtility.FromJson<TubeLevel>(json);

            string filepath = Application.persistentDataPath + "/" + levels[index];

            StreamReader reader = new StreamReader(filepath);

            string readToEnd = reader.ReadToEnd();
            TubeLevel level = JsonUtility.FromJson<TubeLevel>(readToEnd);

            _levelManager.LoadLevel(level);

            _state = GameStates.Playing;
        }

        public List<string> Levels => levels;

        public int CurrentLevel => currentLevel;

        public GameStates State {
            get => _state;
            set => _state = value;
        }

        public void LoadNext() {
            currentLevel++;
            if (currentLevel >= levels.Count) {
                currentLevel = 0;
            }

            StartGame(currentLevel);

        }
    }
}