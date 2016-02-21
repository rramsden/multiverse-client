using UnityEngine;
using Multiverse.Network.Packets;

public class Player : MonoBehaviour {
    public float playerSpeed = 0.03f;
    public bool isMoving = false;
    public bool isPlayer = true;

    Vector3 currentPosition;

    void Start() {
        currentPosition = transform.position;
    }

    void Update () {
        if (isPlayer) {
          updatePosition ();
        }

        if (isMoving && isPlayer) {
          broadcastPosition();
        }
    }

    private void updatePosition() {
        isMoving = currentPosition != transform.position;
        currentPosition = transform.position;

        float h = Input.GetAxis ("Horizontal") * playerSpeed;
        float v = Input.GetAxis ("Vertical") * playerSpeed;

        transform.Translate (h, 0, v);
    }

    private void broadcastPosition() {
        var packet = new PLAYER_MOVE(currentPosition, GetInstanceID());
        Multiverse.Network.Manager.instance.Send(packet);
    }
}
