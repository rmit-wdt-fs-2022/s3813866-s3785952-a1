namespace Main.Sql;

public interface IManager<T>
{
    public void Add(T t);

    public List<T> CheckTable();

}