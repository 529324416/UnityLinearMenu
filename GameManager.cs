using System;
using UnityEngine;


public class GameManager: MonoBehaviour{

    public GameObject pfb;
    public int count;
    public Sprite icon;
    private LinearMenuTest menu;

    void Start(){

        LinearMenuRenderer renderer = FindObjectOfType<LinearMenuRenderer>();
        renderer.init(pfb, count);

        menu = new LinearMenuTest(
            renderer, 
            new string[]{"选项1","选项2","选项3", "选项4"},
            new Sprite[]{icon, icon, icon, icon},
            (index) => {
                Debug.Log($"用户选择了第{index.ToString()}项");
            }
        );
    }
    void Update(){
        menu.onUpdate();
    }
}