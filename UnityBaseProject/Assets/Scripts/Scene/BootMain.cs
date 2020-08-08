using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーンの切り替え等

public class BootMain : MainBase {

    //// ゲームの基本設定 ////
    void Awake() {

        // vシンクを無効にする
        QualitySettings.vSyncCount = 0;
        // フレームレートを固定する
        Application.targetFrameRate = 60;
    }

    //// ゲーム内共通動作 ////
    protected override void Start() {

        // 基底クラスでの設定分を実行
        base.Start();

        // 汎用音の読み込み
        soundManager.CommonLoad();

        // Sceneの切り替え
        SceneManager.LoadScene("TitleScene");
    }
}
