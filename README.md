Vizr
====

A Modern and humble quick launcher for Windows

![](PREVIEW.png)

## Pre-alpha notes
* Open `commands.xml` at assembly path and add to modify commands
* The schema is not finalise and is ***guaranteed to change*** during pre-alpha and crash

## Schema
The current build supports the following template

	<VizrPackage Version="0.1" Enabled="true">
		<Items>
			<Command Title="Visit Jay Wick Labs" Pattern="Visit Jay Wick Labs" HitCount="1">http://labs.jay-wick.com</Command>
			<Request Title="Search IMDB for '{0}'" Pattern="imdb (.+)" HitCount="0">http://www.imdb.com/find?q={0}</Request>
			<Request Title="Google for '{0}'" Pattern="(.+)" HitCount="1">https://www.google.com/search?q={0}</Request>
			<Request Title="I'm feeling lucky '{0}'" Pattern="(.+)" HitCount="0">https://www.google.com/search?q={0}&amp;btnI</Request>
			<Request Title="Search PC for '{0}'" Pattern="(.+)" HitCount="0">search-ms:query={0}&amp;</Request>
		</Items>
	</VizrPackage>
