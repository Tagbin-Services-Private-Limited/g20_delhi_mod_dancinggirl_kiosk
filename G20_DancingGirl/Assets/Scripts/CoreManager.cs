using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CoreManager : MonoBehaviour
{

    public int currentLang = 1;
    [SerializeField] Image language_btn;
    [SerializeField] Sprite[] alLanguages_sprites;


    [SerializeField] Texture2D image_content;
    [SerializeField] RawImage contentDisplay;

   // [SerializeField] Texture2D image_title;
  //  [SerializeField] RawImage contentTitleDisplay;

    [SerializeField] VideoPlayer videoPlayer;

    [SerializeField] string videoName;

    private void Start()
    {
        SetData();
        videoPlayer.url = Application.streamingAssetsPath + "/" + videoName;
        videoPlayer.Play();
    }

    #region Lang Manager
    public void OpenLangaugePanel()
    {
        ToggleLangPopup(true);
       
    }
    public void ChangeLanguage(int index)
    {

        ToggleLangPopup(false);

        if (currentLang == index) return;
        currentLang = index;


        language_btn.sprite = alLanguages_sprites[currentLang - 1];
       

       // StartCoroutine(GetAudioClip(true));

        SetData();

    }

    private void SetData()
    {
        ToggleLangPopup(false);

        if (image_content != null)
        {
            DestroyImmediate(image_content);
        }

        image_content = ImageLoader(currentLang.ToString() + "/Content.png");
        contentDisplay.texture = image_content;

        //contentDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(image_content.width, image_content.height);

       /* if (image_title != null)
        {
            DestroyImmediate(image_title);
        }

        image_title = ImageLoader(currentLang.ToString() + "/Heading.png");
        contentTitleDisplay.texture = image_title;
        contentTitleDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(image_title.width, image_title.height);*/
    }
    Texture2D ImageLoader(string _fileName)
    {
        if (string.IsNullOrEmpty(_fileName)) return null;
        //Create an array of file paths from which to choose
        var folderPath = Application.streamingAssetsPath + "/" + _fileName;  //Get path of folder

        if (!File.Exists(folderPath))
        {
            Debug.Log("FILE NO EXISTS");


            folderPath = folderPath.Substring(0, folderPath.Length - 4) + ".jpg";
            Debug.Log(folderPath);
        }

        Texture2D tex = new Texture2D(1, 1);

        //Converts desired path into byte array
        if (File.Exists(folderPath))
        {
            byte[] pngBytes = File.ReadAllBytes(folderPath);
            tex.LoadImage(pngBytes);
        }

        //Creates texture and loads byte array data to create image



        return tex;
    }

    [SerializeField] GameObject lang_popup;
    private void ToggleLangPopup(bool _enabled)
    {
        LeanTween.cancel(lang_popup);
        if (_enabled)
        {
            StopCoroutine(nameof(closeAutomaticLangPopup));
            StartCoroutine(nameof(closeAutomaticLangPopup));
            lang_popup.SetActive(true);
            LeanTween.alphaCanvas(lang_popup.GetComponent<CanvasGroup>(), 1, 0.3f);
            LeanTween.move(lang_popup.GetComponent<RectTransform>(), new Vector3(0, 109, 0), 0.3f);
        }
        else
        {
            StopCoroutine(nameof(closeAutomaticLangPopup));

            LeanTween.alphaCanvas(lang_popup.GetComponent<CanvasGroup>(), 0, 0.3f).setOnComplete(() => {
                lang_popup.SetActive(false);
            });
            LeanTween.move(lang_popup.GetComponent<RectTransform>(), new Vector3(0, -300, 0), 0.3f);
        }
    }

    IEnumerator closeAutomaticLangPopup()
    {
        yield return new WaitForSeconds(6f);
        ToggleLangPopup(false);
    }
    #endregion

    public void ButtonClickANimation(GameObject go)
    {
        LeanTween.cancel(go);
        LeanTween.scale(go, Vector3.one * 0.8f, 0.15f).setFrom(Vector3.one).setEasePunch();
    }
}
