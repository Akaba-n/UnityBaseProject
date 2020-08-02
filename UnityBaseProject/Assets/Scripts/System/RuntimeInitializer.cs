using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* アプリケーション起動時に実行されるメソッドのクラス */
public class RuntimeInitializer : MonoBehaviour {
    //// シーン読み込み前に実行するメソッド
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad() {
        // プレハブ化している管理用オブジェクト(SystemManager)をインスタンス化
        GameObject prefab = (GameObject)Resources.Load("Prefabs/System/SystemManager");
        GameObject systemManager = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        // 全シーンを通して削除されないように配置
        GameObject.DontDestroyOnLoad(systemManager);
    }    
}
