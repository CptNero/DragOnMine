using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private  float move;

    public float speed;

    private List<Vector3Int> mineableBlocks = new List<Vector3Int>();

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

        if (Input.GetKeyDown(KeyCode.Space)) {
            // foreach (var block in mineableBlocks) {
            //     tilemap.SetTile(block, null);
            // }

            var hit = Physics2D.Raycast(transform.position, -Vector2.up);

            if (hit.collider != null) {
                var cellCoord = tilemap.WorldToCell(hit.point);
                print($"Cell coord:x {cellCoord.x}, Cell coord; y {cellCoord.y}");
                tilemap.SetTile(cellCoord + new Vector3Int(0, -1, 0), null);
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            var mousePos = Input.mousePosition;
            var cellCoord = tilemap.WorldToCell(cam.ScreenToWorldPoint(mousePos));
            print($"Cell coord:x {cellCoord.x}, Cell coord; y {cellCoord.y}");
            tilemap.SetTile(cellCoord, null);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        print("Collision!");

        var tilemap = tileMapObject.GetComponent<Tilemap>();

        foreach (var contact in collision.contacts) {
            var hitPosition = new Vector3(contact.point.x - 0.01f * contact.normal.x,
                                          contact.point.y - 0.01f * contact.normal.y, 0);
            var cellCoord = tilemap.WorldToCell(hitPosition);

            // Bit of a hack
            //tilemap.GetTile(cellCoord).GetComponent<Sprite>
            mineableBlocks.Add(cellCoord + new Vector3Int(0, -1, 0));
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        mineableBlocks.Clear();
    }
}
