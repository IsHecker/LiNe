using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private bool movement;
    [SerializeField] private Vector3 direction;

    [SerializeField] private bool rotation;
    [SerializeField] private Vector3 rotationDirection;


    [Header("LOOP")]
    [SerializeField] private bool tween = true;

    [SerializeField] private EaseAnimationMode easeType;

    [SerializeField] private float destination;
    [SerializeField] private float degree;

    private LTDescr bodyTransform;
    private LTDescr bodyRotation;

    private Transform mytransform;
    private Vector2 velocity;


    private void Start()
    {
        mytransform = GetComponent<Transform>();

        direction.Normalize();
        rotationDirection.Normalize();

        if (!tween)
            return;

        if (movement)
            bodyTransform = mytransform.LeanMoveLocal(direction * destination, 1 / speed);

        if (rotation)
            bodyRotation = mytransform.LeanRotateAroundLocal(rotationDirection, degree, 1 / speed);

        EaseAnimation(easeType);

        bodyTransform?.setLoopPingPong();
        bodyRotation?.setLoopPingPong();
    }
    private void Update()
    {
        if (tween) return;

        RawMovement();

        Rotation();
    }

    private void RawMovement()
    {
        if (direction.magnitude == 0) return;

        velocity.Set(0, speed * Time.deltaTime);
        mytransform.Translate(velocity, Space.World);
    }

    private void Rotation()
    {
        if (rotationDirection.magnitude == 0) return;

        transform.Rotate(speed * Time.deltaTime * rotationDirection, Space.Self);
    }

    private void EaseAnimation(EaseAnimationMode type)
    {
        switch (type)
        {
            case EaseAnimationMode.linear:
                bodyTransform.setEaseLinear();
                break;
            case EaseAnimationMode.easeOutQuad:
                bodyTransform.setEaseOutQuad();
                break;
            case EaseAnimationMode.easeInQuad:
                bodyTransform.setEaseInQuad();
                break;
            case EaseAnimationMode.easeInOutQuad:
                bodyTransform.setEaseInOutQuad();
                break;
            case EaseAnimationMode.easeInCubic:
                bodyTransform.setEaseInCubic();
                break;
            case EaseAnimationMode.easeOutCubic:
                bodyTransform.setEaseOutCubic();
                break;
            case EaseAnimationMode.easeInOutCubic:
                bodyTransform.setEaseInOutCubic();
                break;
            case EaseAnimationMode.easeInQuart:
                bodyTransform.setEaseInQuart();
                break;
            case EaseAnimationMode.easeOutQuart:
                bodyTransform.setEaseOutQuart();
                break;
            case EaseAnimationMode.easeInOutQuart:
                bodyTransform.setEaseInOutQuart();
                break;
            case EaseAnimationMode.easeInQuint:
                bodyTransform.setEaseInQuint();
                break;
            case EaseAnimationMode.easeOutQuint:
                bodyTransform.setEaseOutQuint();
                break;
            case EaseAnimationMode.easeInOutQuint:
                bodyTransform.setEaseInOutQuint();
                break;
            case EaseAnimationMode.easeInSine:
                bodyTransform.setEaseInSine();
                break;
            case EaseAnimationMode.easeOutSine:
                bodyTransform.setEaseOutSine();
                break;
            case EaseAnimationMode.easeInOutSine:
                bodyTransform.setEaseInOutSine();
                break;
            case EaseAnimationMode.easeInExpo:
                bodyTransform.setEaseInExpo();
                break;
            case EaseAnimationMode.easeOutExpo:
                bodyTransform.setEaseOutExpo();
                break;
            case EaseAnimationMode.easeInOutExpo:
                bodyTransform.setEaseInOutExpo();
                break;
            case EaseAnimationMode.easeInCirc:
                bodyTransform.setEaseInCirc();
                break;
            case EaseAnimationMode.easeOutCirc:
                bodyTransform.setEaseOutCirc();
                break;
            case EaseAnimationMode.easeInOutCirc:
                bodyTransform.setEaseInOutCirc();
                break;
            case EaseAnimationMode.easeInBounce:
                bodyTransform.setEaseInBounce();
                break;
            case EaseAnimationMode.easeOutBounce:
                bodyTransform.setEaseOutBounce();
                break;
            case EaseAnimationMode.easeInOutBounce:
                bodyTransform.setEaseInOutBounce();
                break;
            case EaseAnimationMode.easeInBack:
                bodyTransform.setEaseInBack();
                break;
            case EaseAnimationMode.easeOutBack:
                bodyTransform.setEaseOutBack();
                break;
            case EaseAnimationMode.easeInOutBack:
                bodyTransform.setEaseInOutBack();
                break;
            case EaseAnimationMode.easeInElastic:
                bodyTransform.setEaseInElastic();
                break;
            case EaseAnimationMode.easeOutElastic:
                bodyTransform.setEaseOutElastic();
                break;
            case EaseAnimationMode.easeInOutElastic:
                bodyTransform.setEaseInOutElastic();
                break;
            case EaseAnimationMode.pingPong:
                bodyTransform.setLoopPingPong();
                break;
        }
    }


}

public enum EaseAnimationMode
{
           linear,
           
           easeOutQuad,
           
           easeInQuad,
           
           easeInOutQuad,
           
           easeInCubic,
           
           easeOutCubic,
           
           easeInOutCubic,
           
           easeInQuart,
           
           easeOutQuart,
           
           easeInOutQuart,
           
           easeInQuint,
           
           easeOutQuint,
           
           easeInOutQuint,
           
           easeInSine,
           
           easeOutSine,
           
           easeInOutSine,
           
           easeInExpo,
           
           easeOutExpo,
           
           easeInOutExpo,
           
           easeInCirc,
           
           easeOutCirc,
           
           easeInOutCirc,
           
           easeInBounce,
           
           easeOutBounce,
           
           easeInOutBounce,
           
           easeInBack,
           
           easeOutBack,
           
           easeInOutBack,
           
           easeInElastic,
           
           easeOutElastic,
           
           easeInOutElastic,
           
           pingPong,
}
