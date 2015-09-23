# TodoListWebApi
Demo of .net with MongoDB. Dead simple app.


To connect a docker database I recommend using Docker.
Can be done as simple as: docker run --name mymongodb -d mongo

Add the required config to Web.config. Here you can replace the current mongodb connecion string.

All the tests are written in xunit, since this will be the default when .net 5.0 is released.
