using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーンの切り替え等

/* TitleSceneでの基本動作クラス */
public class TitleMain : MainBase {

    // Scene切り替え時実行
    protected override void Start() {

        // 全Scene共通部
        base.Start();

        // サウンドのロード
        soundManager.Load(SoundManager.SOUND_TYPE.BGM, "bgm001");
        soundManager.Load(SoundManager.SOUND_TYPE.VOICE, "vo001");

        // サウンドの再生
        soundManager.Play(SOUND_TYPE.BGM, "bgm001", true);  // 基本BGMなのでループ
        soundManager.Play(SOUND_TYPE.VOICE, "vo001");
    }

    void Update() {

        switch (status) {

            // 未フェードイン時(シーン遷移時)
            case STATE.START:

                if(fadeManager.CheckFadeEnd() == true) {

                    status = STATE.PLAY;
                }
                break;

            // シーン中動作
            case STATE.PLAY:

                // Zキーを押したとき
                if (Input.GetKeyDown(KeyCode.Z)) {

                    soundManager.Play(SOUND_TYPE.SE, "se001");
                }

                // Xキーを押したとき
                if (Input.GetKeyDown(KeyCode.X)) {

                    soundManager.Play(SOUND_TYPE.VOICE, "vo001");
                }

                // スペースキーを押したとき
                if (Input.GetKeyDown(KeyCode.Space)) {

                    // フェードアウトの開始
                    fadeManager.FadeOutPlay();
                    soundManager.Play(SOUND_TYPE.SE, "se001");
                    status = STATE.CHANGE_WAIT;
                }
                break;

            // シーン遷移待ち状態
            case STATE.CHANGE_WAIT:

                // フェード終了時
                if(fadeManager.CheckFadeEnd() == true) {

                    // サウンドの破棄
                    Release();
                    // Sceneの遷移
                    SceneManager.LoadScene("GameScene");
                }
                break;
        }
    }

    //// TitleSceneでのみ扱うResourceの破棄メソッド ////
    void Release() {

        soundManager.Release(SoundManager.SOUND_TYPE.BGM, "bgm001");
        soundManager.Release(SoundManager.SOUND_TYPE.VOICE, "vo001");
    }
}
