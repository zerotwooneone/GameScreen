using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace GameScreen.Pageable
{
    public class PageableObservableCollection<T> : ObservableCollection<T>
    {
        public bool HasMore { get; protected set; }
        protected bool SupressNotification;

        public PageableObservableCollection() : base()
        {
            HasMore = false;
            SupressNotification = false;
        }

        public PageableObservableCollection(IEnumerable<T> collection, bool hasMore) : base(collection)
        {
            HasMore = hasMore;
            SupressNotification = false;
        }

        public PageableObservableCollection(IPageable<T> collection) : base(collection)
        {
            HasMore = collection.HasMore;
        }

        public void Add(IPageable<T> newCollection)
        {
            SupressNotification = true;
            try
            {
                foreach (var item in newCollection)
                {
                    Add(item);
                }
            }
            finally
            {
                SupressNotification = false;
            }
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            HasMore = newCollection.HasMore;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SupressNotification)
            {
                return;
            }
            base.OnCollectionChanged(e);
        }
    }
}