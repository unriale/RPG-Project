using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeOutTime;
        [SerializeField] float fadeInTime;

        Coroutine activeCoroutine = null;


        CanvasGroup canvasGroup;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// 1s -> alpha 0 to 1, frame: deltaTime, num of frames = time/deltaTime, 1/0.1 = 10 frames
        /// 1 / (time/deltatime) = (deltatime / time) -> alpha val per frame 
        public IEnumerator FadeOut()
        {
            return Fade(1);
        }


        public IEnumerator FadeIn()
        {
            return Fade(0);
        }

        private IEnumerator Fade(float target)
        {
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }
            activeCoroutine = StartCoroutine(FadeCoroutine(target));
            yield return activeCoroutine;
        }

        private IEnumerator FadeCoroutine(float target)
        {
            while (!Mathf.Approximately(target, canvasGroup.alpha))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / fadeOutTime);
                yield return null;
            }
        }


        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

    }
}