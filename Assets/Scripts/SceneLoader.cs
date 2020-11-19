using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public static class SceneLoader
    {
        public enum Scene
        {
            GameScene,
            Loading
        }
        public static void Load(Scene scene)
        {
            SceneManager.LoadScene(Scene.Loading.ToString());
            SceneManager.LoadScene(scene.ToString());
        }
    }

}