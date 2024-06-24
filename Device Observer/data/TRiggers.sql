create trigger UpdateListChangesAUUsers on Users
after UPDATE
as
begin
	insert into ListOfChanges(Details)
	select N'Модифицирована запись в USers: где было IdUser = ' + cast(deleted.IdUser as nvarchar(max)) +
			N' FullNameUser = ' + deleted.FullNameUser +
			N' RoleUser = ' + deleted.RoleUser +
			N' стало IdUser = ' + cast(inserted.IdUser as nvarchar(max)) +
			N' FullNameUser = ' + inserted.FullNameUser +
			N' RoleUser = ' + inserted.RoleUser
			from deleted join inserted on deleted.IdUser = inserted.IdUser
end;
go
create trigger UpdateListChangesADUsers on Users
after DELETE
as
begin
	insert into ListOfChanges(Details)
	select N'Удалена запись в USers: IdUser = ' + cast(deleted.IdUser as nvarchar(max)) +
			N' FullNameUser = ' + deleted.FullNameUser +
			N' RoleUser = ' + deleted.RoleUser
			from deleted
end;
go
create trigger UpdateListChangesAIUsers on Users
after INSERT
as
begin
	insert into ListOfChanges(Details)
	select N'Добавлена запись в USers: IdUser = ' + cast(inserted.IdUser as nvarchar(max)) +
			N' FullNameUser = ' + inserted.FullNameUser +
			N' RoleUser = ' + inserted.RoleUser
			from inserted
end;

go

create trigger UpdateListChangesAUResources on Resources
after UPDATE
as
begin
	insert into ListOfChanges(Details)
	select N'Модифицирована запись в Resources: где было IdResource = ' + cast(deleted.IdResource as nvarchar(max)) +
			N' NameResource = ' + deleted.NameResource +
			N' TypeResource = ' + deleted.TypeResource +
			N' DescriptionResource = ' + deleted.DescriptionResource +
			N' стало  IdResource = ' + cast(deleted.IdResource as nvarchar(max)) +
			N' NameResource = ' + deleted.NameResource +
			N' TypeResource = ' + deleted.TypeResource +
			N' DescriptionResource = ' + deleted.DescriptionResource
			from deleted join inserted on deleted.IdResource = inserted.IdResource
end;
go
create trigger UpdateListChangesADResources on Resources
after DELETE
as
begin
	insert into ListOfChanges(Details)
	select N'Удалена запись в Resources: IdResource = ' + cast(deleted.IdResource as nvarchar(max)) +
			N' NameResource = ' + deleted.NameResource +
			N' TypeResource = ' + deleted.TypeResource +
			N' DescriptionResource = ' + deleted.DescriptionResource
			from deleted
end;
go
create trigger UpdateListChangesAIResources on Resources
after INSERT
as
begin
	insert into ListOfChanges(Details)
	select N'Добавлена запись в Resources: IdResource = ' + cast(inserted.IdResource as nvarchar(max)) +
			N' NameResource = ' + inserted.NameResource +
			N' TypeResource = ' + inserted.TypeResource +
			N' DescriptionResource = ' + inserted.DescriptionResource
			from inserted
end;

go

create trigger UpdateListChangesAUAccessLogs on AccessLogs
after UPDATE
as
begin
	insert into ListOfChanges(Details)
	select N'Модифицирована запись в AccessLogs: где было IdLog = ' + cast(deleted.IdLog as nvarchar(max)) +
			N' TimeStampLog = ' + cast(deleted.TimeStampLog as nvarchar(max)) +
			N' Details = ' + deleted.Details +
			N' стало  IdLog = ' + cast(inserted.IdLog as nvarchar(max)) +
			N' TimeStampLog = ' + cast(inserted.TimeStampLog as nvarchar(max)) +
			N' Details = ' + inserted.Details
			from deleted join inserted on deleted.IdLog = inserted.IdLog
end;
go
create trigger UpdateListChangesADAccessLogs on AccessLogs
after DELETE
as
begin
	insert into ListOfChanges(Details)
	select N'Удалена запись в AccessLogs: IdLog = ' + cast(deleted.IdLog as nvarchar(max)) +
			N' TimeStampLog = ' + cast(deleted.TimeStampLog as nvarchar(max)) +
			N' Details = ' + deleted.Details
			from deleted
end;

go

create trigger UpdateAccessLogsAUAccessRights on AccessRights
after UPDATE
as
begin
	insert into AccessLogs(Details)
	select N'Модифицирована запись в AccessRights: где было IdRight = ' + cast(deleted.IdRight as nvarchar(max)) +
			N' IdUser = ' + cast(deleted.IdUser as nvarchar(max))  +
			N' IdResource = ' + cast(deleted.IdResource as nvarchar(max))  +
			N' Permission = ' + deleted.Permission +
			N' стало IdRight = ' + cast(deleted.IdRight as nvarchar(max)) +
			N' IdUser = ' + cast(deleted.IdUser as nvarchar(max))  +
			N' IdResource = ' + cast(deleted.IdResource as nvarchar(max))  +
			N' Permission = ' + deleted.Permission
			from deleted join inserted on deleted.IdRight = inserted.IdRight
end;
go
create trigger UpdateAccessLogsADAccessRights on AccessRights
after DELETE
as
begin
	insert into AccessLogs(Details)
	select N'Удалена запись в AccessRights: IdRight = ' + cast(deleted.IdRight as nvarchar(max)) +
			N' IdUser = ' + cast(deleted.IdUser as nvarchar(max))  +
			N' IdResource = ' + cast(deleted.IdResource as nvarchar(max))  +
			N' Permission = ' + deleted.Permission
			from deleted
end;
go
create trigger UpdateAccessLogsAIAccessRights on AccessRights
after INSERT
as
begin
	insert into AccessLogs(Details)
	select N'Добавлена запись в AccessRights: IdRight = ' + cast(inserted.IdRight as nvarchar(max)) +
			N' IdUser = ' + cast(inserted.IdUser as nvarchar(max))  +
			N' IdResource = ' + cast(inserted.IdResource as nvarchar(max))  +
			N' Permission = ' + inserted.Permission
			from inserted
end;