create table UsersAU
(
	IdUserAU int primary key identity,
	LoginUserAU varchar(max) not null,
	PasswordUserAU varchar(max) not null,
	RoleUser varchar(31) not null
)