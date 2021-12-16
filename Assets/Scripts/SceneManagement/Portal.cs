using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [Tooltip("Time in seconds to wait for establishing the main camera and loading the next scene")]
        [SerializeField] float waitForLoad = 1f;
        enum DestinationIdentifier
        {
            A, B
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                print("COLLISION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1");
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }
            Fader fader = FindObjectOfType<Fader>();
            DontDestroyOnLoad(gameObject);

            // Remove control
            PlayerController controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            controller.enabled = false;

            yield return fader.FadeOut();
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            // Remove control
            PlayerController newController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newController.enabled = false;

            wrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            wrapper.Save();
            wrapper.Load(); // for fixing a bug with an archer
            yield return new WaitForSeconds(waitForLoad);
            yield return fader.FadeIn();

            // Restore control
            newController.enabled = true;
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination == this.destination)
                    return portal;
            }
            return null;
        }
    }
}