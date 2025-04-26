using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 1f;
    public Vector3 offset;
    public SpriteRenderer background;

    void LateUpdate(){
        if(player != null){
            //transform.position = player.position + offset;
            Vector2 bottomLeft = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector2 topLeft = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
            Vector2 bottomRight = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
            Vector3 tmp = Vector3.zero;
            if(Mathf.Abs(background.bounds.extents.x) - Mathf.Abs(player.position.x) < Mathf.Abs((bottomLeft.x - bottomRight.x) / 2)){
                tmp.x = transform.position.x;
            }else{
                tmp.x = player.position.x;
            }
            if(Mathf.Abs(background.bounds.extents.y) - Mathf.Abs(player.position.y) < Mathf.Abs((bottomLeft.y - topLeft.y) / 2)){
                tmp.y = transform.position.y;
            }else{
                tmp.y = player.position.y;
            }
            transform.position = tmp + offset;
        }
    }
}
