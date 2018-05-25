using System;
using System.Text;
using System.Collections.Generic;

[System.Serializable]
public class HistoryQueueBase<T>  {

	protected Queue<T> history;

	public HistoryQueueBase ()
	{
		history = new Queue<T>();
	}

	public virtual void add(T item) 
	{
		//if ( !history.Contains (item)) 
			history.Enqueue (item);
	}


	public virtual void addRange(T[] itemArray) 
	{
		for (int i = 0; i < itemArray.Length; i++) {
			T item = itemArray [i];
			add (item);
		}
	}

	public virtual void addRange(List<T> itemList) 
	{
		for (int i = 0; i < itemList.Count; i++) {
			T item = itemList [i];
			add (item);
		}

	}

	public virtual T get()
	{
		return history.Dequeue ();
	}

	public virtual bool contains(T item)
	{
		return history.Contains (item);
	}

	public virtual T[] getArray()
	{
		return history.ToArray ();
	}

	public virtual List<T> getList()
	{
		return new List<T> (history);
	}


	public override string ToString ()
	{
		StringBuilder sb = new StringBuilder ();
		for (int i = 0; i < history.Count; i++) {
			sb.Append (history.Peek ());
		}	
		return sb.ToString ();
	}

	public virtual void clear()
	{
		history.Clear ();
	}
}
