using UnityEngine;

public class CameraTween
{
    private Camera _camera;
    public CameraTween(Camera camera) => this._camera = camera;
    public LTDescr Move(Vector3 to, float time) => LeanTween.moveLocal(_camera.gameObject, to, time);
    public LTDescr MoveX(float to, float time) => LeanTween.moveLocalX(_camera.gameObject, to, time);
    public LTDescr MoveY(float to, float time) => LeanTween.moveLocalY(_camera.gameObject, to, time);
    public LTDescr Size(float to, float time) => LeanTween.value(_camera.orthographicSize, to, time).setOnUpdate((value) => { _camera.orthographicSize = value; });
    public LTDescr RotateZ(float to, float time) => LeanTween.rotateZ(_camera.gameObject, to, time);
}
