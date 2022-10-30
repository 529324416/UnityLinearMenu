using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearMenuRenderer: MonoBehaviour{

    private GameObject pfb;
    private Vector2 pfbSize;

    private Queue<GameObject> pool;
    private List<GameObject> currentIcons;
    private LinearGeometry geometry;

    private Text titleBox;
    private RectTransform border;
    public string title{
        set => titleBox.text = value;
    }
    public int borderPosition{
        set => border.localPosition = ((RectTransform)currentIcons[value].transform).localPosition;
    }

    
    public Sprite[] icons{
        set{
            checkCount(value.Length);
            for(int i = 0; i < value.Length; i ++){
                currentIcons[i].transform.GetChild(0).
                    GetComponent<Image>().sprite = value[i];
            }
        }
    }
    public void init(GameObject pfb, int defualtCount = 4){
        /* 初始化菜单渲染器 */

        this.pfb = pfb;
        this.pfbSize = pfb.GetComponent<RectTransform>().sizeDelta;
        this.pool = new Queue<GameObject>();
        this.currentIcons = new List<GameObject>();
        this.titleBox = transform.Find("title").GetComponent<Text>();
        this.border = transform.Find("border") as RectTransform;
        geometry = new LinearGeometry(new Vector2(10, 10), pfbSize, 10);
        checkCount(4);
    }
    private void checkCount(int count){

        if(currentIcons.Count == count)return;
        int err = currentIcons.Count - count;
        if(err < 0){
            err *= -1;
            for(int i = 0; i < err; i ++){
                currentIcons.Add(nextIcon);
            }
        }else{
            for(int i = 0; i < err; i ++){
                recycle(currentIcons[count]);
            }
        }
        for(int i = 0; i < currentIcons.Count; i ++){
            ((RectTransform)currentIcons[i].transform).localPosition = geometry.getPositionAsHorizontal(i);
        }
    }
    public GameObject nextIcon{
        get{
            GameObject instance;
            if(pool.Count > 0){
                instance = pool.Dequeue();
                instance.gameObject.SetActive(true);
            }else{
                instance = Object.Instantiate<GameObject>(pfb, transform);
            }
            return instance;
        }
    }
    public void recycle(GameObject instance){

        instance.gameObject.SetActive(false);
        currentIcons.Remove(instance);
        pool.Enqueue(instance);
    }
    public void setVisible(bool value){
        gameObject.SetActive(value);
    }
}