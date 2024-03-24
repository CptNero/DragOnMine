using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private LineRenderer ld;

    private  float move;

    public float speed;

    [SerializeField]
    private GameObject tileMapObject;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        var tilemap = tileMapObject.GetComponent<Tilemap>();

        Debug.DrawLine(transform.position, transform.position + -transform.up, Color.red, 0.01f);

        if (Input.GetKeyDown(KeyCode.Space)) {
            var hit = Physics2D.Raycast(transform.position, -transform.up, 1.0f);
            print(transform.up);

            if (hit.collider != null) {
                var newHit = new Vector3(hit.point.x - 0.01f * hit.normal.x,
                                         hit.point.y - 0.01f * hit.normal.y, 0);
                var cellCoord = tilemap.WorldToCell(newHit);
                Debug.DrawLine(new Vector2(0, 0), newHit, Color.green, 10);
                print($"Cell coord:x {cellCoord.x}, Cell coord; y {cellCoord.y}");
                tilemap.SetTile(cellCoord, null);
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            var mousePos = Input.mousePosition;
            var cellCoord = tilemap.WorldToCell(cam.ScreenToWorldPoint(mousePos));
            print($"Cell coord:x {cellCoord.x}, Cell coord; y {cellCoord.y}");
            tilemap.SetTile(cellCoord, null);
        }
    }
}
