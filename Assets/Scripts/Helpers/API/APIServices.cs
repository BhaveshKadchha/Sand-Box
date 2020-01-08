using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using System.IO;
using BestHTTP;
using System;
using System.Text;

public class APIServices : Singleton<APIServices>
{
    #region GET_METHOD
    /// <summary>
    /// Use when need HTTPresponse.
    /// </summary>
    public void Get(string getURL, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize = false, bool isReconnect = false)
    {
        StartCoroutine(I_GetService(getURL, OnServiceCallBack, shouldAuthorize, isReconnect));
    }

    /// <summary>
    /// Use when filling data in container.Pass type T.
    /// </summary>
    public void Get<T>(string getURL, T container, bool shouldAuthorize = false, Action actionOne = null)
    {
        StartCoroutine(I_GetService(getURL, container, shouldAuthorize, actionOne));
    }

    IEnumerator I_GetService(string getURL, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize, bool isReconnect)
    {
        HTTPRequest request = new HTTPRequest(new Uri(getURL), HTTPMethods.Get);

        if (shouldAuthorize)
            request.AddHeader("Authorization", APIs.Token);

        request.Send();
        yield return StartCoroutine(request);

        if (request.Response != null)
            OnServiceCallBack(request.Response);
        else
        {
            Debug.Log("<color=red>Error: </color> Response is NULL(GET)");
            if (isReconnect)
                Reconnect(getURL, OnServiceCallBack, shouldAuthorize, isReconnect);
        }
    }

    void Reconnect(string getURL, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize, bool isReconnect)
    {
        StartCoroutine(I_GetService(getURL, OnServiceCallBack, shouldAuthorize, isReconnect));
    }

    IEnumerator I_GetService<T>(string getURL, T container, bool shouldAuthorize, Action actionOne)
    {
        HTTPRequest request = new HTTPRequest(new Uri(getURL), HTTPMethods.Get);

        if (shouldAuthorize)
            request.AddHeader("Authorization", APIs.Token);

        request.Send();
        yield return StartCoroutine(request);

        if (request.Response != null)
            container = JsonUtility.FromJson<T>(request.Response.DataAsText);
        else
            Debug.Log("<color=red>Error: </color> Response is NULL(GET) :- Container_Empty");

        actionOne?.Invoke();
    }
    #endregion

    #region POST_METHOD
    public void Post(string URL, KVPList<string, string> data, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize = false)
    {
        StartCoroutine(I_PostService(URL, data, OnServiceCallBack, shouldAuthorize));
    }

    public void Post(string URL, string rawData, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize = false)
    {
        StartCoroutine(I_PostService(URL, rawData, OnServiceCallBack, shouldAuthorize));
    }

    IEnumerator I_PostService(string URL, KVPList<string, string> data, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize)
    {
        HTTPRequest request = new HTTPRequest(new System.Uri(URL), HTTPMethods.Post);

        if (shouldAuthorize)
            request.AddHeader("Authorization", APIs.Token);

        string m_body = "";
        for (int count = 0; count < data.Count; count++)
        {
            m_body = m_body + data[count].Key + ":" + data[count].Value + "\n";
            request.AddField(data[count].Key, data[count].Value);
        }
        request.Send();
        yield return StartCoroutine(request);

        if (request.Response != null)
            OnServiceCallBack(request.Response);
        else
            Debug.Log("<color=red>Error: </color> Response is NULL(POST)");
    }

    IEnumerator I_PostService(string URL, string rawData, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize)
    {
        HTTPRequest request = new HTTPRequest(new Uri(URL), HTTPMethods.Post);

        if (shouldAuthorize)
        {
            request.AddHeader("Authorization", APIs.Token);

            request.AddHeader("Content-Type", "application/json");
        }

        request.RawData = Encoding.UTF8.GetBytes(rawData);

        request.Send();
        yield return StartCoroutine(request);

        if (request.Response != null)
            OnServiceCallBack(request.Response);
        else
            Debug.Log("<color=red>Error: </color> Response is NULL(POST)");
    }
    #endregion

    #region DOWNLOAD_IMAGE
    /// <summary>
    /// Using BEST_HTTP.
    /// </summary>
    public Sprite DownloadImage(string ImageUrl)
    {
        Sprite S = null;

        new HTTPRequest(new Uri(ImageUrl), (request, response) =>
        {
            var tex = new Texture2D(0, 0);
            tex.LoadImage(response.Data);
            S = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }).Send();

        return S;
    }

    /// <summary>
    /// Using unity networking(WWW).
    /// </summary>
    public static IEnumerator DownloadThisImage(string imgUrl, Action<Texture2D> downloadedTexture)
    {
        string imgFileName = Path.GetFileNameWithoutExtension(imgUrl);
        string imgFolder = Application.persistentDataPath + "/img/";
        if (!Directory.Exists(imgFolder)) Directory.CreateDirectory(imgFolder);

        bool imgExists = false;
        if (File.Exists(imgFolder + imgFileName))
        {
            imgExists = true;
            imgUrl = "file://" + imgFolder + imgFileName;
        }

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("<color=red>Error: </color>" + www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            downloadedTexture.Invoke(myTexture as Texture2D);
            if (!imgExists)
                File.WriteAllBytes(Path.Combine(Application.persistentDataPath, imgFolder + imgFileName), www.downloadHandler.data);
        }
    }
    #endregion

    #region UPLOAD_IMAGE
    public void UploadImage(string PostURL, KVPList<string, byte[]> data, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize = true)
    {
        StartCoroutine(I_ImageUploadService(PostURL, data, OnServiceCallBack, shouldAuthorize));
    }

    IEnumerator I_ImageUploadService(string PostURL, KVPList<string, byte[]> data, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize)
    {
        HTTPRequest request = new HTTPRequest(new Uri(PostURL), HTTPMethods.Post);

        if (shouldAuthorize)
            request.AddHeader("Authorization", APIs.Token);

        string m_body = "";
        for (int count = 0; count < data.Count; count++)
        {
            m_body = m_body + data[count].Key + ":" + data[count].Value + "\n";
            request.AddBinaryData(data[count].Key, data[count].Value);
        }

        request.Send();
        yield return StartCoroutine(request);

        if (request.Response != null)
            OnServiceCallBack(request.Response);
        else
            Debug.Log("<color=red>Error: </color> Response is NULL(IMAGE_UPLOAD)");
    }
    #endregion

    #region MixedData
    public void PostMixedData(string URL, KVPList<string, string> data, KVPList<string, byte[]> rawdata, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize = true)
    {
        StartCoroutine(I_MixedPost(URL, data, rawdata, OnServiceCallBack, shouldAuthorize));
    }

    IEnumerator I_MixedPost(string PostURL, KVPList<string, string> data, KVPList<string, byte[]> rawdata, Action<HTTPResponse> OnServiceCallBack, bool shouldAuthorize)
    {
        HTTPRequest request = new HTTPRequest(new Uri(PostURL), HTTPMethods.Post);

        if (shouldAuthorize)
            request.AddHeader("Authorization", APIs.Token);

        string m_body = "";

        for (int count = 0; count < data.Count; count++)
        {
            m_body = m_body + data[count].Key + ":" + data[count].Value + "\n";
            Debug.Log("Data key " + data[count].Key + " data val " + data[count].Value);
            request.AddField(data[count].Key, data[count].Value);
        }

        for (int count = 0; count < rawdata.Count; count++)
        {
            m_body = m_body + rawdata[count].Key + ":" + rawdata[count].Value + "\n";
            request.AddBinaryData(rawdata[count].Key, rawdata[count].Value);
        }

        request.Send();
        yield return StartCoroutine(request);

        if (request.Response != null)
            OnServiceCallBack(request.Response);
        else
            Debug.Log("<color=red>Error: </color> Response is NULL(MIXED_POST)");
    }
    #endregion
}


public class KVPList<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
{
    public void Add(TKey Key, TValue Value)
    {
        base.Add(new KeyValuePair<TKey, TValue>(Key, Value));
    }
}