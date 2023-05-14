using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//NameSpaceForSomeReason

namespace PleaseFixMe{


    public class LevelLoaderScript : MonoBehaviour
    {
        public Animator transition;

        public float transitionTime = 1f;
        public void start() { }

        public void update() {

        }

        public void LevelLoader(int LvlIndexOne) {
            StartCoroutine(LoadScene(LvlIndexOne));
        }

        IEnumerator LoadScene(int levelIndex)
        {

            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(levelIndex);
        }

    }
}