using System.Linq;
using RBV.Utilities;
using RBV.Utilities.Extensions;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.Features
{
    [ExecuteInEditMode, RequireComponent(typeof(MeshFilter))]
    public class RaymarchedVolumeMeshConfigurator : MonoBehaviour
    {
        private const           string  MeshNameFormat = "{0} (ResizedClone)";
        private static readonly Vector3 _defaultSize   = Vector3.one;

        [SerializeField] private ObservableValue<Vector3> size = new(_defaultSize);

        [SerializeField, HideInInspector] private Mesh mesh;

        private bool       _isNeededToChangeMeshSize;
        private MeshFilter _meshFilter;
        private Vector3[]  _originalMeshVertices;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();

            if (mesh != default)
                _originalMeshVertices = mesh.vertices.Select(vertex => vertex.Unscale(size.Value)).ToArray();
        }

        private void Update()
        {
            if (mesh != _meshFilter.sharedMesh)
                CreateResizedMesh();

            _isNeededToChangeMeshSize.IfYesInvoke(() => ChangeMeshSize(size.Value)).IfYesSet(false);
        }

        private void OnEnable()  => size.Changed += ChangeMeshSizeDelayed;
        private void OnDisable() => size.Changed -= ChangeMeshSizeDelayed;

        private Mesh CreateResizedMesh()
        {
            Mesh sharedMesh = _meshFilter.sharedMesh;
            _originalMeshVertices = sharedMesh.vertices;

            string sharedMeshName   = string.Format(MeshNameFormat, sharedMesh.name);
            _meshFilter.mesh = mesh = Instantiate(_meshFilter.sharedMesh);
            mesh.name        = sharedMeshName;

            if (size.Value != _defaultSize)
                ChangeMeshSizeDelayed();
            return mesh;
        }

        private void ChangeMeshSizeDelayed(ChangedValue<Vector3> sizeMultiplier = default) =>
            _isNeededToChangeMeshSize = true;

        public Vector3 ChangeMeshSize(Vector3 sizeMultiplier)
        {
            mesh.vertices = _originalMeshVertices.Select(vertex => Vector3.Scale(vertex, sizeMultiplier)).ToArray();
            mesh.RecalculateBounds();

            return sizeMultiplier;
        }

        [ContextMenu(nameof(SetMeshForAllChildren))]
        private void SetMeshForAllChildren() =>
            SetMeshForAllChildren(gameObject);

        [ContextMenu(nameof(SetMeshForAllChildRaymarchingOperations))]
        private void SetMeshForAllChildRaymarchingOperations() =>
            SetMeshForAllChildren<RaymarchingOperation>(gameObject);

        [ContextMenu(nameof(SetMeshForAllChildRaymarchedObjects))]
        private void SetMeshForAllChildRaymarchedObjects() =>
            SetMeshForAllChildren<RaymarchedObject>(gameObject);

        private void SetMeshForAllChildren<T>(GameObject parent) where T : Component
        {
            foreach (T component in parent.GetComponentsInChildren<T>())
                if (component.TryGetComponent(out MeshFilter meshFilter))
                    meshFilter.sharedMesh = mesh;
        }

        private void SetMeshForAllChildren(GameObject parent)
        {
            foreach (MeshFilter meshFilter in parent.GetComponentsInChildren<MeshFilter>())
                meshFilter.sharedMesh = mesh;
        }
    }
}