using NGross.Core.Models;

namespace NGross.Core.Engine.Parser.ThreadAction;

public interface IActionParser
{
    public List<IThreadAction> Parse(Type t);
}