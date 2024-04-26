using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
   private LineRenderer lineRenderer = null;
   private ParticleSystem splashParticle = null;

   private Coroutine pourRoutine = null;
   private Vector3 targetPosition = Vector3.zero;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while(gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();

            MoveToPosition(0, transform.position);
            MoveToPosition(1, targetPosition);

            yield return null;
        }
       
    }

    public void End()
    {

    }

    private IEnumerator EndPour()
    {
        yield return null;
    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        //Make this longer most likely
        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);

        return endPoint;
    }

    private void MoveToPosition(int index, Vector3 targetposition)
    {
        lineRenderer.SetPosition(index, targetposition);
    }

    private void AnimateToPosition(int index, Vector3 targetposition)
    {

    }

    private bool HasReachedPosition(int index, Vector3 targetposition)
    {
        return false;
    }

    private IEnumerator UpdateParticle()
    {
        yield return null;
    }
}
