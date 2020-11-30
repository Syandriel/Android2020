using UnityEngine;

public interface IPlayerControl {
    void Move(Vector2 moveDirection);
    void AttackSpecial();
    void AttackNormal();
    void AttackCrouch();
    void AttackForward();
    void AttackUp();
    void AttackAir();
    void Grab();
    float GetMovementSpeed();
    bool IsBusy();
    Collider2D Get2DCollider();
    Sprite GetSprite();
}
