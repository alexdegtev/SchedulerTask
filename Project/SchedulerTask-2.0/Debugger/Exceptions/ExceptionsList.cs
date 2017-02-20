using System.Collections;
using System.Collections.Generic;

namespace Debugger.Exceptions
{
    public class ExceptionsList : ICollection<Exception>
    {
        public string CollectionName;
        private List<Exception> exceptions;

        public ExceptionsList()
        {
            this.exceptions = new List<Exception>();
        }
        public ExceptionsList(List<IException> exceptions)
        {
            this.exceptions = new List<Exception>();

            foreach (var exception in exceptions)
            {
                this.exceptions.Add(exception as Exception);
            }
        }

        public void Add(Exception item)
        {
            exceptions.Add(item);
        }

        public void Clear()
        {
            exceptions.Clear();
        }

        public bool Contains(Exception item)
        {
            return exceptions.Contains(item);
        }

        public void CopyTo(Exception[] array, int arrayIndex)
        {
            exceptions.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return exceptions.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Exception item)
        {
            return exceptions.Remove(item);
        }

        public IEnumerator<Exception> GetEnumerator()
        {
            return exceptions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return exceptions.GetEnumerator();
        }

        public List<Exception> getExeptionList()
        {
            return exceptions;
        }
    }
}
