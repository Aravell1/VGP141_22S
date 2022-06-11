using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGP141.DataStructures;
using Node = VGP141.DataStructures.DirectedGraph<UnityEngine.Vector3>.Node;

namespace VGP141
{
    public abstract class UnitView : Subject, IObserver
    {
        [Range(2, 10)]
        public int PatrolPointCount;

        private UnitData _unitData;
        private DirectedGraph<Vector3> patrolPath;
        private Node _targetGraphNode;
        private Transform _targetTransform;
        private PlayerController _playerController;

        public void Initialize(UnitData data)
        {
            _unitData = data;

            // Create our patrol path
            Vector3[] positions = new Vector3[PatrolPointCount];
            for (int i = 0; i < PatrolPointCount; i++)
            {
                positions[i] = new Vector3(Random.Range(BuildMenu.X_RANGE.x, BuildMenu.X_RANGE.y),
                    0, Random.Range(BuildMenu.Z_RANGE.x, BuildMenu.Z_RANGE.y));
                patrolPath.AddNode(positions[i]);
            }

            for (int i = 0; i < PatrolPointCount; i++)
            {
                if (i + 1 == PatrolPointCount)
                {
                    patrolPath.AddEdge(positions[i], positions[0]);
                    break;
                }
                patrolPath.AddEdge(positions[i], positions[i + 1]);
            }

            // Set starting node
            SetTarget(patrolPath.FindNode(positions[0]));

            _playerController = FindObjectOfType<PlayerController>();
        }

        protected override void Awake()
        {
            base.Awake();
            patrolPath = new DirectedGraph<Vector3>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_targetTransform != null)
            {
                if (Vector3.Distance(transform.position, _targetTransform.position) > _unitData.AttackRange)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                    _targetTransform.position, _unitData.MoveSpeed * Time.deltaTime);
                }
                else
                {
                    // Start to attack the player
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, _targetGraphNode.Data) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                    _targetGraphNode.Data, _unitData.MoveSpeed * Time.deltaTime);
                }
                else
                {
                    SetTarget(_targetGraphNode.Outgoing[0]);
                }
            }
        }

        private void SetTarget(Node targetNode)
        {
            _targetGraphNode = targetNode;
        }

        private void SetTarget(Transform target)
        {
            _targetTransform = target;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Bullet>() != null)
            {
                Notify(Events.ATTACKED);
                SetTarget(_playerController.transform);
            }
        }

        public void OnNotify(string eventId)
        {
            switch (eventId)
            {
                case Events.ATTACKED:
                    SetTarget(_playerController.transform);
                    break;
                default:
                    break;
            }
        }

        internal void OnUnitCreated(object sender, UnitView unitView)
        {
            AddObserver(unitView);
            unitView.AddObserver(this);
        }
    }
}