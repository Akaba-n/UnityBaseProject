using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 全サウンドタイプ統一管理クラス */
public class SoundManager : GameDefine {

    // 各サウンド再生クラスのインスタンス化
    [SerializeField] BgmPlayer bgmPlayer     = null;
    [SerializeField] SePlayer sePlayer       = null;
    [SerializeField] VoicePlayer voicePlayer = null;

    // サウンド読み込みメソッド
    public void Load(SOUND_TYPE soundType, string fileName) {

        switch (soundType) {
            case SOUND_TYPE.BGM:
                bgmPlayer.Load(fileName);
                break;
            case SOUND_TYPE.SE:
                sePlayer.Load(fileName);
                break;
            case SOUND_TYPE.VOICE:
                voicePlayer.Load(fileName);
                break;
        }
    }

    // ゲーム全体で使用する汎用音の読み込み(今回はSEにしかない)
    public void CommonLoad() {

        sePlayer.CommonLoad();
    }

    // サウンド再生用メソッド(BGMの時のみループ指定、デフォではfalse)
    public void Play(SOUND_TYPE soundType, string fileName, bool loop = false) {

        switch (soundType) {
            case SOUND_TYPE.BGM:
                bgmPlayer.Play(fileName, loop);
                break;
            case SOUND_TYPE.SE:
                sePlayer.Play(fileName);
                break;
            case SOUND_TYPE.VOICE:
                voicePlayer.Play(fileName);
                break;
        }
    }

    // BGM再生確認メソッド
    public bool IsPlayBGM() {

        return bgmPlayer.IsPlay();
    }

    // サウンド停止メソッド
    public void Stop(SOUND_TYPE soundType) {

        switch (soundType) {
            case SOUND_TYPE.BGM:
                bgmPlayer.Stop();
                break;
            case SOUND_TYPE.SE:
                sePlayer.AllStop();
                break;
            case SOUND_TYPE.VOICE:
                voicePlayer.AllStop();
                break;
        }
    }

    // 音量調整メソッド
    public void SetVolume(SOUND_TYPE soundType, float volume) {

        switch (soundType) {
            case SOUND_TYPE.BGM:
                bgmPlayer.SetVolume();
                break;
            case SOUND_TYPE.SE:
                sePlayer.SetVolume();
                break;
            case SOUND_TYPE.VOICE:
                voicePlayer.SetVolume();
                break;
        }
    }

    // ロード済み音声破棄メソッド
    public void Release(SOUND_TYPE soundType, string fileName) {

        switch (soundType) {
            case SOUND_TYPE.BGM:
                bgmPlayer.Release(fileName);
                break;
            case SOUND_TYPE.SE:
                sePlayer.Release(fileName);
                break;
            case SOUND_TYPE.VOICE:
                voicePlayer.Release(fileName);
                break;
        }
    }

    // ロード済み汎用音声破棄メソッド
    public void CommonRelease() {

        sePlayer.CommonRelease();
    }
}
