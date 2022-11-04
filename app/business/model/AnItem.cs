using System;

namespace app.business.model
{
    /// <summary>
    /// This is a representative example of a business model object. Feel free to delete or rename it at will. 
    /// </summary>
    public class AnItem
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public AnItem(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public AnItem(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
