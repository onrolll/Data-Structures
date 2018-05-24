using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TextEditor_Rope_Trie
{
    class Program
    {
        static void Main(string[] args)
        {
			var te = new TextEditor();

			var command = Console.ReadLine();
			var regex = new Regex("\"(.*)\"");
			while (command != "end")
			{
				var match = regex.Match(command);
 				var tokens = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				bool firstSwitch = false;
                switch(tokens[0])
				{
					case "login":
						te.Login(tokens[1]);
						firstSwitch = true;
						break;

					case "logout":
						te.Logout(tokens[1]);
						firstSwitch = true;
						break;

					case "users":
						firstSwitch = true;
						IEnumerable<string> users;

						if (tokens.Length != 1)
							users = te.Users(tokens[1]);
						else
                            users = te.Users();
						
                        foreach (var user in users)                     
                            Console.WriteLine(user);
						
						break;

					default:
						break;
				}
				if (!firstSwitch)
				{
					var str = match.ToString().Trim(new char[]{'"'});
					switch (tokens[1])
					{
						case "insert":
							te.Insert(tokens[0], int.Parse(tokens[2]), str);
							break;
						case "prepend":
							te.Prepend(tokens[0], str);
							break;
						case "substring":
							te.Substring(tokens[0], int.Parse(tokens[2]), int.Parse(tokens[3]));
							break;
						case "delete":
							te.Delete(tokens[0], int.Parse(tokens[2]), int.Parse(tokens[3]));
							break;
						case "clear":
							te.Clear(tokens[0]);
							break;
						case "print":
							var print = te.Print(tokens[0]);
							if (print == "" || print == null)
								break;
							Console.WriteLine(print);
							break;
						case "length":
							Console.WriteLine(te.Length(tokens[0]));
							break;
						case "undo":
							te.Undo(tokens[0]);
							break;
						default:
							break;                     
					}
				}
				command = Console.ReadLine();
              
			}

		}
    }
}
