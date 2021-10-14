using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;


namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Awake()
        {
            // to be sure Restore() is called first
            StartCoroutine(LoadLastScene()); 
        }

        private IEnumerator LoadLastScene()
        {
            yield return FindObjectOfType<SavingSystem>().LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            Load(); // TEMP FOR FIXING A BUG WITH ALIVE ARCHER (THAT IS ACTUALLY DEAD...)
            yield return new WaitForSeconds(1f);
            yield return fader.FadeIn();
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}