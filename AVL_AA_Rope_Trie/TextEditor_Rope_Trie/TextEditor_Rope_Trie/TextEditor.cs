using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace TextEditor_Rope_Trie
{
	public class TextEditor: ITextEditor
    {

		public Trie<BigList<char>> userString { get; set; }
		public Trie<Stack<string>> userStringHistory { get; set; }

        public TextEditor()
        {
			this.userString = new Trie<BigList<char>>();
			this.userStringHistory = new Trie<Stack<string>>();
        }

		public void Clear(string username)
		{
			if (!this.userString.Contains(username))
                return;
			this.userStringHistory.GetValue(username).Push(string.Join("",this.userString.GetValue(username)));
			this.userString.GetValue(username).Clear();
		}

		public void Delete(string username, int startIndex, int length)
		{
			if (!this.userString.Contains(username))
                return;
			this.userStringHistory.GetValue(username).Push(string.Join("",this.userString.GetValue(username)));
			this.userString.GetValue(username).RemoveRange(startIndex, length);
		}

		public void Insert(string username, int index, string str)
		{
			if (!this.userString.Contains(username))
                return;
			this.userStringHistory.GetValue(username).Push(string.Join("",this.userString.GetValue(username)));
			this.userString.GetValue(username).InsertRange(index, str);
		}

		public int Length(string username)
		{
			return this.userString.GetValue(username).Count;
		}

		public void Login(string username)
		{
			this.userString.Insert(username, new BigList<char>());
			this.userStringHistory.Insert(username, new Stack<string>());
		}

		public void Logout(string username)
		{
			if (!this.userString.Contains(username))
                return;
			this.userString.Delete(username);
			this.userStringHistory.Delete(username);
		}

		public void Prepend(string username, string str)
		{
			if (!this.userString.Contains(username))
                return;
			this.userStringHistory.GetValue(username).Push(string.Join("", this.userString.GetValue(username)));
			this.userString.GetValue(username).AddRangeToFront(str);
		}

		public string Print(string username)
		{
			if (!this.userString.Contains(username))
				return null;
			return string.Join("", this.userString.GetValue(username));
			
		}

		public void Substring(string username, int startIndex, int length)
		{
			if (!this.userString.Contains(username))
                return;
			
			this.userStringHistory.GetValue(username).Push(string.Join("", this.userString.GetValue(username)));
            
			var  newStr = this.userString.GetValue(username).Range(startIndex, length);

			this.userString.GetValue(username).Clear();
			this.userString.GetValue(username).AddRange(newStr);
		}

		public void Undo(string username)
		{
			if (!this.userString.Contains(username))
                return;
			this.userString.GetValue(username).Clear();
			this.userString.GetValue(username).AddRange(this.userStringHistory.GetValue(username).Pop());
		}

		public IEnumerable<string> Users(string prefix = "")
		{
			return this.userString.GetByPrefix(prefix);
		}
	}
}
