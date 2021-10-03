namespace Core.Entities
{
    public class Topic : BaseEntity
    {
        public string Name {get; private set;}

        protected Topic()
        {
        }

        protected Topic(string name)
        {
            Name = name;
        }

        public static Topic Create(string name)
            => new Topic(name);
    }
}