using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBall : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] float guideSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float destroyMagmaTime;
    private float timer;
    [SerializeField] private Rigidbody2D rigid;

    private void Start()
    {
		player = GameManager.instance.GetPlayer().gameObject;
		DynamicObjectManager.instance.AddObject(this.gameObject);
    }
    private void GuideToPlayer()
    {
		if (timer <= destroyMagmaTime)
		{
			Vector2 dir = transform.right;
			Vector2 targetDir = player.transform.position - transform.position;
			Vector3 crossVec = Vector3.Cross(dir, targetDir);
			float inner = Vector3.Dot(Vector3.forward, crossVec);
			float saveAngle = inner + transform.rotation.eulerAngles.z;
			transform.rotation = Quaternion.Euler(0, 0, saveAngle * rotateSpeed);

			float moveDirAngle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
			Vector2 moveDir = Vector2.zero;
			moveDir = new Vector2(Mathf.Cos(moveDirAngle), Mathf.Sin(moveDirAngle));
			rigid.velocity = moveDir * guideSpeed;
			timer += Time.deltaTime;
		}
		else
		{
			GetComponent<ParticleSystem>().Stop();
			GetComponent<CircleCollider2D>().enabled = false;
			Invoke("DestroyObject", 10f);
		}
	}


	private void DestroyObject()
    {
		Destroy(this.gameObject);
	}

    private void Update()
    {
        GuideToPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Player"))
		{
			GameManager.instance.GetPlayer().Hit();
		}
    }
}


