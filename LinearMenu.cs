using System;
using UnityEngine;


public class LinearMenuCore{

    public int selectionIndex{get; protected set;}
    public int count{get; protected set;}
    protected Action<int> onMoved;
    
    public LinearMenuCore(int count, Action<int> onMoved = null){

        this.count = count;
        this.onMoved = onMoved;
        this.selectionIndex = 0;
    }
    protected void moveCallback(){
        if(onMoved != null)onMoved(selectionIndex);
    }
    public void moveLast(){
        /* 移动到上一个选项，如果已经是第一个选项，则跳到最后一个选项 */

        selectionIndex = selectionIndex > 0 ? selectionIndex - 1: count - 1;
        moveCallback();
    }
    public void moveNext(){
        /* 移动到下一个选项，如果已经是最后一个选项，则跳到第一个选项 */

        selectionIndex = selectionIndex < count - 1 ? selectionIndex + 1: 0;
        moveCallback();
    }
    public void setSelectionIndex(int index, bool shouldCallback = true){

        int lastIndex = selectionIndex;
        selectionIndex = Math.Clamp(index, 0, count - 1);
        if(lastIndex != selectionIndex && shouldCallback){
            moveCallback();
        }
    }
}


public abstract class LinearMenuBase{

    protected LinearMenuCore core;
    protected Action<int> chooseCallback;

    public LinearMenuBase(int count, Action<int> chooseCallback = null){
        core = new LinearMenuCore(count, onMoved);
        this.chooseCallback = chooseCallback;
    }
    protected abstract void onMoved(int index);
    public abstract void show();
    public abstract void hide();

    public void onUpdate(){
        /* 控制菜单 */

        if(Input.GetKeyDown(KeyCode.A)){
            core.moveLast();
        }else if(Input.GetKeyDown(KeyCode.D)){
            core.moveNext();
        }else if(Input.GetKeyDown(KeyCode.Space)){
            chooseCallback(core.selectionIndex);
        }else if(Input.GetKeyDown(KeyCode.Escape)){
            hide();
        }
    }
}


public class LinearMenuTest: LinearMenuBase{

    private string[] titles;
    private Sprite[] icons;
    private LinearMenuRenderer renderer;
    public LinearMenuTest(LinearMenuRenderer renderer, string[] titles, Sprite[] icons, Action<int> callback):base(titles.Length, callback){

        this.titles = titles;
        this.icons = icons;
        this.renderer = renderer;
    }
    protected override void onMoved(int index){
        Debug.Log($"选项移动到了第{index.ToString()}项");
        renderer.title = titles[index];
        renderer.borderPosition = index;
    }
    public override void show(){
        renderer.setVisible(true);
    }
    public override void hide(){
        renderer.setVisible(false);
    }
}