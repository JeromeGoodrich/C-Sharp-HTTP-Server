[![Build status](https://ci.appveyor.com/api/projects/status/8n7gcbchs56fgnby?svg=true)](https://ci.appveyor.com/project/Jgoodrich07/c-sharp-http-server)

# HTTP Server In C\# #

**System Requirements**

- windows machine
- windows git client e.g. git bash
- java (needed to run cob\_spec test suite)

**Important**
Before cloning ensure your .gitconfig file contains the following lines:

    [core]
    longpaths = true

this will allow you to clone the repo even if the path exceeds 
Windows allotted 260 characters.


- Clone the repo: 
`git clone git@github.com:Jgoodrich07/C-Sharp-HTTP-Server.git`

- Update the cob\_spec s
ubmodule:
`git submodule update --init --recursive`

####Run CobSpecServer against cobspec test suite

**start the fitnesse server:**

	java -jar fitnesse.jar -p 9090

type "http://localhost:9090" into your browser

**start CobSpecServer:**

from the root directory `cd CobSpecServer\bin\Release`

start the server by typing the name of the executable
It should be `CobSpecServer.exe` 

**Options:**

	-p	The port number cob_spec is listening on. default is 5000
	-d	The absolute path to the public directory you wish to serve.
	-l	The path to the log file you wish to use. 

**Example :** `CobSpecServer.exe -p 5000 -l /Documents/logfile.txt -d /My/Directory`

*Note* the public directory can be any directory. It just must be a directory with the contents that cob_spec expects in order to pass the tests.

Once the server starts you can run the tests 
using the interface in your browser


