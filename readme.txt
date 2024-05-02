Another attemt at this mod

Stuff to remember

Scripts in mods must reside in <Mod>/Data/Scripts/<AnyFolderHere>/*.cs and the game compiles the found .cs files upon world load.
The subfolder in Scripts is important because each file/folder there is compiled as an individual assembly.
Also that subfolder name decides the Storage folder name (<WorkshopId>.sbm_<FolderFromScripts>) when using Write/ReadToLocalStorage().
Use the mod name for that folder in Scripts, makes it easier to identify the mod in Storage.

--> /Data/Scripts/improvised-engineering-2/
--> .sbm_improvised-engineering-2