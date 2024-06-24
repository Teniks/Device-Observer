create procedure BackupDataBase
    @backupPath VARCHAR(255)
AS
BEGIN
    DECLARE @databaseName VARCHAR(128);
    SELECT @databaseName = DB_NAME();

    -- Проверка существования папки для резервной копии
    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'master')
    BEGIN
        RAISERROR('Не удалось найти базу данных "master".', 16, 1)
        RETURN
    END

    -- Команда резервного копирования
    BACKUP DATABASE @databaseName
    TO DISK = @backupPath
    WITH INIT, STATS = 10;

    PRINT 'Резервная копия базы данных ' + @databaseName + ' успешно создана в ' + @backupPath;
END;
go
CREATE PROCEDURE RestoreDatabase
    @backupPath VARCHAR(255)
AS
BEGIN
    DECLARE @databaseName VARCHAR(128);
    SELECT @databaseName = DB_NAME();

    -- Проверка существования файла резервной копии
    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'master')
    BEGIN
        RAISERROR('Не удалось найти базу данных "master".', 16, 1)
        RETURN
    END

    -- Команда загрузки резервной копии
    RESTORE DATABASE @databaseName
    FROM DISK = @backupPath
    WITH REPLACE;

    PRINT 'База данных ' + @databaseName + ' успешно восстановлена из ' + @backupPath;
END;