using System;
using UnityEngine ;

public class Knife : MonoBehaviour
{
   [SerializeField] private float movementSpeed;
   [SerializeField] private float hitDamage;
   [SerializeField] private Wood wood;
   [SerializeField] private ParticleSystem woodFx;

   private ParticleSystem.EmissionModule woodFxEmission;
      
      
   private Rigidbody _body;
   private Vector3 _movementVector;
   private bool isMoving = false;

   private void Start()
   {
      _body = GetComponent<Rigidbody>();
      woodFxEmission = woodFx.emission;
   }

   private void Update()
   {
      isMoving = Input.GetMouseButton(0);
      if (isMoving)
      {
         _movementVector = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * (movementSpeed * Time.deltaTime);
         _body.position += _movementVector;

      }
   }

   private void OnCollisionStay(Collision other)
   {
      Coll coll = other.collider.GetComponent<Coll>();
      if (coll !=null)
      {
         woodFxEmission.enabled = true;
         woodFx.transform.position = other.contacts[0].point;
         coll.HitCollider(hitDamage);
         wood.Hit(coll.index, hitDamage);
      }
   }

   private void OnCollisionExit(Collision other)
   {
      woodFxEmission.enabled = false;
   }
}
