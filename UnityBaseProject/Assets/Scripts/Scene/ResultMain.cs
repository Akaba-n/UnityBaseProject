using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーンの切り替え等

/* ResultSceneの基本動作クラス */
public class ResultMain : MainBase {

    //// Scene遷移時動作 ////
    protected override void Start() {

        // 全Scene共通部分
        base.Start();

        // サウンドのロード
        soundManager.Load(SoundManager.SOUND_TYPE.BGM, "bgm003");
        soundManager.Load(SoundManager.SOUND_TYPE.VOICE, "vo002");

        // サウンドの再生
        soundManager.Play(SOUND_TYPE.BGM, "bgm003");
        soundManager.Play(SOUND_TYPE.VOICE, "vo002");
    }

    void Update() {

        switch (status) {

            // 未フェードイン時
            case STATE.START:

                // フェードインが完了したら
                if(fadeManager.CheckFadeEnd() == true) {

                    status = STATE.PLAY;
                }
                break;

            // Scene中動作
            case STATE.PLAY:

                // スペースキーを押したとき
                if (Input.GetKeyDown(KeyCode.Space)) {

                    fadeManager.FadeOutPlay();
                    soundManager.Play(SOUND_TYPE.VOICE, "vo002");
                    status = STATE.CHANGE_WAIT;
                }
                break;

            // Scene遷移待機時
            case STATE.CHANGE_WAIT:

                if(fadeManager.CheckFadeEnd() == true) {

                    // Resourceの破棄
                    Release();
                    // Sceneの遷移
                    SceneManager.LoadScene("TitleScene");
                }
                break;
        }
    }

    //// ResultSceneでのみ扱うResourceの破棄メソッド ////
    void Release() {

        // サウンドの破棄
        soundManager.Release(SOUND_TYPE.BGM, "bgm003");
        soundManager.Release(SOUND_TYPE.VOICE, "vo002");
    }
}
