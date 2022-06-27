using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField] private float AttachSpeed;
    private Vector3 _defaultPosition;
    private bool _foundPlayer;

    private TweenerCore<Vector3, Vector3, VectorOptions> _returnRoutine;
    private Unit _unit;
    [SerializeField] private Unit Unit;

    private void Start()
    {
        _defaultPosition = Unit.transform.position;
    }

    private void Update()
    {
        if (!_foundPlayer) return;
        MoveToUnit();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Unit otherUnit))
        {
            if (otherUnit.UnitState != UnitState.Attached) return;
            _unit = otherUnit;
            _foundPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Unit otherUnit))
            if (otherUnit.Equals(_unit))
            {
                _foundPlayer = false;
                ReturnToDefaultPosition();
            }
    }

    private void MoveToUnit()
    {
        if (Vector3.Distance(_unit.transform.position, Unit.transform.position) < 0.001f ||
            Unit.UnitState == UnitState.Attached)
        {
            gameObject.SetActive(false);
            _foundPlayer = false;
            _returnRoutine?.Kill();
            return;
        }

        var position = _unit.transform.position + _unit.GetAttachOffset();
        Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, position, AttachSpeed * Time.deltaTime);
    }

    private void ReturnToDefaultPosition()
    {
        _returnRoutine = Unit.transform.DOMove(_defaultPosition, AttachSpeed).SetSpeedBased().SetEase(Ease.InOutSine);
    }
}