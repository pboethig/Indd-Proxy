# Indd-Proxy
## C# console app to handle InDesign documents with Indesign Server

As a backend base to colaborate on marketing materials like flyer, posters, businesscards there is a need to centralise all designarts from Indesign and share it accross your projects. With Indesign itself that is a painfull marketingproccess. IndesignServer is a ui less version of Indesign, so you can compute your requests to this server and the server responds his dones. For that you need something like a service-facade, which interacts with the IndesignServer.dll and exchanges commandmessages from the webclient and the other way arround.

Here comes Indd-Proxy into the game. It takes json payloads with commands for the server like "exporting as pdf, set a new image on a specific frame, set a new text etc". After Indd-Proxy finished his task, he will notify the client with a http request and a client defined payload, so called webhooks. 

### Prerequisites
- Visual Studio 2017 64 Bit
- .Net Framework >=4.5.x
- Installed Adobe Indesign Server (cc2017). Should work with 2015, 2016 too

### Install
- Open Project "Indd.sln" in Visual Studio
- You need the IndesignServer.dll in the references: http://magento2-tuts.blogspot.de/2017/04/connect-to-indesignserver-api-with-c-in.html 
- add MicrosoftVisualBasic.dll to your references
- Project->create

### Run Tests
- In your Visual Studio testwindow you can now run all nunit tests. All Tests are passed green means, you have installed everything right.

### usage

- Compile or debug project to get the Indd-Proxy.exe
- create your json commands in a json file 
- pass the path to the command.json with the payload inside. (See wiki docs)

```
Indd-Proxy.exe -f <path to json payload>
```

## Available commands
### JSON Commandlist Payloads

For all available commands see further docs: https://github.com/pboethig/Indd-Proxy/wiki/Command-payloads
