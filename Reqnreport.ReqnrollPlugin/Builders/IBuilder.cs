namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}
