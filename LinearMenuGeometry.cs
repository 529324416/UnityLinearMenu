using System.Collections.Generic;
using UnityEngine;

public class LinearGeometry{
    
    private Vector2 entryPoint;
    private Vector2 size;
    private float space;
    public LinearGeometry(Vector2 entryPoint, Vector2 size, float space){
        
        this.entryPoint = entryPoint;
        this.size = size;
        this.space = space;
    }
    public Vector2 getPositionAsHorizontal(int index){
        return entryPoint + Vector2.right * index * (size.x + space);
    }
    public Vector2 getPositionAsVertical(int index){
        return entryPoint + Vector2.up * index * (size.y + space);
    }
}