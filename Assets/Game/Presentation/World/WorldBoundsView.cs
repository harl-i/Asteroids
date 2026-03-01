using Game.Core.World;
using Game.Infrastructure.Physics;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LineRenderer))]
public class WorldBoundsView : MonoBehaviour
{
    [SerializeField] private float _lineWidth = 0.08f;
    [SerializeField] private Color _lineColor = Color.white;

    private LineRenderer _lineRenderer;
    private PhysicsWorldProvider _worldProvider;

    [Inject]
    public void Construct(PhysicsWorldProvider worldProvider)
    {
        _worldProvider = worldProvider;
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        ConfigureLineRenderer();
    }

    private void Start()
    {
        DrawBorder(GetWorldBounds());
    }

    private void OnValidate()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        ConfigureLineRenderer();
    }

    private WorldBounds GetWorldBounds()
    {
        return _worldProvider.World.Bounds;
    }

    private void ConfigureLineRenderer()
    {
        if (_lineRenderer == null)
        {
            return;
        }

        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = 4;
        _lineRenderer.widthMultiplier = Mathf.Max(0.01f, _lineWidth);
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;

        if (_lineRenderer.sharedMaterial == null)
        {
            _lineRenderer.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
        }
    }

    private void DrawBorder(WorldBounds bounds)
    {
        if (_lineRenderer == null)
        {
            return;
        }

        _lineRenderer.SetPosition(0, new Vector3(bounds.MinX, bounds.MinY, 0f));
        _lineRenderer.SetPosition(1, new Vector3(bounds.MinX, bounds.MaxY, 0f));
        _lineRenderer.SetPosition(2, new Vector3(bounds.MaxX, bounds.MaxY, 0f));
        _lineRenderer.SetPosition(3, new Vector3(bounds.MaxX, bounds.MinY, 0f));
    }
}
