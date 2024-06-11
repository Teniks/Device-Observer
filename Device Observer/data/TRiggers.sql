create trigger UpdateListChangesAUUsers on Users
after UPDATE
as
begin
	insert into ListOfChanges(Details)
	select 'Модифицирована запись в USers: где было IdUser = ' + cast(deleted.IdUser as nvarchar(max)) +
			' FullNameUser = ' + deleted.FullNameUser +
			' RoleUser = ' + deleted.RoleUser +
			' стало IdUser = ' + cast(inserted.IdUser as nvarchar(max)) +
			' FullNameUser = ' + inserted.FullNameUser +
			' RoleUser = ' + inserted.RoleUser
			from deleted join inserted on deleted.IdUser = inserted.IdUser
end;
go
create trigger UpdateListChangesADUsers on Users
after DELETE
as
begin
	insert into ListOfChanges(Details)
	select 'Удалена запись в USers: IdUser = ' + cast(deleted.IdUser as nvarchar(max)) +
			' FullNameUser = ' + deleted.FullNameUser +
			' RoleUser = ' + deleted.RoleUser
			from deleted
end;
go
create trigger UpdateListChangesAIUsers on Users
after INSERT
as
begin
	insert into ListOfChanges(Details)
	select 'Добавлена запись в USers: IdUser = ' + cast(inserted.IdUser as nvarchar(max)) +
			' FullNameUser = ' + inserted.FullNameUser +
			' RoleUser = ' + inserted.RoleUser
			from inserted
end;

go

create trigger UpdateListChangesAUResources on Resources
after UPDATE
as
begin
	insert into ListOfChanges(Details)
	select 'Модифицирована запись в Resources: где было IdResource = ' + cast(deleted.IdResource as nvarchar(max)) +
			' NameResource = ' + deleted.NameResource +
			' TypeResource = ' + deleted.TypeResource +
			' DescriptionResource = ' + deleted.DescriptionResource +
			' стало  IdResource = ' + cast(deleted.IdResource as nvarchar(max)) +
			' NameResource = ' + deleted.NameResource +
			' TypeResource = ' + deleted.TypeResource +
			' DescriptionResource = ' + deleted.DescriptionResource
			from deleted join inserted on deleted.IdResource = inserted.IdResource
end;
go
create trigger UpdateListChangesADResources on Resources
after DELETE
as
begin
	insert into ListOfChanges(Details)
	select 'Удалена запись в Resources: IdResource = ' + cast(deleted.IdResource as nvarchar(max)) +
			' NameResource = ' + deleted.NameResource +
			' TypeResource = ' + deleted.TypeResource +
			' DescriptionResource = ' + deleted.DescriptionResource
			from deleted
end;
go
create trigger UpdateListChangesAIResources on Resources
after INSERT
as
begin
	insert into ListOfChanges(Details)
	select 'Добавлена запись в Resources: IdResource = ' + cast(inserted.IdResource as nvarchar(max)) +
			' NameResource = ' + inserted.NameResource +
			' TypeResource = ' + inserted.TypeResource +
			' DescriptionResource = ' + inserted.DescriptionResource
			from inserted
end;

go

create trigger UpdateListChangesAUAccessLogs on AccessLogs
after UPDATE
as
begin
	insert into ListOfChanges(Details)
	select 'Модифицирована запись в AccessLogs: где было IdLog = ' + cast(deleted.IdLog as nvarchar(max)) +
			' TimeStampLog = ' + cast(deleted.TimeStampLog as nvarchar(max)) +
			' Details = ' + deleted.Details +
			' стало  IdLog = ' + cast(inserted.IdLog as nvarchar(max)) +
			' TimeStampLog = ' + cast(inserted.TimeStampLog as nvarchar(max)) +
			' Details = ' + inserted.Details
			from deleted join inserted on deleted.IdLog = inserted.IdLog
end;
go
create trigger UpdateListChangesADAccessLogs on AccessLogs
after DELETE
as
begin
	insert into ListOfChanges(Details)
	select 'Удалена запись в AccessLogs: IdLog = ' + cast(deleted.IdLog as nvarchar(max)) +
			' TimeStampLog = ' + cast(deleted.TimeStampLog as nvarchar(max)) +
			' Details = ' + deleted.Details
			from deleted
end;

go

create trigger UpdateAccessLogsAUAccessRights on AccessRights
after UPDATE
as
begin
	insert into AccessLogs(Details)
	select 'Модифицирована запись в AccessRights: где было IdRight = ' + cast(deleted.IdRight as nvarchar(max)) +
			' IdUser = ' + cast(deleted.IdUser as nvarchar(max))  +
			' IdResource = ' + cast(deleted.IdResource as nvarchar(max))  +
			' Permission = ' + deleted.Permission +
			' стало IdRight = ' + cast(deleted.IdRight as nvarchar(max)) +
			' IdUser = ' + cast(deleted.IdUser as nvarchar(max))  +
			' IdResource = ' + cast(deleted.IdResource as nvarchar(max))  +
			' Permission = ' + deleted.Permission
			from deleted join inserted on deleted.IdRight = inserted.IdRight
end;
go
create trigger UpdateAccessLogsADAccessRights on AccessRights
after DELETE
as
begin
	insert into AccessLogs(Details)
	select 'Удалена запись в AccessRights: IdRight = ' + cast(deleted.IdRight as nvarchar(max)) +
			' IdUser = ' + cast(deleted.IdUser as nvarchar(max))  +
			' IdResource = ' + cast(deleted.IdResource as nvarchar(max))  +
			' Permission = ' + deleted.Permission
			from deleted
end;
go
create trigger UpdateAccessLogsAIAccessRights on AccessRights
after INSERT
as
begin
	insert into AccessLogs(Details)
	select 'Добавлена запись в AccessRights: IdRight = ' + cast(inserted.IdRight as nvarchar(max)) +
			' IdUser = ' + cast(inserted.IdUser as nvarchar(max))  +
			' IdResource = ' + cast(inserted.IdResource as nvarchar(max))  +
			' Permission = ' + inserted.Permission
			from inserted
end;