using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField] private float cutoutSize = .2f, falloffSize = .05f;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask wallMask;
    private Camera _camera;
    private readonly RaycastHit[] _hits = new RaycastHit[5];
    private static readonly int CutoutPos = Shader.PropertyToID("_CutoutPosition");
    private static readonly int CutoutSize = Shader.PropertyToID("_CutoutSize");
    private static readonly int FalloffSize = Shader.PropertyToID("_FalloffSize");

    private void Awake() => _camera = Camera.main;

    private void Update()
    {
        var position = target.position;
        var cutoutPos = _camera.WorldToViewportPoint(position);
        cutoutPos.y /=  Screen.width / Screen.height;

        var offset = position - transform.position;
        var size = Physics.RaycastNonAlloc(transform.position, offset, _hits, offset.magnitude, wallMask);

        for (int i = 0; i < size; i++)
        {
            if (_hits[i].transform.TryGetComponent(out Renderer renderer))
            {
                for (int j = 0; j < renderer.materials.Length; j++)
                {
                    renderer.materials[j].SetVector(CutoutPos, cutoutPos);
                    renderer.materials[j].SetFloat(CutoutSize, cutoutSize);
                    renderer.materials[j].SetFloat(FalloffSize, falloffSize);
                }
            }
        }
    }
}