# Indd-Proxy
## C# console app to handle InDesign Documents with Indesign Server
### Prerequissites
- Visual Studio 2017 64 Bit
- Installed Adobe Indesign Server (cc2017). Should work with 2015, 2016 too

### Install
- Open Project "Indd.sln" in Visual Studio
- You need the IndesignServer.dll in the references: http://magento2-tuts.blogspot.de/2017/04/connect-to-indesignserver-api-with-c-in.html 
- add MicrosoftVisualBasic.dll to your references
- Project->create
- Run debug (that creates the testfxures)
- Create a volumne with Z: following paths on on your windows: 
- Copy all folders from your <projectroot>Indd-Proxy\bin\Debug\Tests\Functional\Fixures\* to Z:    

### Run Tests
- In Testwindow you can nnow run all nunit tests. All Tests are passed green means, you have installed everything right.

## current state
- Commandline abstraction works 
- Central storageconfig gets loaded
- Adobe IndesignServer instance gets crearted successfully
- Json Commands gets successfully converted to a dynamic commandlist
- Commands gets executed
- All available tests are running
- IndesignDocument gets lodad
- Syslog gets written
- IndesignServerinstance gets successfully created

### Available commands
#### Document
- Document.Open
- Document.SaveAndClose
- Document.ExportPDF
- Document.CreateCopy
#### Images
- Images.RelinkAll
- Images.SetLinks
