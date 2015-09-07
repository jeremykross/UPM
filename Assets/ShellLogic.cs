using UnityEngine;
using System.Collections;

public class ShellLogic : MonoBehaviour
{
    public GameObject explosion;
    bool isOnScreen = false;
    public bool isAlive = true;
    float rotY = 0;
    float dimen;

    // Use this for initialization
    void Start ()
    {
        var rigidBody = GetComponent<Rigidbody2D> ();
        rigidBody.AddForce (new Vector2 (Random.value * 250 * (((int) (Random.value * 100)) % 2 == 0 ? 1:-1), -Random.value * 150 - 50));
        var screenSize = Camera.main.WorldToScreenPoint(new Vector3(.64f * transform.lossyScale.x, .64f * transform.localScale.y, 0));
        dimen = screenSize.x - Screen.width/2;
    }

    // Update is called once per frame
    void Update ()
    {
        var rigidBody = GetComponent<Rigidbody2D> ();
        var screenPoint = Camera.main.WorldToScreenPoint (transform.position);

        if(isAlive) {
            var newPosX = screenPoint.x;
            var newPosY = screenPoint.y;

            if (screenPoint.y < Screen.height-dimen) {
                isOnScreen = true;
            }

            if (isOnScreen) {
                if (screenPoint.y < dimen || screenPoint.y > Screen.height - dimen) {
                    rigidBody.velocity = new Vector2 (rigidBody.velocity.x, -rigidBody.velocity.y);
                    if (screenPoint.y < dimen)
                        newPosY = dimen;
                    if (screenPoint.y > Screen.height - dimen)
                        newPosY = Screen.height - dimen;
                }
                if (screenPoint.x < dimen || screenPoint.x > Screen.width - dimen) {
                    rigidBody.velocity = new Vector2 (-rigidBody.velocity.x, rigidBody.velocity.y);
                    if (screenPoint.x < dimen)
                        newPosX = dimen;
                    if (screenPoint.x > Screen.width - dimen)
                        newPosX = Screen.width - dimen;
                }
            }

            rigidBody.position = Camera.main.ScreenToWorldPoint (new Vector2 (newPosX, newPosY));
        } else {
            rigidBody.gravityScale = 3;
            rotY += Time.deltaTime * 175;
            rigidBody.transform.Rotate(new Vector3(0, rotY, 0));
            if(screenPoint.y < 0) {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
        if(!isOnScreen || !isAlive) return;
        var other = collision.gameObject;

        if (other.tag != null && other.tag.Equals ("explosion")) {
            ExplosionLogic logic = other.GetComponent<ExplosionLogic>();
            if(logic.isAlive) {
                Destroy (gameObject);
                var particles = 
                    ((GameObject)Instantiate (explosion, transform.position, Quaternion.identity)).GetComponent<ParticleSystem> ();
                particles.GetComponent<ExplosionLogic> ().ComboCount = 
                    other.GetComponent<ExplosionLogic> ().ComboCount + 1;
                particles.Play ();
            }
        }
    }
}
