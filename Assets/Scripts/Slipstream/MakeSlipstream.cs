using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSlipstream : MonoBehaviour {

    public Material trailMaterial;
    public float lifeTime = 5.0f;
    public float width = 0.1f;
    public float trailMinDistance = 0.10f;
    public Vector3 directionToCamera = new Vector3(0, 0, -1);
    public bool isTrigger = true;
    public bool makeTrail = true;

    private Transform transformToFollow;
    private Mesh mesh;
    private new PolygonCollider2D collider;

    private LinkedList<Vector3> trailPositions;
    private LinkedList<Vertex> leftVertices;
    private LinkedList<Vertex> rightVertices;


    private void Awake() {
        GameObject trail = new GameObject("Slipstream", new[] { typeof(MeshRenderer), typeof(MeshFilter), typeof(PolygonCollider2D) });
        mesh = trail.GetComponent<MeshFilter>().mesh = new Mesh();
        trail.GetComponent<Renderer>().material = trailMaterial;

        collider = trail.GetComponent<PolygonCollider2D>();
        collider.isTrigger = isTrigger;
        collider.SetPath(0, null);

        transformToFollow = base.transform;

        trailPositions = new LinkedList<Vector3>();
        trailPositions.AddFirst(transform.position);

        leftVertices = new LinkedList<Vertex>();
        rightVertices = new LinkedList<Vertex>();
    }


    private void Update() {
        if(makeTrail) {
            AddVertices();
            RemoveVertices();
        }
    }


    private void AddVertices() {
        if((trailPositions.First.Value - transformToFollow.position).sqrMagnitude > trailMinDistance * trailMinDistance) {
            Vector3 directionToCurrentPosition = (transformToFollow.position - trailPositions.First.Value).normalized;

            Vector3 leftPosition = transformToFollow.position + (Vector3.Cross(directionToCamera, directionToCurrentPosition) * -width * 0.5f);
            Vector3 rightPosition = transformToFollow.position + (Vector3.Cross(directionToCamera, directionToCurrentPosition) * width * 0.5f);

            leftVertices.AddFirst(new Vertex(leftPosition, transformToFollow.position, (leftPosition - transformToFollow.position).normalized));
            rightVertices.AddFirst(new Vertex(rightPosition, transformToFollow.position, (rightPosition - transformToFollow.position).normalized));

            trailPositions.AddFirst(transformToFollow.position);
            SetMesh();
        }
    }


    private void RemoveVertices() {
        LinkedListNode<Vertex> currentVertex = leftVertices.Last;
        while(currentVertex != null && currentVertex.Value.TimeAlive > lifeTime) {
            leftVertices.RemoveLast();
            rightVertices.RemoveLast();
            trailPositions.RemoveLast();
            SetMesh();
            currentVertex = leftVertices.Last;
        }
    }


    private void SetMesh() {
        if(trailPositions.Count >= 2) {
            Vector3[] vertices = new Vector3[trailPositions.Count * 2];
            Vector2[] uvs = new Vector2[trailPositions.Count * 2];
            int[] triangles = new int[(trailPositions.Count - 1) * 6];
            Vector2[] colliderPath = new Vector2[(trailPositions.Count - 1) * 2];

            LinkedListNode<Vertex> leftVertexNode = leftVertices.First;
            LinkedListNode<Vertex> rightVertexNode = rightVertices.First;

            float timeDelta = leftVertices.Last.Value.TimeAlive - leftVertices.First.Value.TimeAlive;

            for(int i = 0; i < leftVertices.Count; ++i) {
                Vertex leftVertex = leftVertexNode.Value;
                Vertex rightVertex = rightVertexNode.Value;

                int vertIndex = i * 2;
                vertices[vertIndex] = leftVertex.Position;
                vertices[vertIndex + 1] = rightVertex.Position;

                colliderPath[i] = leftVertex.Position;
                colliderPath[colliderPath.Length - (i + 1)] = rightVertex.Position;

                float uvValue = leftVertex.TimeAlive / timeDelta;
                uvs[vertIndex] = new Vector2(uvValue, 0);
                uvs[vertIndex + 1] = new Vector2(uvValue, 1);

                if(i > 0) {
                    int triIndex = (i - 1) * 6;
                    triangles[triIndex] = vertIndex - 2;
                    triangles[triIndex + 1] = vertIndex - 1;
                    triangles[triIndex + 2] = vertIndex + 1;
                    triangles[triIndex + 3] = vertIndex - 2;
                    triangles[triIndex + 4] = vertIndex + 1;
                    triangles[triIndex + 5] = vertIndex;
                }

                leftVertexNode = leftVertexNode.Next;
                rightVertexNode = rightVertexNode.Next;
            }

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            if(isTrigger) {
                collider.SetPath(0, colliderPath);
            }
        }
    }



    private class Vertex {
        private Vector3 centerPosition;
        private Vector3 derivedDirection;
        private float creationTime;

        public Vector3 Position { get; private set; }
        public float TimeAlive { get { return Time.time - creationTime; } }

        public void AdjustWidth(float width) {
            Position = centerPosition + (derivedDirection * width);
        }

        public Vertex(Vector3 position, Vector3 centerPosition, Vector3 derivedDirection) {
            this.Position = position;
            this.centerPosition = centerPosition;
            this.derivedDirection = derivedDirection;
            creationTime = Time.time;
        }
    }
}
