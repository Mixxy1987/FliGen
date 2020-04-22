namespace FliGen.Common
{
    public interface IFactory<out T>
    {
        T Create();
    }
}