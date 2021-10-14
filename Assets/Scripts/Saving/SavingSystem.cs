using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string fileName)
        {
            Dictionary<string, object> states = LoadFile(fileName);
            CaptureState(states);
            SaveFile(fileName, states);
        }

        public void Load(string fileName)
        {
            RestoreState(LoadFile(fileName));
        }

        public void Delete(string fileName)
        {
            File.Delete(GetPathFromSaveFile(fileName));
        }

        public IEnumerator LoadLastScene(string fileName)
        {
            Dictionary<string, object> stateDict = LoadFile(fileName);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (stateDict.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)stateDict["lastSceneBuildIndex"];
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreState(stateDict);
        }

        private Dictionary<string, object> LoadFile(string fileName)
        {
            string path = GetPathFromSaveFile(fileName);
            if (!File.Exists(path)) return new Dictionary<string, object>();
            print($"Load from {path}");
            using (FileStream fileStream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(fileStream);
            }
        }

        private void SaveFile(string fileName, object state)
        {
            string path = GetPathFromSaveFile(fileName);
            using (FileStream fileStream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, state);
            }
            print($"Save to {path}");
        }

        private void CaptureState(Dictionary<string, object> states)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) 
            {
                states[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            states["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if(state.ContainsKey(id))
                    saveable.RestoreState(state[id]);
            }
        }

        private string GetPathFromSaveFile(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName + ".sav");
        }
    }
}