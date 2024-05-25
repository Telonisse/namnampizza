using UnityEngine;
using Oculus.Interaction;
using UnityEngine.SceneManagement;

public class ObjectGrabbedEventSender : OneGrabFreeTransformer, ITransformer
{
    public delegate void ObjectGrabbed(GameObject source);
    public event ObjectGrabbed onObjectGrabbed;
    public delegate void ObjectMoved(GameObject source);
    public event ObjectMoved onObjectMoved;
    public delegate void ObjectReleased(GameObject source);
    public event ObjectReleased onObjectReleased;

    [SerializeField] bool quit = false;

    public new void Initialize(IGrabbable grabbable)
    {
        base.Initialize(grabbable);
    }
    public new void BeginTransform()
    {
        base.BeginTransform();
        if (quit)
        {
            Application.Quit();
        }
        else 
        {
            SceneManager.LoadSceneAsync(1);
        }

        onObjectGrabbed?.Invoke(gameObject);
    }

    public new void UpdateTransform()
    {
        base.UpdateTransform();
        onObjectMoved?.Invoke(gameObject);
    }

    public new void EndTransform()
    {
        //Parent class does nothing with that method so no need to call it
        onObjectReleased?.Invoke(gameObject);
    }
}
