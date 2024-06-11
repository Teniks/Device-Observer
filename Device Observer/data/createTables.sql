create table Users
(
	IdUser int primary key identity,
	FullNameUser nvarchar(255) not null,
	RoleUser nvarchar(255) not null
);

create table Resources
(
	IdResource int primary key identity not null,
	NameResource nvarchar(255) not null,
	TypeResource nvarchar(255) not null,
	DescriptionResource nvarchar(MAX) not null
);

create table AccessRights
(
	IdRight int primary key identity,
	IdUser int not null,
	IdResource int not null,
	Permission nvarchar(MAX) not null,
	foreign key (IdUser) references Users(IdUser),
	foreign key (IdResource) references Resources(IdResource)
);

create table ListOfChanges
(
	IdChanges int primary key identity not null,
	DateChanges DateTime default GETDATE(),
	Details nvarchar(MAX) not null
);

create table AccessLogs
(
	IdLog int primary key identity not null,
	TimeStampLog Datetime default GETDATE(),
	Details varchar(max) not null
);