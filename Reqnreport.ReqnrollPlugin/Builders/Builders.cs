using System.Collections;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders
{
    public class Builders<TBuilder, TKey, TResult> : IEnumerable<TBuilder> where TKey : notnull
    {
        private readonly Dictionary<TKey, TBuilder> _builders = [];

        public TBuilder With(TKey key, Func<TKey, TBuilder> creator)
        {
            _builders.TryGetValue(key, out var builder);

            if(builder == null)
            {
                builder = creator(key);

                _builders.Add(key, builder);
            }

            return builder;


            //if (!_builders.ContainsKey(key))
            //{
            //    _builders.Add(key, );
            //}

            //return _builders[key];
        }

        public List<TResult> Build() => _builders.Values.OfType<IBuilder<TResult>>().Select(b => b.Build()).ToList();

        public IEnumerator<TBuilder> GetEnumerator()
        {
            return _builders.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
