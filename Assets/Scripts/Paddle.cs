using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // config params
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    // references
    GameSession session;
    Ball ball;
    // Start is called before the first frame update
    void Start() {
        session = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update() {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos() {
        if (session.IsAutoPlayEnabled()) {
            return ball.transform.position.x;
        }
        else {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
