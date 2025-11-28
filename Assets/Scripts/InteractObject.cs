using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] public float FadeDuration = 1.0f;

    private Image fadeImage;
    private GameObject persistentRoot;

    public void Interact()
    {
        Debug.Log("Fade");

        // Find the fade overlay (must exist in the scene)
        if (fadeImage == null)
        {
            fadeImage = GameObject.Find("FadeOverlay")?.GetComponent<Image>();
        }

        if (fadeImage != null)
        {
            //keep this objects root during the new scene being loaded 
            persistentRoot = transform.root.gameObject;
            DontDestroyOnLoad(persistentRoot);

            StartCoroutine(FadeAndLoadScene());
        }
        else
        {
            Debug.LogWarning("No FadeOverlay found — loading scene instantly.");
            SceneManager.LoadScene("MainMenu");
        }

        //SceneManager.LoadScene("HouseTemp");
        Debug.Log("Interaction done debug");
    }

    private IEnumerator FadeAndLoadScene()
    {
        // fade to white 
        yield return StartCoroutine(Fade(0f, 1f));


        //Load the next scene
        // here i would like to write an if statement/statements to check the tag of the object that the interactor script is grabbing and depending on the tag, load a specific scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HouseTemp");

        //wait for the new scene to be fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //wait one extra frame for scene objects to load (FadeOverlay)
        yield return null;

        //find the fade overlay image in the new scene
        fadeImage = GameObject.Find("FadeOverlay")?.GetComponent<Image>();
        if (fadeImage != null )
        {
            //fade back from white
            yield return StartCoroutine(Fade(1f, 0f));
        }
        else
        {
            Debug.Log("Fadeoverlay missing in new scene");
        }

        Destroy(persistentRoot);

    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = Color.white;

        while (elapsed < FadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / FadeDuration);
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = c;
            yield return null;
        }

        c.a = endAlpha;
        fadeImage.color = c;
    }
}
