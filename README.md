# PMD: Shift! Core Server

## Downloading

To download this repository, run:
```
git clone --recursive https://github.com/PMDShift/Server
```

This will download the core server files, as well as all dependancies.

## Configuring Your Database

You will need to install [MySQL](http://dev.mysql.com/downloads/mysql/). The prefered version is 8.0, although the server will likely run with later versions.

Once installed, extract the files from the *Content_Data.zip* file. 
Run *LoadTableBackup.bat*. You will be prompted for your database username, database password, and database version. Input those fields and your database will be loaded with the default datasets.

You will now need to configure the database connections used by the server application. From the root server directory, navigate to *Data\Data\* and open *config.xml*. Update the username and password used by the database.

## Compiling Your Server

Install [Visual Studio](https://www.visualstudio.com). Once installed, launch *Server.sln*. In the Visual Studio interface, change *Debug* to *Release*. Build the server. Output files will be placed in *Server\bin\Release*.

That's it! Your server is setup and can now be run!
