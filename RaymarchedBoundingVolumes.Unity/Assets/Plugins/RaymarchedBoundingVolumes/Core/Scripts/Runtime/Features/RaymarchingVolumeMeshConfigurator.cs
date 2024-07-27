using System.Linq;
using RaymarchedBoundingVolumes.Utilities;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode, RequireComponent(typeof(MeshFilter))]
    public class RaymarchingVolumeMeshConfigurator : MonoBehaviour
    {
        private const           string  MeshNameFormat = "{0} (ResizedClone)";
        private static readonly Vector3 _defaultSize   = Vector3.one;

        [SerializeField, Unwrapped] private ObservableValue<Vector3> size = new(_defaultSize);

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

            if (_isNeededToChangeMeshSize)
            {
                _isNeededToChangeMeshSize = false;
                ChangeMeshSize(size.Value);
            }
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

        private void ChangeMeshSizeDelayed(Vector3 sizeMultiplier = default) => _isNeededToChangeMeshSize = true;

        public Vector3 ChangeMeshSize(Vector3 sizeMultiplier)
        {
            mesh.vertices = _originalMeshVertices.Select(vertex => Vector3.Scale(vertex, sizeMultiplier)).ToArray();
            mesh.RecalculateBounds();

            return sizeMultiplier;
        }

        [ContextMenu(nameof(SetMeshForAllRaymarchedObjects))]
        private void SetMeshForAllRaymarchedObjects()
        {
            GameObject[] rootGameObjects = gameObject.scene.GetRootGameObjects();
            foreach (GameObject rootGameObject in rootGameObjects)
                SetMeshForAllChildRaymarchedObjects(rootGameObject);
        }

        [ContextMenu(nameof(SetMeshForAllChildRaymarchedObjects))]
        private void SetMeshForAllChildRaymarchedObjects() => SetMeshForAllChildRaymarchedObjects(gameObject);

        private void SetMeshForAllChildRaymarchedObjects(GameObject rootGameObject)
        {
            foreach (RaymarchedObject raymarchedObject in rootGameObject.GetComponentsInChildren<RaymarchedObject>())
                raymarchedObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        }
    }
}