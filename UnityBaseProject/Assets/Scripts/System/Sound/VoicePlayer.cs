using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour {

    private static int VOICE_MAX_NUM = 8;   // voiceのチャンネル数

    [SerializeField] DebugManager debugManager = null;

    // 同時最大再生数を8とする
    [SerializeField] AudioSource[] audioSource = new AudioSource[VOICE_MAX_NUM];

    Dictionary<string, AudioClip> voiceList = new Dictionary<string, AudioClip>();

    //// ファイル読み込みメソッド ////
    public void Load(string fileName) {

        AudioClip audio = Resources.Load("Sounds/Voice/" + fileName) as AudioClip;

        // AudioClipが用意できなかった又はロード失敗時
        if (audio == null) {

            debugManager.SetDebugLog("「" + fileName + "」のボイスファイルがロード出来ません");
        }

        // 既に登録されていなければ、リストに登録する
        if (!voiceList.ContainsKey(fileName)) {

            voiceList.Add(fileName, audio);
        }
    }

    //// Voice再生メソッド ////
    public void Play(string fileName) {

        // ロードしているリストの中に存在しているか確認
        if (voiceList.ContainsKey(fileName)) {

            // 使用可能なオーディオソースを確認
            int playAudioNum = GetPlayAudioNumber();

            if (playAudioNum <= -1) {

                debugManager.SetDebugLog("空きオーディオが無いためボイスを再生できません");
            }
            else {

                audioSource[playAudioNum].clip = voiceList[fileName];
                audioSource[playAudioNum].Play();
            }
        }
        else {

            debugManager.SetDebugLog("「" + fileName + "」のボイスファイルがありません");
        }
    }

    void Update() {

        // 再生が終了したらAudioClipを空にする
        for (int i = 0; i < VOICE_MAX_NUM; i++) {

            if(audioSource[i].time == 0 && audioSource[i].isPlaying == false) {

                audioSource[i].clip = null;
            }
        }
    }

    //// 全てのVoiceを停止するメソッド ////
    public void AllStop() {

        for (int i = 0; i < VOICE_MAX_NUM; i++) {

            audioSource[i].Stop();
        }
    }

    //// 再生可能なオーディオソースがあるか調べるメソッド ////
    // 再生可能の時一番小さなオーディオソースの番号が、再生不可の時-1が返る
    private int GetPlayAudioNumber() {

        for (int i = 0; i < VOICE_MAX_NUM; i++) {

            if (audioSource[i].time == 0 && audioSource[i].isPlaying == false) {

                return i;
            }
        }
        return -1;
    }

    //// ボリュームをセットするメソッド ////
    public void SetVolume(float volume) {

        for (int i = 0; i < VOICE_MAX_NUM; i++) {

            audioSource[i].volume = volume;
        }
    }

    //// 読み込んだファイルの破棄メソッド ////
    public void Release(string fileName) {

        AllStop();

        if (voiceList.ContainsKey(fileName)) {

            voiceList.Remove(fileName);
        }
    }
}
