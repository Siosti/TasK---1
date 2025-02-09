using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private Rigidbody2D rb;
    [Header("Motion")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool canJump;
    [Header("Abbility")]
    [SerializeField] private Color lineColor;
    [Header("GrounCheck")]
    [SerializeField] Collider2D feetCollider;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Tilemap map;

    private void Start()
    {
        StartCoroutine(ColorMap());
    }
    private void FixedUpdate()
    {
        GrounChech();
        rb.velocity = new Vector2(inputController.moveDir*speed,rb.velocity.y);
        if(inputController.isJumped && canJump)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        
    }

    IEnumerator ColorMap()
    {
        while (true) 
        {
            Vector3 worldPos = new Vector3(transform.position.x, transform.position.y - 1, 0);
            Vector3Int cellPos = map.WorldToCell(worldPos);
            TileBase tile = map.GetTile(cellPos);
            if (tile != null)
            {
                map.SetTileFlags(cellPos, TileFlags.None);
                if (map.GetColor(cellPos) != lineColor) map.SetColor(cellPos, lineColor);
                else map.SetColor(cellPos, Color.white);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void GrounChech()
    {
        if (!feetCollider.IsTouchingLayers(groundMask))
        {
            canJump = false;
        }
        else canJump = true;
    }
}
