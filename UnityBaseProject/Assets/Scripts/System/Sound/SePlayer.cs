using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SePlayer : MonoBehaviour
{

    private static int SE_MAX_NUM = 8;  // チャンネル数の定義

    // DebugManager
    [SerializeField] DebugManager debugManager = null;

    // 同時最大再生数を8とする
    [SerializeField] AudioSource[] audioSource = new AudioSource[SE_MAX_NUM];

    Dictionary<string, AudioClip> seList = new Dictionary<string, AudioClip>();


    //// ファイルのロードメソッド ////
    public void Load(string fileName)
    {

        // 指定したファイルのロード
        AudioClip audio = Resources.Load("Sounds/Se/" + fileName) as AudioClip;

        // ロードできなかった時
        if (audio == null)
        {

            debugManager.SetDebugLog("「" + fileName + "」のSEファイルがロード出来ません");
        }

        // 未登録時、リストに追加する
        if (!seList.ContainsKey(fileName))
        {

            seList.Add(fileName, audio);
        }
    }

    //// 全汎用SEをロードメソッド ////
    public void CommonLoad()
    {

        // 汎用SEのロード
        Object[] audio = Resources.LoadAll("Sound/Se/Common", typeof(AudioClip));

        // 汎用SEの格納
        foreach (AudioClip clip in audio)
        {

            if (audio == null)
            {

                debugManager.SetDebugLog("汎用SEファイルがロード出来ません");
            }

            // 未登録時、リストに追加する
            if (!seList.ContainsKey(clip.name))
            {

                seList.Add(clip.name, clip);
            }
        }
    }

    //// SE再生メソッド ////
    public void Play(string fileName)
    {

        // ロードしているリストの中に存在しているか確認
        if (seList.ContainsKey(fileName))
        {

            // 使用可能なオーディオソースを確認
            // 無い時は-1を返す
            int playAudioNum = GetPlayAudioNumber();

            // 空きオーディオが無い時
            if (playAudioNum <= -1)
            {

                debugManager.SetDebugLog("空きオーディオが無いためSEを再生出来ません");
            }
            else
            {

                audioSource[playAudioNum].clip = seList[fileName];
                audioSource[playAudioNum].Play();
            }
        }
        // リストに存在していない時
        else
        {

            debugManager.SetDebugLog("「" + fileName + "」のSEファイルを再生出来ません");
        }
    }

    //// 全てのSEを停止するメソッド ////
    public void AllStop()
    {

        for (int i = 0; i < SE_MAX_NUM; i++)
        {

            audioSource[i].Stop();
        }
    }

    void Update()
    {

        // 再生が終了したらClipを空きにする
        for (int i = 0; i < SE_MAX_NUM; i++)
        {

            if (audioSource[i].time == 0 && audioSource[i].isPlaying == false)
            {

                audioSource[i].clip = null;
            }
        }
    }

    //// 全チャンネルボリューム変更メソッド ////
    public void SetVolume(float volume)
    {

        for (int i = 0; i < SE_MAX_NUM; i++)
        {

            audioSource[i].volume = volume;
        }
    }

    //// 再生可能なオーディオソースがあるか調べるメソッド ////
    // 可能なオーディオソースがある時その番号を、無い時-1を返す
    private int GetPlayAudioNumber()
    {

        for (int i = 0; i < SE_MAX_NUM; i++)
        {

            if (audioSource[i].time == 0 && audioSource[i].isPlaying == false)
            {

                return i;
            }
        }
        return -1;
    }

    //// ロード済みSEを破棄するメソッド ////
    public void Release(string fileName)
    {

        // 全てのSEを停止
        AllStop();

        // 指定したSEの破棄
        if (seList.ContainsKey(fileName))
        {

            seList.Remove(fileName);
        }
    }

    //// ロード済み汎用SEを破棄する(ロードし直す)メソッド ////
    public void CommonRelease() {

        AllStop();

        // 汎用SEのロード
        Object[] audio = Resources.LoadAll("Sound/Se/Common", typeof(AudioClip));

        // 汎用SEの格納
        foreach (AudioClip clip in audio) {

            if (audio == null) {

                debugManager.SetDebugLog("汎用SEファイルがロード出来ません");
            }

            // 未登録時、リストに追加する
            if (!seList.ContainsKey(clip.name)) {

                seList.Add(clip.name, clip);
            }
        }
    }
}
