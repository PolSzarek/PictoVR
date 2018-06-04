using UnityEngine;
using UnityEngine.SceneManagement;

static public class MoveObjectToNewScene

{
    static GameObject targetObject;
    static string targetSceneName = "";
    static int targetSceneIndex;
    static Scene currentScene;
    static Scene newScene;
 
    /// <summary>
    /// Move a GameObject from the current scene to another scene.
    /// </summary>
    /// <param name="sceneName">Name of the scene you want to load.</param>
    /// <param name="targetGameObject">GameObject you want to move to the new scene.</param>
    static public void LoadScene(string sceneName, GameObject targetGameObject)
    {
        // set some globals
        targetObject = targetGameObject;
        targetSceneName = sceneName;
 
        // get the current active scene
        currentScene = SceneManager.GetActiveScene();
 
        // load the new scene in the background
        SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
 
        // Attach the SceneLoaded method to the sceneLoaded delegate.
        // SceneLoaded will be called when the new scene is loaded.
        SceneManager.sceneLoaded += SceneLoaded;
    }
    
    static public void LoadScene(int sceneIndex, GameObject targetGameObject)
    {
        // set some globals
        targetObject = targetGameObject;
        targetSceneIndex = sceneIndex;
 
        // get the current active scene
        currentScene = SceneManager.GetActiveScene();
 
        // load the new scene in the background
        SceneManager.LoadSceneAsync(targetSceneIndex, LoadSceneMode.Additive);
 
        // Attach the SceneLoaded method to the sceneLoaded delegate.
        // SceneLoaded will be called when the new scene is loaded.
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.activeSceneChanged += ChangedActiveScene;

    }

    private static void ChangedActiveScene(Scene arg0, Scene arg1)
    {
        targetObject.GetComponent<PictoGameManager>().OnNewScene();
        
    }

    /// <summary>
    /// After new scene loads, move GameObject from current scene to new scene.
    /// When finished, unload current scene. The new scene becomes current scene.
    /// </summary>
    /// <param name="newScene">New scene that was loaded.</param>
    /// <param name="loadMode">Mode that was used to load the scene.</param>
    static public void SceneLoaded(Scene newScene, LoadSceneMode loadMode)
    {
        // remove this method from the sceneLoaded delegate
        SceneManager.sceneLoaded -= SceneLoaded;
 
        // get the scene we just loaded into the background
        if (targetSceneName.Length > 0)
            newScene = SceneManager.GetSceneByName(targetSceneName);
        else
        {
            newScene = SceneManager.GetSceneByBuildIndex(targetSceneIndex);
        }
        // move the gameobject from scene A to scene B
        SceneManager.MoveGameObjectToScene(targetObject.gameObject, newScene);
 
        // unload scene A
        SceneManager.UnloadSceneAsync(currentScene);
        

    }
}