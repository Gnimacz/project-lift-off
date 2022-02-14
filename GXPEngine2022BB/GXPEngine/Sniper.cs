using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;
using GXPEngine;

public class Sniper : Sprite {
    private Sprite target;
    private float moveSpeed = 0.3f;
    private float desiredRotation = 0;
    private float rotationSpeed = 10f;
    private Vector2 movePoint;
    private Rectangle[] moveAreas = new Rectangle[4];
    private Rectangle[] currentMoveAreas = new Rectangle[2];
    private int[] currentNumbersOfAreas = new int[2];

    public Sniper() : base("square.png") {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.4f, 0.4f);
        SetPossibleMoveAreas();
        SetMovementPoint();
    }

    void Update() {
        //Console.WriteLine("{0}, {1} -> {2}, {3}", x, y, movePoint.x, movePoint.y);
        MovementInitialization();
        SetRotationBetween360();
        SetDesiredRotation();
        SlowRotation();
    }

    void SetPossibleMoveAreas() {
        moveAreas[0] = new Rectangle(0, 0, 1366, 154);
        moveAreas[1] = new Rectangle(1093, 0, 273, 766);
        moveAreas[2] = new Rectangle(0, 612, 1366, 154);
        moveAreas[3] = new Rectangle(0, 0, 273, 766);
    }

    public void SetTarget(Sprite target) {
        this.target = target;
    }

    void SetDesiredRotation() {
        float diffX = target.x - x;
        float diffY = target.y - y;
        float cos = Mathf.Abs(diffX) /
                    Mathf.Sqrt(Mathf.Pow(Mathf.Abs(diffX), 2) +
                               Mathf.Pow(Mathf.Abs(diffY), 2)); // calculate cos of desired angle
        if (diffX > float.Epsilon && diffY < float.Epsilon) {
            desiredRotation = 90 - Mathf.Acos(cos) * 360 / (Mathf.PI * 2); // set rotation in degrees
        }
        else if (diffX > float.Epsilon && diffY > float.Epsilon) {
            desiredRotation = 90 + Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
        }
        else if (diffX < float.Epsilon && diffY > float.Epsilon) {
            desiredRotation = 270 - Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
        }
        else {
            desiredRotation = 270 + Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
        }
    }

    void SetRotationBetween360() {
        if (rotation < float.Epsilon)
            rotation = 360 + rotation;
        else
            rotation = rotation % 360;
    }

    void SlowRotation() {
        float positiveTurn = desiredRotation - rotation;
        float degreeToTurn = 0f;
        if (positiveTurn > float.Epsilon) {
            if (positiveTurn - 180 < float.Epsilon) degreeToTurn = positiveTurn;
            else degreeToTurn = -positiveTurn;
        }
        else {
            if (Mathf.Abs(positiveTurn) - 180 > float.Epsilon) degreeToTurn = -positiveTurn;
            else degreeToTurn = positiveTurn;
        }

        if (Mathf.Abs(degreeToTurn) - rotationSpeed * Time.deltaTime < float.Epsilon)
            rotation = desiredRotation;
        else {
            rotation += Mathf.Sign(degreeToTurn) * rotationSpeed * Time.deltaTime;
        }
    }

    void MovementInitialization() {
        SetCurrentAreaOfMovement();
        if (CheckIfPointIsInAreas(movePoint, currentMoveAreas)) {
            if (CheckIfPathIsInAreas()) {
                Movement();
            }
            else {
                Console.WriteLine("{0}, {1} -> {2}, {3}", x, y, movePoint.x, movePoint.y);
                SetCornerMovePoint();
                Console.WriteLine("{0}, {1} -> {2}, {3}", x, y, movePoint.x, movePoint.y);
                Console.WriteLine("\n");
                Movement();
            }
        } else {
            SetMovementPoint();
        }
    }
    
    void Movement() {
        float lengthToPoint =
            Mathf.Sqrt(Mathf.Pow(Mathf.Abs(movePoint.x - x), 2) + Mathf.Pow(Mathf.Abs(movePoint.y - y), 2));
        if (Mathf.Sqrt(Mathf.Pow(Mathf.Abs(movePoint.x - x) / lengthToPoint, 2) +
                       Mathf.Pow(Mathf.Abs(movePoint.y - y) / lengthToPoint, 2)) * moveSpeed * Time.deltaTime >
                        lengthToPoint) {
            x = movePoint.x;
            y = movePoint.y;
            SetMovementPoint();
        } else {
            Translate((movePoint.x - x) / lengthToPoint * moveSpeed * Time.deltaTime,
                (movePoint.y - y) / lengthToPoint * moveSpeed * Time.deltaTime);
        }
    }

    void SetCurrentAreaOfMovement() {
        if (target.x - 683 < float.Epsilon && target.y - 383 < float.Epsilon) {
            currentMoveAreas[0] = moveAreas[1];
            currentMoveAreas[1] = moveAreas[2];
            currentNumbersOfAreas[0] = 1;
            currentNumbersOfAreas[1] = 2;
        }
        else if (target.x - 683 < float.Epsilon && target.y - 383 > float.Epsilon) {
            currentMoveAreas[0] = moveAreas[0];
            currentMoveAreas[1] = moveAreas[1];
            currentNumbersOfAreas[0] = 0;
            currentNumbersOfAreas[1] = 1;
        }
        else if (target.x - 683 > float.Epsilon && target.y - 383 < float.Epsilon) {
            currentMoveAreas[0] = moveAreas[2];
            currentMoveAreas[1] = moveAreas[3];
            currentNumbersOfAreas[0] = 2;
            currentNumbersOfAreas[1] = 3;
        }
        else {
            currentMoveAreas[0] = moveAreas[0];
            currentMoveAreas[1] = moveAreas[3];
            currentNumbersOfAreas[0] = 0;
            currentNumbersOfAreas[1] = 3;
        }
    }

    void SetMovementPoint() {
        Vector2[] randomPoint = new Vector2[2];
        randomPoint[0].x = Utils.Random(currentMoveAreas[0].left, currentMoveAreas[0].right);
        randomPoint[0].y = Utils.Random(currentMoveAreas[0].top, currentMoveAreas[0].bottom);
        randomPoint[1].x = Utils.Random(currentMoveAreas[1].left, currentMoveAreas[1].right);
        randomPoint[1].y = Utils.Random(currentMoveAreas[1].top, currentMoveAreas[1].bottom);

        if (Utils.Random(0f, 2f) > 1f) {
            movePoint.x = randomPoint[0].x;
            movePoint.y = randomPoint[0].y;
        }
        else {
            movePoint.x = randomPoint[1].x;
            movePoint.y = randomPoint[1].y;
        }
    }

    void SetCornerMovePoint() {
        int currentArea = FindCurrentArea();
        if(currentArea == 5)
            Console.WriteLine("FindCurrentArea Method Broke");
        Vector2[] CornerPoints = new Vector2[2];
        if (currentArea == 0 || currentArea == 2) {
            CornerPoints[0].x = moveAreas[currentArea].x + 137;
            CornerPoints[0].y = moveAreas[currentArea].y + 77;
            CornerPoints[1].x = moveAreas[currentArea].width - 137;
            CornerPoints[1].y = moveAreas[currentArea].y + 77;
        }
        else {
            CornerPoints[0].x = moveAreas[currentArea].x + 137;
            CornerPoints[0].y = moveAreas[currentArea].y + 77;
            CornerPoints[1].x = moveAreas[currentArea].x + 137;
            CornerPoints[1].y = moveAreas[currentArea].height - 77;
        }

        int[] NumberOfCurrentAreas = new int[2];

        for (int i = 0; i < CornerPoints.Length; i++) {
            for (int j = 0; j < currentMoveAreas.Length; j++) {
                if (CheckIfPointIsInArea(CornerPoints[i], currentMoveAreas[j])) {
                    NumberOfCurrentAreas[i]++;
                }
            }
        }

        if (NumberOfCurrentAreas[0] > NumberOfCurrentAreas[1]) {
            movePoint.x = CornerPoints[0].x;
            movePoint.y = CornerPoints[0].y;
        }
        else {
            movePoint.x = CornerPoints[1].x;
            movePoint.y = CornerPoints[1].y;
        }

    }

    
    int FindCurrentArea() {

        List<int> returnAreas = new List<int>();
        returnAreas.Add(5);
        Console.WriteLine(returnAreas.Count);
        for (int i = 0; i < moveAreas.Length; i++)
        {
            if (x - moveAreas[i].left > float.Epsilon && x - moveAreas[i].right < float.Epsilon)
                if (y - moveAreas[i].top > float.Epsilon && y - moveAreas[i].bottom < float.Epsilon)
                {
                    returnAreas.Add(i);
                    if (returnAreas.Contains(5))
                        returnAreas.Remove(5);
                }
        }

        for (int i = 0; i < returnAreas.Count; i++) {
            Console.WriteLine("area = {0}, targetArea = {1}", returnAreas[i], currentNumbersOfAreas[i]);
        }
        Console.WriteLine("count = {0}", returnAreas.Count);

        List<bool> isCurrentArea = new List<bool>();

        for (int i = 0; i < returnAreas.Count; i++) {
            bool isCurrent = false;
            for (int j = 0; j < currentNumbersOfAreas.Length; j++) {
                if (returnAreas[i] == currentNumbersOfAreas[j]) {
                    isCurrent = true;
                }
            }
            
            if(isCurrent)
                isCurrentArea.Add(true);
            else {
                isCurrentArea.Add(false);
            }
        }
        

        if (returnAreas.Count == 1) {
            if (returnAreas.Contains(5))
                return 5;
            else {
                return returnAreas[0];
            }

        }

        if (returnAreas.Count == 2) {
            if (isCurrentArea[0] && isCurrentArea[1] || !isCurrentArea[0] && !isCurrentArea[1]) {
                return Utils.Random(0f, 2f) > 1f ? returnAreas[0] : returnAreas[1];
            }

            if (isCurrentArea[0]) {
                Console.WriteLine("current area is {0}", returnAreas[0]);
                return returnAreas[0];
            }

            if (isCurrentArea[1]) {
                Console.WriteLine("current area is {0}", returnAreas[1]);
                return returnAreas[1];
            }

            
        }
        
        Console.WriteLine("current area is 10");
        return 10;
    }

    bool CheckIfPointIsInAreas(Vector2 point, Rectangle[] areas) {
        foreach (Rectangle area in areas) {
            if (point.x - area.left > float.Epsilon && point.x - area.right < float.Epsilon)
                if (point.y - area.top > float.Epsilon && point.y - area.bottom < float.Epsilon) {
                    return true;
                }
        }

        return false;
    }
    
    bool CheckIfPointIsInArea(Vector2 point, Rectangle area) {
        if (point.x - area.left > float.Epsilon && point.x - area.right < float.Epsilon)
                if (point.y - area.top > float.Epsilon && point.y - area.bottom < float.Epsilon) {
                    return true;
                }
        
        return false;
    }

    bool CheckIfPathIsInAreas() {
        Vector2 shiftingPoint = new Vector2(x, y);
        for (int i = 0; i < 100; i++) {
            if (!CheckIfPointIsInAreas(shiftingPoint, moveAreas)) {
                return false;
            }

            shiftingPoint.x += (movePoint.x - x) / 100;
            shiftingPoint.y += (movePoint.y - y) / 100;
        }

        return true;
    }
}