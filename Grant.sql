﻿IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\LockTooTaxi')
BEGIN
    CREATE LOGIN [IIS APPPOOL\LockTooTaxi] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
CREATE USER [WebDatabaseUser] 
  FOR LOGIN [IIS APPPOOL\LockTooTaxi]
GO
EXEC sp_addrolemember 'db_owner', 'WebDatabaseUser'
GO