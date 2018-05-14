using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Properties
{
	private Dictionary<string, string> list;
	private string filename;

	public Properties(string file)
	{
		reload(file);
	}

	public string get(string field, string defValue)
	{
		return (get(field) == null) ? (defValue) : (get(field));
	}
	public string get(string field)
	{
		return (list.ContainsKey(field)) ? (list[field]):(null);
	}

	public void set(string field, object value)
	{
		if (!list.ContainsKey (field)) {
			list.Add (field, value.ToString ());
		} else {
			list [field] = value.ToString ();
		}
	}

	public void save()
	{
		save(this.filename);
	}

	public void save(string filename)
	{
		this.filename = filename;

		if (!System.IO.File.Exists(filename))
			System.IO.File.Create(filename);

		System.IO.StreamWriter file = new System.IO.StreamWriter(filename);

		foreach (string prop in list.Keys)
			if (string.IsNullOrEmpty (list [prop])) {
				file.WriteLine (prop + "=" + list [prop]);
			}
		file.Close();
	}

	public void reload()
	{
		reload(this.filename);
	}

	public void reload(string filename)
	{
		this.filename = filename;
		list = new Dictionary<string, string>();

		if (System.IO.File.Exists (filename)) {
			loadFromFile (filename);
		} else {
			System.IO.File.Create (filename);
		}
	}

	public string loadProperty(string field)
	{
		if (list.ContainsKey (field)) {
			return list [field];
		}
		return null;
	}

	private void loadFromFile(string file)
	{
		foreach (string line in System.IO.File.ReadAllLines(file))
		{
			if ((!string.IsNullOrEmpty(line)) &&
				(!line.StartsWith(";")) &&
				(!line.StartsWith("#")) &&
				(!line.StartsWith("'")) &&
				(line.Contains("=")))
			{
				int index = line.IndexOf('=');
				string key = line.Substring(0, index).Trim();
				string value = line.Substring(index + 1).Trim();

				if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
					(value.StartsWith("'") && value.EndsWith("'")))
				{
					value = value.Substring(1, value.Length - 2);
				}

				try
				{
					//ignore dublicates
					list.Add(key, value);
				}
				catch { }
			}
		}
	}


}