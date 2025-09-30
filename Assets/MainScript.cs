using System;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Image[] ImgDestination;
    // public GameObject[] FrameShadows;
    public TMP_InputField IPAddInput;
    public TMP_Text PathLocationText;
    public ParticleSystem[] TopParticles;
    public ParticleSystem[] BottomParticles;
    public GameObject Glow1;
    public GameObject Glow2;
    public GameObject Glow3;
    public GameObject Glow4;
    public GameObject Glow5;
    public GameObject Glow6;
    public GameObject Glow7;
    public GameObject Glow8;

    string imagespath;
    FileInfo[] LastFiles;
    float fadeDuration = 1f;
    int repeatCount = 5;
    bool[] isGlowing;

    void Start()
    {

        string fotomokopath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Fotomoko2");
        imagespath = Path.Combine(fotomokopath, "folder_name");
        // imagespath = @"\\192.168.3.156\Users\Avo\Documents\Fotomoko2\folder_name";

        PathLocationText.text = "Path Location: " + imagespath;

        print("Current Path: " + imagespath);

        if (!Directory.Exists(imagespath))
        {
            Directory.CreateDirectory(imagespath);
        }

        isGlowing = new bool[]
        {
            false, false, false, false, false, false, false, false, false
        };

        for (int i = 0; i < TopParticles.Length; i++)
        {
            BottomParticles[i].Stop();
            TopParticles[i].Stop();
        }

        StartCoroutine(DisableGlow());

        StartCoroutine(ImageChecker());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Cursor.visible = false;

            print("Cursor Hidden");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Cursor.visible = true;

            print("Cursor Visible");
        }
    }

    IEnumerator DisableGlow()
    {
        Image targetImage1 = Glow1.GetComponent<Image>();
        Image targetImage2 = Glow2.GetComponent<Image>();
        Image targetImage3 = Glow3.GetComponent<Image>();
        Image targetImage4 = Glow4.GetComponent<Image>();
        Image targetImage5 = Glow5.GetComponent<Image>();
        Image targetImage6 = Glow6.GetComponent<Image>();
        Image targetImage7 = Glow7.GetComponent<Image>();
        Image targetImage8 = Glow8.GetComponent<Image>();

        targetImage1.color = new Color(targetImage1.color.r, targetImage1.color.g, targetImage1.color.b, 0f);
        targetImage2.color = new Color(targetImage2.color.r, targetImage2.color.g, targetImage2.color.b, 0f);
        targetImage3.color = new Color(targetImage3.color.r, targetImage3.color.g, targetImage3.color.b, 0f);
        targetImage4.color = new Color(targetImage4.color.r, targetImage4.color.g, targetImage4.color.b, 0f);
        targetImage5.color = new Color(targetImage5.color.r, targetImage5.color.g, targetImage5.color.b, 0f);
        targetImage6.color = new Color(targetImage6.color.r, targetImage6.color.g, targetImage6.color.b, 0f);
        targetImage7.color = new Color(targetImage7.color.r, targetImage7.color.g, targetImage7.color.b, 0f);
        targetImage8.color = new Color(targetImage8.color.r, targetImage8.color.g, targetImage8.color.b, 0f);

        yield return null;
    }

    IEnumerator ImageChecker()
    {
        while (true)
        {
            LoadImages();
            yield return new WaitForSeconds(2f);
        }
    }

    // void LoadImages()
    // {
    //     // checks all the png files sa folder ng imagespath
    //     var files = new DirectoryInfo(imagespath)
    //     .GetFiles("*.png")
    //     .OrderByDescending(f => f.LastWriteTime)
    //     .Take(ImgDestination.Length)
    //     .ToArray();

    //     if (LastFiles == null || !files.SequenceEqual(LastFiles))
    //     {
    //         for (int i = 0; i < files.Length; i++)
    //         {
    //             // ImgDestination[i].sprite = LoadSprite(files[i].FullName);
    //             StartCoroutine(FadeImage(ImgDestination[i], LoadSprite(files[i].FullName), 0.5f));
    //         }
    //         LastFiles = files;
    //     }
    // }
    void LoadImages()
    {
        var files = new DirectoryInfo(imagespath)
            .GetFiles("*.png")
            .OrderByDescending(f => f.LastWriteTime)
            .Take(ImgDestination.Length)
            .ToArray();

        // First time: just load without fade
        if (LastFiles == null)
        {
            for (int i = 0; i < files.Length; i++)
                ImgDestination[i].sprite = LoadSprite(files[i].FullName);

            LastFiles = files;
            return;
        }

        // Check if something new appeared or changed
        for (int i = 0; i < files.Length; i++)
        {
            if (i >= LastFiles.Length || files[i].FullName != LastFiles[i].FullName)
            {
                // Only fade if the file is different
                StartCoroutine(FadeImage(ImgDestination[i], LoadSprite(files[i].FullName), 0.5f));
            }
        }

        LastFiles = files;
    }

    Sprite LoadSprite(string path)
    {
        byte[] filedata = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(filedata);

        // Crop from each border
        int cropX = (int)(tex.width * 0.1f);
        int cropY = (int)(tex.height * 0.2f);
        int cropWidth = tex.width - (cropX * 2);
        int cropHeight = tex.height - (cropY * 2);

        Rect CropRect = new Rect(cropX, cropY, cropWidth, cropHeight);

        return Sprite.Create(tex, CropRect, new Vector2(0.5f, 0.5f));
    }

    IEnumerator FadeImage(Image img, Sprite newSprite, float duration = 0.5f)
    {
        if (img == null) yield break;

        // Fade out
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = 1 - (t / duration);
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            yield return null;
        }

        // Swap sprite
        img.sprite = newSprite;

        // Fade in
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = t / duration;
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            yield return null;
        }

        // Make sure it’s fully visible
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    }

    public void UpdateIPAdd(string ipadd)
    {
        ipadd = IPAddInput.text;
        if (string.IsNullOrWhiteSpace(ipadd)) return;

        // string imagespath1 = $@"\\{ipadd}\Users\Avo\Documents\Fotomoko2\folder_name";
        string imagespath1 = $@"\\{ipadd}\Fotomoko2\folder_name";

        if (Directory.Exists(imagespath1))
        {
            imagespath = imagespath1;
            PathLocationText.text = "Path Location: " + imagespath;
            print("Updated Path: " + imagespath);
        }
        else
        {
            PathLocationText.text = "Path Location Not Found: " + imagespath1;
        }
    }


    public void onGlowButton(int glowNumber)
    {

        if (isGlowing[glowNumber]) return;

        TopParticles[glowNumber-1].Play();
        BottomParticles[glowNumber-1].Play();

        if (glowNumber == 1)
        {
            StartCoroutine(GlowFadeLoop(Glow1, 1));
        }
        else if (glowNumber == 2)
        {
            StartCoroutine(GlowFadeLoop(Glow2, 2));
        }
        else if (glowNumber == 3)
        {
            StartCoroutine(GlowFadeLoop(Glow3, 3));
        }
        else if (glowNumber == 4)
        {
            StartCoroutine(GlowFadeLoop(Glow4, 4));
        }
        else if (glowNumber == 5)
        {
            StartCoroutine(GlowFadeLoop(Glow5, 5));
        }
        else if (glowNumber == 6)
        {
            StartCoroutine(GlowFadeLoop(Glow6, 6));
        }
        else if (glowNumber == 7)
        {
            StartCoroutine(GlowFadeLoop(Glow7, 7));
        }
        else if (glowNumber == 8)
        {
            StartCoroutine(GlowFadeLoop(Glow8, 8));
        }
    }

    private IEnumerator GlowFadeLoop(GameObject GlowObject, int count)
    {
        isGlowing[count] = true;

        for (int i = 0; i < repeatCount; i++)
        {
            // Fade in
            yield return StartCoroutine(GlowFade(0f, 1f, GlowObject));
            // Fade out
            yield return StartCoroutine(GlowFade(1f, 0f, GlowObject));
        }

        TopParticles[count-1].Stop();
        BottomParticles[count-1].Stop();
        isGlowing[count] = false;
    }

    private IEnumerator GlowFade(float startAlpha, float endAlpha, GameObject GlowObject)
    {
        Image targetImage = GlowObject.GetComponent<Image>();

        float elapsed = 0f;
        Color color = targetImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            color.a = newAlpha;
            targetImage.color = color;
            yield return null;
        }

        // Ensure final alpha is set
        color.a = endAlpha;
        targetImage.color = color;
    }

    public void onExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
