using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    //// インスタンスの作成
    [SerializeField] DebugManager debugManager = null;
    [SerializeField] Transform instanceTransform = null;

    // ロードしたエフェクトの格納用リスト
    Dictionary<string, GameObject> effectLoadList = new Dictionary<string, GameObject>();

    //// エフェクトのロードメソッド ////
    public void Load(string fileName) {

        GameObject effect = Resources.Load("Effect/" + fileName) as GameObject;

        if (effect == null) {

            debugManager.SetDebugLog("「" + fileName + "」のエフェクトがロード出来ません");
        }

        if (!effectLoadList.ContainsKey(fileName)) {

            effectLoadList.Add(fileName, effect);
        }
    }


    //// エフェクトの座標指定生成メソッド ////
    void CreateEffect(string fileName, Vector3 transform, bool loop = false) {

        if (effectLoadList.ContainsKey(fileName)) {

            // エフェクトのインスタンスを作成
            GameObject effectObject = Instantiate(effectLoadList[fileName], transform, Quaternion.identity);

            // EffectManager管理で作成
            effectObject.transform.SetParent(instanceTransform, false);

            // ループ指定の場合エフェクトは削除しない
            if (loop) return;

            // ループしない場合、デュレーションの時間が経過したら自動的に削除
            EffectControl effectControl = effectObject.GetComponent<EffectControl>();

            // エフェクトがループエフェクトか調べる
            if (effectControl.GetLoopFlag())
            {

                debugManager.SetDebugLog("「" + fileName + "」のエフェクトはループエフェクトの為削除できません");
                return;
            }

            // エフェクトの再生時間を超えたら自動的に削除
            Destroy(effectObject, effectControl.GetDuration());
        }
        // エフェクトがリストに存在しない時
        else {

            debugManager.SetDebugLog("「" + fileName + "」のエフェクトを作成出来ません");
        }

        return;
    }


    //// エフェクトを指定されたHierarchy内で生成するメソッド ////
    // クリエイトした各クラスでの管理(削除など)が必要
    public GameObject CreateParentEffect(string fileName, Transform transform) {

        GameObject effectObject = null;

        if (effectLoadList.ContainsKey(fileName)) {

            effectObject = Instantiate(effectLoadList[fileName], Vector3.zero, Quaternion.identity);

            // 指定されたGameObjectの子供として生成
            effectObject.transform.SetParent(transform, false);
        }
        else {

            debugManager.SetDebugLog("「" + fileName + "」のエフェクトを作成出来ません");
        }

        return effectObject;
    }


    //// instanceTranceform以下に配置されたエフェクトを全て消すメソッド ////
    // CreateParentEffectで生成されたオブジェクトは消去しない為、各自消去
    public void AllDestroyEffect() {

        foreach (Transform n in instanceTransform.transform) {

            GameObject.Destroy(n.gameObject);
            return;
        }
    }

    //// ロード済みリストに登録されたエフェクトを消すメソッド ////
    public void Release(string fileName) {

        if(effectLoadList.ContainsKey(fileName)){

            effectLoadList.Remove(fileName);
        }
    }
}
