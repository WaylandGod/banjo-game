Naming:	
World refers to runtime objects.
Levels refer to data definitions from which worlds are created.
Levels != World because Levels can contain data used by IGame.

Controllers:
Constructors should only be used to read config and setup private fields
OnStart runs after all controllers are initialized and should be safe to reference runtime components
OnUpdate runs every frame
