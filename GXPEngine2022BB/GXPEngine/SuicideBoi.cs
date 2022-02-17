using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;
using GXPEngine;

public class SuicideBoi : AnimationSprite {
    private float moveSpeed = 0.3f;
    private float rotationSpeed = 10f;
    private float desiredRotation = 0f;
    private int animationCounter = 0;
    private Sprite target;

    public int health = 1;
    public int damage = 2;

    public SuicideBoi() : base("Kamikazee.png", 7, 1) {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.8f, -0.8f);
    }

    void Update() {
        if (health > 0) {
            Movement();
            SetRotationBetween360();
            SetDesiredRotation();
            SlowRotation();
        } else {
            Dying();
        }
    }
    
    void Dying() {
        if(animationCounter >= 10)
            if (health <= 0) {
                animationCounter = 0;
                SetFrame(currentFrame + 1);
                if (currentFrame == 1) {
                    SoundChannel sound = new Sound("Explosion.mp3").Play(true);
                    sound.Volume = 0.1f;
                    sound.IsPaused = false;
                }

                if(currentFrame == 6)
                    LateDestroy();
            }

        animationCounter++;
    }
    
    public void Suicide() {
        health = 0;
    }
    
    public void SetTarget(Sprite target) {
        this.target = target;
    }

    void SetDesiredRotation() {
        float diffX = target.x - x;
        float diffY = target.y - y;
        float cos = Mathf.Abs(diffX) / Mathf.Sqrt(Mathf.Pow(Mathf.Abs(diffX), 2) + Mathf.Pow(Mathf.Abs(diffY), 2));
        if (diffX > float.Epsilon && diffY < float.Epsilon) {
            desiredRotation = 90 - Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
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

    void Movement() {
        Move(0, -moveSpeed * Time.deltaTime);
    }


    void OnCollision(GameObject other) {
        if (other is Bullet && health > 0) {
            Bullet bullet = other.FindObjectOfType<Bullet>();
            if (!bullet.canDamage) {
                health -= bullet.damage;
                if (health <= 0) {
                    Level.currentNumberOfEnemies--;
                }
                Flash();
                other.LateRemove();
            }
        }
    }
    private async void Flash()
    {
        //SetColor(150, 0, 0);
        visible = false;
        await Task.Delay(35);
        visible = true;
        await Task.Delay(35);
        visible = false;
        await Task.Delay(35);
        visible = true;
        //SetColor(255, 255, 255);
    }

}