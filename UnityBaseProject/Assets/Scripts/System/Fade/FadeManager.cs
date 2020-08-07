using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* シーン切り替え時フェードクラス */
public class FadeManager : MonoBehaviour {

    enum FADE {
        STAY,
        IN,
        OUT
    };

    [SerializeField] private Image image = null;

    private FADE state = FADE.STAY;

    private float time = 0f;
    private float fadeTime = 0.5f;  // かける時間


    //// フェードアウト実行用メソッド ////
    public void FadeOutPlay() {

        // 待機状態(フェード実行前)の時
        if (state == FADE.STAY) {

            time = 0f;
            state = FADE.OUT;
        }
    }

    //// フェードイン実行用メソッド ////
    public void FadeInPlay() {

        // 待機状態(フェード実行前)の時
        if (state == FADE.STAY) {

            time = fadeTime;
            state = FADE.IN;
        }
    }


    //// 実際の動作 ////
    void Update() {

        // フェード実行メソッド実行済み
        if (state != FADE.STAY) {

            // フェードインとフェードアウトで逆動作
            if(state == FADE.OUT) { time += Time.deltaTime; }
            else if(state == FADE.IN) { time -= Time.deltaTime; }

            // 今回はフェードがUIの透明度でのフェードなのでこの動作
            float a = time / fadeTime;
            var color = image.color;
            color.a = a;
            image.color = color;

            // フェード終了
            if(state == FADE.OUT && color.a >= 1 || state == FADE.IN && color.a <= 0) {

                state = FADE.STAY;
            }
        }
    }


    //// フェード終了判定メソッド ////
    public bool CheckFadeEnd() {

        // STAY(動作外)時：true
        if (state == FADE.STAY) { return true; }
        // INorOUT(動作中)時：false
        else { return false; }
    }
}
