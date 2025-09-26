using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class FakeDbSet<T> : DbSet<T>, IQueryable<T>, IAsyncEnumerable<T> where T : class
{
    private readonly IQueryable<T> queryable;

    public FakeDbSet(IEnumerable<T> data)
    {
        queryable = data.AsQueryable();
    }

    public override IEntityType EntityType { get; }
    public Type ElementType => queryable.ElementType;

    public Expression Expression => queryable.Expression;

    public IQueryProvider Provider => new AsyncQueryProvider<T>(queryable.Provider);

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new AsyncEnumerator<T>(queryable.GetEnumerator());
    }

    public IEnumerator<T> GetEnumerator() => queryable.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class AsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider inner;

    public AsyncQueryProvider(IQueryProvider inner)
    {
        this.inner = inner;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        return new AsyncEnumerable<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new AsyncEnumerable<TElement>(expression);
    }

    public object Execute(Expression expression)
    {
        return inner.Execute(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return inner.Execute<TResult>(expression);
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        // EF Core ожидает, что здесь возвращается TResult, а не Task<TResult>
        return Execute<TResult>(expression);
    }
}

internal class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public AsyncEnumerable(IEnumerable<T> enumerable)
        : base(enumerable) { }

    public AsyncEnumerable(Expression expression)
        : base(expression) { }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);
}

internal class AsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> inner;

    public AsyncEnumerator(IEnumerator<T> inner)
    {
        this.inner = inner;
    }

    public T Current => inner.Current;

    public ValueTask DisposeAsync()
    {
        inner.Dispose();
        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(inner.MoveNext());
    }
}
