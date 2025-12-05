

namespace Watchables
{
  public sealed class OperationStep<T> where T : unmanaged
  {

    public bool IsRequired { get; private set; }
    public IValueWrapper<T> Value { get; private set; }
    internal NumericOperation Operation { get; private set; }

    internal OperationStep(NumericOperation op, IValueWrapper<T> value = null, bool required = false)
    {
      NumericUtility.ValidateType(typeof(T));
      Operation = op;
      Value = value ?? default(T).Wrap();
      IsRequired = required;
    }

  }
}