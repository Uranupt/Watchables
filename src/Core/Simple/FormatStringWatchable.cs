

namespace Watchables
{
  /// <summary>
  /// An <see cref="IWatchable{T}"/> implementation whose value is the result of <see cref="string.Format(string, object[])"/>
  /// performed with a given format string and array of inputs. Will automatically update from any <see cref="IWatchable"/> inputs. 
  /// </summary>
  public sealed class FormatStringWatchable : NestedWatchable<string>
  {

    private readonly string _format;
    private readonly object[] _inputs;
    private readonly string[] _replacements;
    private readonly string _replacement;
    private readonly bool _inputsRequired = true;

    /// <summary>
    /// Default constructor which does not allow for replacement on destruction of <see cref="IWatchable"/> <paramref name="inputs"/>. If any supplied <paramref name="inputs"/> are
    /// already destroyed, the <see cref="FormatStringWatchable"/> will immediately destroy itself.
    /// </summary>
    public FormatStringWatchable(string format, params object[] inputs)
    {
      _format = format;
      _inputs = inputs;
      OnConstruction();
    }

    /// <summary>
    /// Constructor which allows specification for a <see cref="string"/> to be used as a <paramref name="replacement"/> for destroyed <see cref="IWatchable"/> inputs.
    /// </summary>
    public FormatStringWatchable(string format, string replacement, params object[] inputs)
    {
      _inputsRequired = false;
      _format = format;
      _inputs = inputs;
      _replacement = replacement;
      OnConstruction();
    }


    /// <summary>
    /// Constructor which allows specification of <see cref="string"/>s to be used as replacements for their paired inputs if they are <see cref="IWatchable"/> and become destroyed.
    /// </summary>
    public FormatStringWatchable(string format, params (object input, string replacement)[] inputs)
    {
      _inputsRequired = false;
      _format = format;
      _inputs = new object[inputs.Length];
      _replacements = new string[inputs.Length];
      for(int i = 0; i < inputs.Length; i++)
      {
        _inputs[i] = inputs[i].input;
        _replacements[i] = inputs[i].replacement;
      }
      OnConstruction();
    }

    /// <inheritdoc/>
    protected override bool CheckFatalDestruction(IWatchable dependency) => _inputsRequired;

    /// <inheritdoc/>
    protected override void Evaluate()
    {
      Value = string.Format(_format, _inputs);
    }

    /// <inheritdoc/>
    protected override void OnNonFatalDestruction(IWatchable dependency) => FindAndReplace(dependency);

    /// <inheritdoc/>
    protected override void BeforeDestroyed()
    {
      foreach(object input in _inputs)
      {
        if(input is IWatchable watchable)
        {
          Unregister(watchable);
        }
      }
    }

    /// <inheritdoc/>
    protected override void ClearValue() => Value = "";

    private void FindAndReplace(object input)
    {
      int index = -1;
      for(int i = 0; i < _inputs.Length; i++)
      {
        if(_inputs[i] == input)
        {
          index = i;
          break;
        }
      }
      if(index < 0) { return; }
      if(_replacements != null)
      {
        _inputs[index] = _replacements[index];
      }
      else if(_replacement != null)
      {
        _inputs[index] = _replacement;
      }
    }

    private void OnConstruction()
    {
      for(int i = 0; i < _inputs.Length; i++)
      {
        object input = _inputs[i];
        if(input is IWatchable watchable)
        {
          if(watchable.IsDestroyed)
          {
            if(_inputsRequired)
            {
              Destroy(_owner);
              return;
            }
            else
            {
              FindAndReplace(input);
            }
            continue;
          }
          Register(watchable);
        }
      }
      Evaluate();
    }

  }
}
