using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////// デバッグ(開発)用クラス //////
public class DebugManager : MonoBehaviour {

    [SerializeField] GameObject fpsRenderSystem = null;

    public bool LogRender = false;
    public bool FpsRender = false;

    public void SetDebugLog(string text) {

        if (LogRender == true) {

            Debug.Log(text);
        }
    }

    public void Update() {

        if (FpsRender == true) {

            if (fpsRenderSystem.activeSelf != FpsRender) {

                fpsRenderSystem.SetActive(FpsRender);
            }
        }
    }
}
