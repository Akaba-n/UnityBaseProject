using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour {

    [SerializeField] public ParticleSystem particleControl = null;

    //// 再生時間確認メソッド ////
    public float GetDuration() {

        // エフェクトをインスタンス化している時
        if (particleControl != null) { return particleControl.main.duration; }
        // していない時
        else { return 0f; }
    }


    //// ループフラグ ////
    public bool GetLoopFlag() {

        return particleControl.main.loop;
    }
}
