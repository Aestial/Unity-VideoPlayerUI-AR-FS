using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RA.Video
{
    public class VideoDownload : Singleton<VideoDownload>
    {
        public string filePath;

        [SerializeField] string directoryName;
        //[SerializeField] string serverPath;
        [SerializeField] string fileName;

        string localPath;
        //string url;

        void Awake()
        {
            //localPath = Path.Combine(Application.persistentDataPath, directoryName);
            filePath = Path.Combine(Application.persistentDataPath, fileName);
            //url = Path.Combine(serverPath, fileName);
        }

        void Update()
        {

        }

        public IEnumerator DownloadCoroutine(string url)
        {
            Debug.Log("Downloading video :" + url);

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log("VideoDownload - Error: " + webRequest.error);
                }
                else
                {
                    File.WriteAllBytes(filePath, webRequest.downloadHandler.data);
                    Debug.Log("VideoDonload - Video saved: " + filePath);
                }
                webRequest.Dispose();
            }
            yield return null;
        }
    }
}
