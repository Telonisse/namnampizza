using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Oculus.Interaction;
using Meta;

public class SlliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public LayerMask sliceableLayer;
    public VelocityEstimator velocityEstimator;

    public Material crossSectionMaterial;
    public float cutForce = 2000;
    
    List<Collider> collist;

    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);

        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        BoxCollider colliderbox = slicedObject.AddComponent<BoxCollider>();
        slicedObject.AddComponent<Grabbable>();
        slicedObject.AddComponent<PhysicsGrabbable>();
        slicedObject.AddComponent<TouchHandGrabInteractable>();

        slicedObject.layer = LayerMask.NameToLayer("Sliceable");
        slicedObject.gameObject.tag = "Sausage";

        //slicedObject.GetComponent<PhysicsGrabbable>().InjectAllPhysicsGrabbable(IPointable pointable, rb);

        collist.Add(collider);

        colliderbox.isTrigger = true;

        //slicedObject.GetComponent<PhysicsGrabbable>().InjectPointable(IPointable pointable);

        //slicedObject.GetComponent<PhysicsGrabbable>().

        //slicedObject.GetComponent<TouchHandGrabInteractable>().InjectAllTouchHandGrabInteractable(collider, collist);
        
        //GetComponent<Collider>().convex = true;

        //rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
  
}
