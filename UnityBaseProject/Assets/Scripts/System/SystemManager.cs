using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* システム管理系オブジェクトの管理用 */
public class SystemManager : GameDefine {

    // システム管理オブジェクトのインスタンス
    public static SystemManager instance;

    // 各システム管理系オブジェクト(増やすたびに追加)
    public SoundManager soundManager   = null;
    public EffectManager effectManager = null;
    public FadeManager fadeManager     = null;
    public DebugManager debugManager   = null;

    void Awake() {
        // 既に存在しているなら削除
        if(instance != null) {
            Destroy(gameObject);
        }
        // 存在していないなら指定する
        else {
            instance = this;
        }

        // シーン遷移では破棄させない
        DontDestroyOnLoad(gameObject);


        //// bootシーンのみで読み込み
        // 全シーンで使用するサウンドの読み込み
        soundManager.CommonLoad();
        // vシンクの無効化
        QualitySettings.vSyncCount = 0;
        // フレームレートの固定
        Application.targetFrameRate = MAX_FPS;
    }
}
