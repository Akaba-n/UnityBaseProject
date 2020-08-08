using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* BGM関連操作クラス */
public class BgmPlayer : MonoBehaviour {

    // デバッグ用
    [SerializeField] DebugManager debugManager = null;
    // 再生用オーディオソースの作成
    [SerializeField] AudioSource audioSource = null;
    
    // ロードしたBGMの格納リスト
    Dictionary<string, AudioClip> bgmList = new Dictionary<string, AudioClip>();


    // BGMロードメソッド
    public void Load(string fileName) {

        AudioClip audio = Resources.Load("Sounds/Bgm/" + fileName) as AudioClip; // as:AudioClipへのキャスト

        // ロード失敗時エラーログ(デバッグ用)
        if(audio == null) {

            debugManager.SetDebugLog("「" + fileName + "」のBGMファイルがロード出来ません");
        }

        // 未ロードの時リストに追加(ロード)
        if (!bgmList.ContainsKey(fileName)) {

            bgmList.Add(fileName, audio);
        }
    }

    // BGMの再生メソッド
    public void Play(string fileName, bool loop) {

        // 既ロード時再生
        if (bgmList.ContainsKey(fileName)) {

            audioSource.clip = bgmList[fileName];
            audioSource.loop = loop;
            audioSource.Play();
        }
        // 未ロード時エラーログ(デバッグ用)
        else {

            debugManager.SetDebugLog("「" + fileName + "」のBGMファイルを再生出来ません");
        }
    }

    // BGM再生判定メソッド
    public bool IsPlay() {

        return audioSource.isPlaying;
    }

    // BGMの停止メソッド
    public void Stop() {

        audioSource.Stop();
    }

    // BGMの音量調整メソッド
    public void SetVolume(float volume) {

        audioSource.volume = volume;
    }

    // ロード済みBGMの破棄メソッド
    public void Release(string fileName) {

        audioSource.Stop();

        if (bgmList.ContainsKey(fileName)) {

            bgmList.Remove(fileName);
        }
    }
}
