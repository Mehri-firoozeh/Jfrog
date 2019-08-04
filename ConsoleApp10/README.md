# Guidence of applicaiton
This is a console applicaiton. You can run it by simply clone the repository and hit F5 in Visual Studio.

User provides repository name and an integer to indicate desired number of most popular artifacts.

* Default number of search item calls (n) is currently 1000. Therefore, n should not be larger than that.
(Both strings currently instantiated with default value but it can easily get change to be read from the console.)
* App config currently includes the credentials and base Url.
* The class name config is resposible to access and pass these information.  
* CommunicationApi is a platform to make all the REST calls necessary.



