using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*---------- FPS表示用クラス ----------*/
public class FpsRender : MonoBehaviour {

    private int screenLongSide;
    private Rect boxRect;
    private GUIStyle style = new GUIStyle();

    private int frameCount;
    private float oldTime;
    private double frameRate;

    private void Awake() {

        // 開始時間を格納
        oldTime = Time.realtimeSinceStartup;
    }

    private void Update() {

        frameCount++;
        // 経過時間の計測
        var time = Time.realtimeSinceStartup - oldTime;

        // 約0.5秒経っていなければ終了
        if (time < 0.5f) { return; }

        // 0.5秒経っていればFPSを求めてリセット
        frameRate = System.Math.Round(frameCount / time, 1, System.MidpointRounding.AwayFromZero);
        oldTime = Time.realtimeSinceStartup;
        frameCount = 0;

        UpdateUISize();
    }

    ////// 画面へ描画する大きさ関連の設定 //////
    private void UpdateUISize() {

        // 画面の縦と横の大きいほうを取得
        screenLongSide = Mathf.Max(Screen.width, Screen.height);
        // 表示領域の設定
        var rectLongSide = screenLongSide / 10; // 表示領域の幅
        boxRect = new Rect(1, 1, rectLongSide, rectLongSide / 3);   // 表示領域の範囲
        style.fontSize = (int)(screenLongSide / 36.8);  // 表示されるフォントサイズ
        style.normal.textColor = Color.white;   // フォントの色
    }

    ////// 画面への描画メソッド //////
    private void OnGUI() {

        GUI.Box(boxRect, "");
        GUI.Label(boxRect, " " + frameRate + "fps", style);
    }
}
