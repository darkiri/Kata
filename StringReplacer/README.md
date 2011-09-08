StringReplacer 
--------------
_aka DictionaryReplacer_

_http://codingdojo.org/cgi-bin/wiki.pl?KataDictionaryReplacer_

Create a method that takes a string and a dictionary, and replaces every key in the dictionary pre and suffixed with a dollar sign, with the corresponding value from the Dictionary.

	Dictionary:	[Name:World]
	Input:		Hello, $Name$!
	Output:		Hello, World!


When _$..$_ placeholder in the string not found in the dictionary then it should be removed from the string.

	Dictionary:	[AnotherName:World]
	Input:		Hello, $Name$!
	Output:		Hello, !


When a key in the dictionary contains some placeholder for another key, then it should also be replaced.

	Dictionary:	[Name:World; Header:'Hello, $Name$']
	Input:		$Header$
	Output:		Hello, World!

