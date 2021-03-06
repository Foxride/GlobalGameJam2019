﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class WebCamScript : MonoBehaviour
{
    private WebCamTexture webCamTexture;

    public const string FileName = "Face.png";

    private string persistentDataPath;
    
    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();

        persistentDataPath = Application.persistentDataPath;
            
        StartCoroutine(WriteTextureToFile());
    }

    public IEnumerator WriteTextureToFile()
    {
        Debug.Log(PathToFile);
        
        var photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        byte[] bytes = photo.EncodeToPNG();
        File.WriteAllBytes(PathToFile, bytes);
        yield break;
    }

    public string PathToFile => $"{persistentDataPath}/{FileName}";
}
