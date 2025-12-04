

namespace Watchables
{
  public abstract class BoundedWatchable<T> : SealableNestedBase<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private IValueWrapper<T> _minimum;
    private IValueWrapper<T> _maximum;

    public IValueWrapper<T> Minimum => _minimum;
    public IValueWrapper<T> Maximum => _maximum;

    public BoundedWatchable(IValueWrapper<T> minimum, IValueWrapper<T> maximum)
    {
      NumericUtility.ValidateType(typeof(T));
      SetBound(ref _minimum, minimum);
      SetBound(ref _maximum, maximum);
    }

    public bool SetMinimum(IValueWrapper<T> minimum, object owner = null)
    {
      return MutationGuard(() => SetBound(ref _minimum, minimum), owner);
    }

    public bool SetMaximum(IValueWrapper<T> maximum, object owner = null)
    {
      return MutationGuard(() => SetBound(ref _maximum, maximum), owner);
    }

    protected override void OnDestroyed(IWatchable self)
    {
      ClearBound(ref _minimum);
      ClearBound(ref _maximum);
    }

    protected void SetBound(ref IValueWrapper<T> field, IValueWrapper<T> value)
    {
      ClearBound(ref field);
      if(value is IWatchable)
      {
        Register(value as IWatchable);
      }
      field = value;
      Evaluate();
    }

    protected void ClearBound(ref IValueWrapper<T> field)
    {
      if(field == null) { return; }
      if(field is IWatchable)
      {
        Unregister(field as IWatchable);
      }
      field = null;
    }

  }
}