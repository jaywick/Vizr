Vizr
====

A Modern and humble quick launcher for Windows

![Searching 'labs' in Vizr](PREVIEW.png)

## Pre-alpha notes
* Commands are stored in `defaults.xml` in path of assembly
* The schema is not finalise and is ***guaranteed to change*** during pre-alpha and crash

## Schema
The current build supports the following template

```xml
<VizrPackage Version="0.1">
	<Items>
		<Command Title="Visit Jay Wick Labs" Pattern="Visit Jay Wick Labs" HitCount="1">http://labs.jay-wick.com</Command>
		<Request Title="Search IMDB for '{0}'" Pattern="imdb (.+)" HitCount="0">http://www.imdb.com/find?q={0}</Request>
		<Request Title="Google for '{0}'" Pattern="(.+)" HitCount="1">https://www.google.com/search?q={0}</Request>
		<Request Title="I'm feeling lucky '{0}'" Pattern="(.+)" HitCount="0">https://www.google.com/search?q={0}&amp;btnI</Request>
		<Request Title="Search PC for '{0}'" Pattern="(.+)" HitCount="0">search-ms:query={0}&amp;</Request>
	</Items>
</VizrPackage>
```

## Terms
* Package - a list of entries
* Entry - a command, request or another item which the app will check when you enter some text
* Command - a simple text instruction like `run this file`
* Request - an instruction involving an argument such as `define {0}`
* `{0}` - the placeholder for the argument
