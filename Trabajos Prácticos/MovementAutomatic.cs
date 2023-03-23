using System;
using UnityEngine;

public class MovementAutomatic : MonoBehaviour
{
    enum TypeMovementBot { HorizontalBounce,VerticalBounce,HorizontalFromLeft, HorizontalFromRight,VerticalFromAbove, VerticalFromBelow }

    [SerializeField] TypeMovementBot typeMovementRobot;
    [SerializeField] float step;
    [SerializeField] float startingPoint;
    [SerializeField] float endPoint;
    private Boolean goingUp = true;


    private Transform t;

    private void Awake()
    {
        t = GetComponent<Transform>();
    }

    private void Start() 
    {
        if(typeMovementRobot == TypeMovementBot.HorizontalBounce || typeMovementRobot == TypeMovementBot.HorizontalFromLeft || typeMovementRobot == TypeMovementBot.HorizontalFromRight) {
            t.position = new Vector2(startingPoint, t.position.y);
        } else {
            t.position = new Vector2(t.position.x, startingPoint);
        }
    }

    private void Update()
    {
        try
        {
            switch (typeMovementRobot)
            {
                case TypeMovementBot.HorizontalBounce:
                    HorizontalBounce();
                    break;
                case TypeMovementBot.VerticalBounce:
                    VerticalBounce();
                    break;
                case TypeMovementBot.HorizontalFromLeft:
                    HorizontalFromLeft(true);
                    break;
                case TypeMovementBot.HorizontalFromRight:
                    HorizontalFromRight(true);
                    break;
                case TypeMovementBot.VerticalFromAbove:
                    VerticalFromAbove(true);
                    break;
                case TypeMovementBot.VerticalFromBelow:
                    VerticalFromBelow(true);
                    break;
            }
        }catch(Exception e)
        {
            Debug.LogError("No se ha encontrado el componente Transform en el objeto actual");
        }
    }
    private void HorizontalBounce()
    {
        if (t.position.x < endPoint && t.localScale.x == Math.Abs(t.localScale.x))
        {
            HorizontalFromLeft();
        }
        else if (t.position.x > startingPoint)
        {
            HorizontalFromRight();
        }
        else
        {
            t.localScale = new Vector2(Math.Abs(t.localScale.x), t.localScale.y);
        }
    }
    private void VerticalBounce()
    {
        if(t.position.y < endPoint && goingUp)
        {
            VerticalFromBelow();
        } else if (t.position.y > startingPoint)
        {
            VerticalFromAbove();
        } 
        else
        {
            goingUp = true;
        }

    }
    private void HorizontalFromLeft(Boolean solo = false)
    {
        t.localScale = new Vector2(Math.Abs(t.localScale.x),t.localScale.y);
        t.position = new Vector2(t.position.x + step,t.position.y);
        if (solo)
        {
            if (t.position.x > endPoint)
            {
                t.position = new Vector2(startingPoint, t.position.y);
            }
        }
    }

    private void HorizontalFromRight(Boolean solo = false)
    {
        t.localScale = new Vector2(-Math.Abs(t.localScale.x),t.localScale.y);
        t.position = new Vector2(t.position.x - step,t.position.y);
        if (solo)
        {
            if (t.position.x < startingPoint)
            {
                t.position = new Vector2(endPoint, t.position.y);
            }
        }
    }

    private void VerticalFromAbove(Boolean solo = false)
    {
        t.position = new Vector2(t.position.x, t.position.y - step);
        goingUp = false;
        if(solo) 
        {
            if(t.position.y < startingPoint) 
            {
                t.position = new Vector2(t.position.x, endPoint);
            }
        }
    }

    private void VerticalFromBelow(Boolean solo = false)
    {
        t.position = new Vector2(t.position.x, t.position.y + step);
        goingUp = true;
        if(solo) 
        {
            if(t.position.y > endPoint) 
            {
                t.position = new Vector2(t.position.x, startingPoint);
            }
        }
    }
}