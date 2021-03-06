using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;
using GXPEngine;

public class Grunt : AnimationSprite {
    private float speed = 0.2f;
    private Sprite target;
    private int animationCounter = 0;
    private float desiredRotation = 0f;
    private float rotationSpeed = 0.2f;
    private int lastShootTime = 0;
    private int shootLevel = 1;
    private int shootIntervals = 1400;
    private Vector2 movePoint;
    private Vector2[] movePoints = new Vector2[3];
    private float t = 0;
    public int health = 3;

    public Grunt() : base("Grunt.png", 8, 1) {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.8f, -0.8f);
        ChooseMovement();
    }

    public Grunt(int shootLevel = 1) : base("Grunt.png",8 , 1) {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.8f, -0.8f);
        ChooseMovement();
        this.shootLevel = shootLevel;
    }

    void Update() {
        //SmoothMovement();
        if (health > 0) {
            Movement();
            SetRotationBetween360();
            SetDesiredRotation();
            SlowRotation();
            Shooting();
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
                if(currentFrame == 7)
                    LateRemove();
            }

        animationCounter++;
    }
    
    void Shooting() {
        switch (shootLevel) {
            case 1:
                if (Time.time > lastShootTime) {
                    Shoot();
                    lastShootTime = Time.time + shootIntervals;
                }

                break;
            case 2:
                if (Time.time > lastShootTime) {
                    ShootLevelTwo();
                    lastShootTime = Time.time + shootIntervals;
                }

                break;
            default:
                break;
        }
    }

    void ChooseSmoothMovement() {
        movePoints[0].x = this.x;
        movePoints[0].y = this.y;
        for (int i = 1; i < movePoints.Length; i++) {
            movePoints[i].x = Utils.Random(50, 1316);
            movePoints[i].y = Utils.Random(30, 738);
        }
    }

    void ChooseMovement() {
        movePoint.x = Utils.Random(50, 1316);
        movePoint.y = Utils.Random(30, 738);
    }

    public void SetTarget(Sprite target) {
        this.target = target;
    }

    void Shoot() {
        Bullet projectile = new Bullet("GruntBullet.png", 0.5f, 0, -1, true, 1, 0.25f);
        EasyDraw canvas = parent.FindObjectOfType<EasyDraw>();
        SoundChannel sound = new Sound("GruntShot.mp3").Play(true);
        sound.Volume = 0.1f;
        sound.IsPaused = false;
        Console.WriteLine("plays");
        projectile.SetOrigin(projectile.width / 2, projectile.height / 2);
        projectile.rotation = rotation;
        projectile.SetXY(x, y);
        canvas.AddChildAt(projectile, 4);
    }

    void ShootLevelTwo() {
        Bullet[] projectiles = new Bullet[3];
        EasyDraw canvas = parent.FindObjectOfType<EasyDraw>();
        SoundChannel sound = new Sound("GruntShot.mp3").Play();
        sound.Volume = 0.1f;
        sound.IsPaused = false;
        for (int i = 0; i < 3; i++) {
            projectiles[i] = new Bullet("GruntBullet.png", 0.5f, 0, -1, true, 1, 0.25f); ;
            projectiles[i].SetOrigin(projectiles[i].width / 2, projectiles[i].height / 2);
            projectiles[i].rotation = rotation - 45 + i * 45;
            projectiles[i].SetXY(x, y);
            canvas.AddChildAt(projectiles[i], 4);
        }
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

    void Movement() {
        float lengthToPoint =
            Mathf.Sqrt(Mathf.Pow(Mathf.Abs(movePoint.x - x), 2) + Mathf.Pow(Mathf.Abs(movePoint.y - y), 2));
        if (Mathf.Sqrt(Mathf.Pow(Mathf.Abs(movePoint.x - x) / lengthToPoint, 2) +
                       Mathf.Pow(Mathf.Abs(movePoint.y - y) / lengthToPoint, 2)) * speed * Time.deltaTime >
            lengthToPoint) {
            ChooseMovement();
        }
        else {
            Translate((movePoint.x - x) / lengthToPoint * speed * Time.deltaTime,
                (movePoint.y - y) / lengthToPoint * speed * Time.deltaTime);
        }
    }


    void SmoothMovement() {
        if (t - 1 <= float.Epsilon) {
            Vector2 nextMovePoint;
            nextMovePoint.x = Mathf.Pow((1 - t), 2) * movePoints[0].x + 2 * (1 - t) * t * movePoints[1].x +
                              Mathf.Pow(t, 2) * movePoints[2].x;
            nextMovePoint.y = Mathf.Pow((1 - t), 2) * movePoints[0].y + 2 * (1 - t) * t * movePoints[1].y +
                              Mathf.Pow(t, 2) * movePoints[2].y;

            float nextTravelLength = Mathf.Pow(nextMovePoint.x - x, 2) + Mathf.Pow(nextMovePoint.y - y, 2);

            nextMovePoint.x = x;

            t += 0.001f;
        }
        else {
            t = 0;
            ChooseSmoothMovement();
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

    void OnCollision(GameObject other) {
        if (other is Bullet && health > 0) {
            Bullet bullet = other.FindObjectOfType<Bullet>();
            if (!bullet.canDamage) {
                health -= bullet.damage;
                Flash();
                if (health <= 0) {
                    Level.currentNumberOfEnemies--;
                }

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