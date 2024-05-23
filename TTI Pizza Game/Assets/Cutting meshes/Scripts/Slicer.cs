using UnityEngine;

using EzySlice;

//using BNG;
using Oculus.Interaction;

public class Slicer : MonoBehaviour

{

    //accesses what material sliced objects become

    public Material materialAfterSlice;

    //checks if objects are on the slicable layer

    public LayerMask sliceMask;

    //checks if it makes contact with slicable objects or not

    public bool isTouched;

    private void Update()

    {

        //runs if the collider interacts with a slicable object

        if (isTouched == true)

        {

            //turns it off

            isTouched = false;

            //checks if the slicable objects have made contact with the rigidbody

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

            //creates the new slices

            foreach (Collider objectToBeSliced in objectsToBeSliced)

            {

                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                //creates the new slices

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                //positions the new slices

                upperHullGameobject.transform.position = objectToBeSliced.transform.position;

                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                //accesses the function for applying physics to the slices

                MakeItPhysical(upperHullGameobject);

                MakeItPhysical(lowerHullGameobject);

                //gets rid of the original object

                Destroy(objectToBeSliced.gameObject);

            }

        }

    }

    //gives the slices rigidbodies and convex colliders

    private void MakeItPhysical(GameObject obj)

    {

        obj.AddComponent<MeshCollider>().convex = true;

        obj.AddComponent<Rigidbody>();

        obj.AddComponent<Grabbable>();

        //allows for further slicing (done by me)

        obj.layer = LayerMask.NameToLayer("Cuttable");

        //calculates volume from the length, width, and height of the slice collider (done by me)

        float volume = transform.localScale.x * transform.localScale.y * transform.localScale.z;

        //retains object density (done by me)

        float density = 1;

        //calculates mass from density and volume (done by me)

        obj.GetComponent<Rigidbody>().mass = volume * density;

    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)

    {

        //returns a value that is sliced objects

        return obj.Slice(transform.position, transform.up, crossSectionMaterial);

    }

}
