To setup the TimeService web service on your IIS web server, 
use IIS to create a IIS virtual directory called TimeServer, 
pointing at the real folder C:\Inetpub\wwwroot\TimeServer.
This real folder should have been created when you un-zipped
the zip file containing the source code.

Then in Visual Studio 2003, load the TimeServer solution and set
a web reference to the TimeService web service (using the 
Project...Add web reference menu item). The web service can be 
found at the ISS virtual directory that you've just created.



