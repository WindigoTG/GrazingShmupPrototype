using UnityEngine;
using System.Linq;

namespace GrazingShmup
{
	[ExecuteInEditMode]
	public class EnemyRoute : MonoBehaviour
	{
		[SerializeField] Color _lineColor = Color.red;
		private Vector3[] _nodes;
		[SerializeField, Range(0.1f, 5.0f)] private float _radius = 0.5f;

		[SerializeField] Color _resultBezierColor = Color.green;

		[SerializeField] int _segmentsCount = 25;
		private Vector3[] _positions;

		[SerializeField] bool _isLooped;
		[SerializeField] bool _shouldFollowScreen;
		[SerializeField] EnemyType _enemyType;

		void Update()
		{

		}

		private void OnDrawGizmos()
		{
			_positions = new Vector3[_segmentsCount];

			_nodes = GetComponentsInChildren<Transform>().Skip(1).
		Select(t => t.position).ToArray();

			for (int i = 0; i < _segmentsCount; i++)
			{
				float t = (float)i / (float)(_segmentsCount - 1);
				Vector3 point = CalculateBezierPoint(t, _nodes[0], _nodes[1], _nodes[2], _nodes[3]);
				_positions[i] = point;
			}

			Gizmos.color = _lineColor;
			for (var i = 0; i < _nodes.Length; i++)
			{
				var currentNode = _nodes[i];
				var previousNode = Vector3.zero;
				if (i > 0)
				{
					previousNode = _nodes[i - 1];
				}
				else if (i == 0 && _nodes.Length > 1)
				{
					if (_isLooped)
						previousNode = _nodes[_nodes.Length - 1];
					else
						previousNode = currentNode;
				}
				Gizmos.DrawLine(previousNode, currentNode);
				Gizmos.DrawWireSphere(currentNode, _radius);
			}

			Gizmos.color = _resultBezierColor;
			for (var i = 0; i < _positions.Length; i++)
			{
				var currentNode = _positions[i];
				var previousNode = Vector3.zero;
				if (i > 0)
				{
					previousNode = _positions[i - 1];
				}
				else if (i == 0 && _positions.Length > 1)
				{
					if (_isLooped)
						previousNode = _positions[_positions.Length - 1];
					else
						previousNode = currentNode;
				}
				Gizmos.DrawLine(previousNode, currentNode);
			}
		}

		Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			float u = 1 - t;
			float tt = t * t;
			float uu = u * u;
			float uuu = uu * u;
			float ttt = tt * t;
			Vector3 p = uuu * p0;
			p += 3 * uu * t * p1;
			p += 3 * u * tt * p2;
			p += ttt * p3;
			return p;
		}

		public Vector3[] GetRoute()
		{
			_nodes = GetComponentsInChildren<Transform>().Skip(1).
				Select(t => t.position).ToArray();

			_positions = new Vector3[_segmentsCount];

			for (int i = 0; i < _segmentsCount; i++)
			{
				float t = (float)i / (float)(_segmentsCount - 1);
				Vector3 point = CalculateBezierPoint(t, _nodes[0], _nodes[1], _nodes[2], _nodes[3]);
				_positions[i] = point;
			}

			return _positions;
		}

		public EnemyType EnemyType => _enemyType;
	}
}